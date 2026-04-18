using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.CarDtos;
using Carola.WebUI.Options;
using Microsoft.Extensions.Options;

namespace Carola.WebUI.Services
{
    public class OpenAiChatService : IAiChatService
    {
        private const int MaxCarsInPrompt = 24;
        private readonly HttpClient _httpClient;
        private readonly ICarService _carService;
        private readonly OpenAiChatOptions _options;
        private readonly ILogger<OpenAiChatService> _logger;

        public OpenAiChatService(
            HttpClient httpClient,
            ICarService carService,
            IOptions<OpenAiChatOptions> options,
            ILogger<OpenAiChatService> logger)
        {
            _httpClient = httpClient;
            _carService = carService;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<string> GetAssistantReplyAsync(string userMessage, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(userMessage))
            {
                return "Sorunuzu yazdıktan sonra size uygun araçları önerebilirim.";
            }

            var apiKey = ResolveApiKey();
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return "AI asistan şu anda yapılandırılmadı. Lütfen sunucu tarafında OpenAI API anahtarını ekleyin.";
            }

            var cars = await _carService.GetAllCarsWithCategoryAsync();
            var promptCars = cars
                .Where(x => x.IsAvailable)
                .DefaultIfEmpty()
                .FirstOrDefault() is null
                    ? cars
                    : cars.Where(x => x.IsAvailable).ToList();

            var inventorySummary = BuildInventorySummary(promptCars);
            if (string.IsNullOrWhiteSpace(inventorySummary))
            {
                return "Şu anda öneri sunabileceğim araç verisi bulunamadı.";
            }

            var requestBody = new
            {
                model = string.IsNullOrWhiteSpace(_options.Model) ? "gpt-4.1-mini" : _options.Model,
                instructions = BuildInstructions(),
                max_output_tokens = 550,
                input = new object[]
                {
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new
                            {
                                type = "input_text",
                                text = $"Kullanıcı mesajı: {userMessage}\n\nAraç envanteri:\n{inventorySummary}"
                            }
                        }
                    }
                }
            };

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, ResolveBaseUrl());
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            using var response = await _httpClient.SendAsync(requestMessage, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("OpenAI yanıtı başarısız oldu. Status: {StatusCode}, Body: {Body}", response.StatusCode, responseContent);
                return "Şu anda AI asistanına ulaşırken bir sorun oluştu. Lütfen biraz sonra tekrar deneyin.";
            }

            var assistantReply = ExtractAssistantReply(responseContent);
            return string.IsNullOrWhiteSpace(assistantReply)
                ? "Size uygun araçları analiz ettim ancak şu anda yanıt oluşturamadım. Lütfen sorunuzu biraz daha açık yazar mısınız?"
                : assistantReply;
        }

        private string BuildInstructions()
        {
            return
                "Sen Carola için çalışan deneyimli bir araç kiralama danışmanısın.\n" +
                "Kullanıcıya her zaman Türkçe cevap ver.\n" +
                "Yanıtların doğal, teknik, mantıklı ve çalışan gibi güven veren bir tonda olsun.\n" +
                "Sadece verilen araç envanterine göre öneri yap.\n" +
                "Mümkünse 2 veya 3 araç öner ve her biri için neden uygun olduğunu açıkla.\n" +
                "Fiyatları mutlaka Türk lirası ve günlük fiyat olarak belirt.\n" +
                "Kullanıcının ihtiyacı aile, bagaj, çocuk, yakıt, bütçe, vites veya konfor ise bunları yorumla.\n" +
                "Envanterde uygun araç azsa bunu dürüstçe söyle ve en yakın alternatifleri ver.\n" +
                "Yanıtı kısa paragraflar halinde ver, gereksiz uzun olma.";
        }

        private string BuildInventorySummary(IEnumerable<ResultCarDto> cars)
        {
            var builder = new StringBuilder();

            foreach (var car in cars.Take(MaxCarsInPrompt))
            {
                builder.Append("- ID: ")
                    .Append(car.CarId)
                    .Append(" | Marka Model: ")
                    .Append(car.Brand)
                    .Append(' ')
                    .Append(car.Model)
                    .Append(" | Kategori: ")
                    .Append(car.Category?.CategoryName ?? "-")
                    .Append(" | Yıl: ")
                    .Append(car.ModelYear)
                    .Append(" | Koltuk: ")
                    .Append(car.SeatCount)
                    .Append(" | Bagaj: ")
                    .Append(car.LuggageCapacity)
                    .Append(" | Yakıt: ")
                    .Append(car.FuelType)
                    .Append(" | Vites: ")
                    .Append(car.TransmissionType)
                    .Append(" | Günlük Fiyat: ")
                    .Append(car.DailyPrice.ToString("0.##"))
                    .Append(" ₺")
                    .Append(" | Durum: ")
                    .Append(string.IsNullOrWhiteSpace(car.Status) ? (car.IsAvailable ? "Müsait" : "Dolu") : car.Status)
                    .AppendLine();
            }

            return builder.ToString().Trim();
        }

        private string ExtractAssistantReply(string responseContent)
        {
            using var document = JsonDocument.Parse(responseContent);
            if (!document.RootElement.TryGetProperty("output", out var outputElement) || outputElement.ValueKind != JsonValueKind.Array)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            foreach (var item in outputElement.EnumerateArray())
            {
                if (!item.TryGetProperty("type", out var typeElement) || typeElement.GetString() != "message")
                {
                    continue;
                }

                if (!item.TryGetProperty("content", out var contentElement) || contentElement.ValueKind != JsonValueKind.Array)
                {
                    continue;
                }

                foreach (var content in contentElement.EnumerateArray())
                {
                    if (content.TryGetProperty("type", out var contentType) &&
                        contentType.GetString() == "output_text" &&
                        content.TryGetProperty("text", out var textElement))
                    {
                        builder.AppendLine(textElement.GetString());
                    }
                }
            }

            return builder.ToString().Trim();
        }

        private string ResolveApiKey()
        {
            return !string.IsNullOrWhiteSpace(_options.ApiKey)
                ? _options.ApiKey
                : Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? string.Empty;
        }

        private string ResolveBaseUrl()
        {
            return string.IsNullOrWhiteSpace(_options.BaseUrl)
                ? "https://api.openai.com/v1/responses"
                : _options.BaseUrl;
        }
    }
}
