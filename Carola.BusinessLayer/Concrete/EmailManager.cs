using System.Net;
using System.Net.Mail;
using Carola.BusinessLayer.Abstract;
using Carola.BusinessLayer.Utilities;
using Carola.DtoLayer.Dtos.EmailDtos;

namespace Carola.BusinessLayer.Concrete
{
    public class EmailManager : IEmailService
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public EmailManager(SmtpConfiguration smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public async Task SendBookingApprovalOfferAsync(BookingApprovalEmailDto model)
        {
            if (string.IsNullOrWhiteSpace(_smtpConfiguration.Host) ||
                string.IsNullOrWhiteSpace(_smtpConfiguration.FromEmail) ||
                string.IsNullOrWhiteSpace(model.CustomerEmail))
            {
                throw new InvalidOperationException("SMTP ayarlari veya musteri e-postasi eksik. appsettings.json icindeki Smtp alanini doldurun.");
            }

            using var message = new MailMessage
            {
                From = new MailAddress(_smtpConfiguration.FromEmail, _smtpConfiguration.FromName),
                Subject = $"Arac teklifiniz hazir: {model.CarDisplayName}",
                Body = BuildOfferBody(model),
                IsBodyHtml = true
            };

            message.To.Add(model.CustomerEmail);

            using var client = new SmtpClient(_smtpConfiguration.Host, _smtpConfiguration.Port)
            {
                EnableSsl = _smtpConfiguration.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            if (!string.IsNullOrWhiteSpace(_smtpConfiguration.UserName))
            {
                client.Credentials = new NetworkCredential(_smtpConfiguration.UserName, _smtpConfiguration.Password);
            }

            await client.SendMailAsync(message);
        }

        private static string BuildOfferBody(BookingApprovalEmailDto model)
        {
            var safeName = WebUtility.HtmlEncode(model.CustomerName);
            var safeCar = WebUtility.HtmlEncode(model.CarDisplayName);
            var safePlate = WebUtility.HtmlEncode(model.PlateNumber);
            var safeFuel = WebUtility.HtmlEncode(model.FuelType);
            var safeTransmission = WebUtility.HtmlEncode(model.TransmissionType);
            var totalPrice = model.TotalPrice.ToString("N0");
            var dailyPrice = model.DailyPrice.ToString("N0");

            return $@"
<!DOCTYPE html>
<html lang=""tr"">
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>Carola Teklif Maili</title>
</head>
<body style=""margin:0;padding:0;background:#eef3fb;font-family:Segoe UI,Arial,sans-serif;color:#132238;"">
  <div style=""display:none;max-height:0;overflow:hidden;opacity:0;"">Booking onaylandi. {safeCar} icin teklifiniz ve %30 indirim avantaji hazir.</div>
  <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""background:#eef3fb;padding:24px 12px;"">
    <tr>
      <td align=""center"">
        <table role=""presentation"" width=""680"" cellspacing=""0"" cellpadding=""0"" style=""width:680px;max-width:100%;background:#ffffff;border-radius:28px;overflow:hidden;box-shadow:0 18px 55px rgba(15,23,42,.10);"">
          <tr>
            <td style=""padding:16px 24px;background:#f8fbff;border-bottom:1px solid #dbe6f5;"">
              <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"">
                <tr>
                  <td align=""left"" style=""font-size:18px;font-weight:800;color:#0f172a;letter-spacing:.02em;"">Carola</td>
                  <td align=""right"" style=""font-size:12px;font-weight:700;color:#2563eb;text-transform:uppercase;letter-spacing:.10em;"">Booking Onayi</td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td bgcolor=""#0f172a"" style=""background:#0f172a;background-image:linear-gradient(135deg,#0f172a 0%,#1d4ed8 100%);padding:36px 32px 34px;"">
              <div style=""font-size:12px;letter-spacing:.16em;text-transform:uppercase;color:#bfdbfe;font-weight:800;"">Teklif Hazir</div>
              <h1 style=""margin:14px 0 12px;font-size:38px;line-height:1.06;color:#ffffff;font-weight:800;"">Rezervasyonunuz Onaylandi</h1>
              <p style=""margin:0;color:#dbeafe;font-size:15px;line-height:1.8;"">
                Sayin {safeName}, {safeCar} icin booking talebiniz admin tarafinda onaylandi. Araciniz, fiyat teklifiniz ve kampanya avantajlariniz asagida sizi bekliyor.
              </p>
              <table role=""presentation"" cellspacing=""0"" cellpadding=""0"" style=""margin-top:20px;"">
                <tr>
                  <td style=""padding:10px 16px;border-radius:999px;background:rgba(255,255,255,.12);font-size:13px;color:#ffffff;font-weight:700;"">{model.TotalDay} gunluk premium teklif</td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td style=""padding:24px 24px 10px;"">
              <img src=""{model.CarImageUrl}"" alt=""Arac Gorseli"" style=""width:100%;max-height:300px;object-fit:cover;border-radius:22px;display:block;background:#eef2ff;"">
            </td>
          </tr>
          <tr>
            <td style=""padding:10px 24px 24px;"">
              <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""border-collapse:collapse;"">
                <tr>
                  <td style=""padding:22px;border:1px solid #e5e7eb;border-radius:22px;background:#f8fbff;"">
                    <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"">
                      <tr>
                        <td style=""font-size:12px;font-weight:800;text-transform:uppercase;letter-spacing:.10em;color:#2563eb;padding-bottom:10px;"">Teklif Ozeti</td>
                      </tr>
                      <tr>
                        <td style=""font-size:30px;font-weight:800;color:#111827;padding-bottom:8px;"">{safeCar}</td>
                      </tr>
                      <tr>
                        <td style=""font-size:14px;color:#475569;line-height:1.9;padding-bottom:18px;"">
                          Plaka: <strong>{safePlate}</strong><br>
                          Yakit: <strong>{safeFuel}</strong><br>
                          Vites: <strong>{safeTransmission}</strong><br>
                          Kiralama Suresi: <strong>{model.TotalDay} gun</strong>
                        </td>
                      </tr>
                      <tr>
                        <td style=""padding:18px 20px;border-radius:18px;background:#111827;color:#ffffff;"">
                          <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"">
                            <tr>
                              <td style=""font-size:13px;color:#cbd5e1;padding-bottom:6px;"">Gunluk Fiyat</td>
                              <td align=""right"" style=""font-size:28px;font-weight:800;color:#ffffff;padding-bottom:6px;"">{dailyPrice} TL</td>
                            </tr>
                            <tr>
                              <td colspan=""2"" style=""padding:8px 0;border-bottom:1px solid rgba(255,255,255,.12);""></td>
                            </tr>
                            <tr>
                              <td style=""font-size:13px;color:#cbd5e1;padding-top:12px;"">Toplam Teklif Tutari</td>
                              <td align=""right"" style=""font-size:36px;font-weight:800;color:#bef264;padding-top:12px;"">{totalPrice} TL</td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td style=""padding:0 24px 22px;"">
              <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"">
                <tr>
                  <td style=""padding:16px 18px;border-radius:18px;background:#ecfdf5;border:1px solid #bbf7d0;color:#166534;font-size:14px;line-height:1.8;"">
                    <strong style=""display:block;font-size:15px;color:#14532d;margin-bottom:6px;"">Onay Bilgisi</strong>
                    Booking talebiniz aktif olarak onaylandi. Dilerseniz bu teklif uzerinden ek kampanya ve esnek teslimat avantaji ile rezervasyon surecinizi tamamlayabilirsiniz.
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td style=""padding:0 24px 24px;"">
              <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"">
                <tr>
                  <td style=""padding-right:8px;width:50%;"">
                    <img src=""{model.CouponImageUrl}"" alt=""Yuzde 30 Indirim Kuponu"" style=""width:100%;border-radius:20px;display:block;border:1px solid #dbe6f5;"">
                  </td>
                  <td style=""padding-left:8px;width:50%;"">
                    <img src=""{model.PromoImageUrl}"" alt=""Promosyon Gorseli"" style=""width:100%;border-radius:20px;display:block;border:1px solid #dbe6f5;"">
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td style=""padding:0 24px 28px;"">
              <div style=""padding:18px 20px;border-radius:18px;background:#eff6ff;border:1px solid #bfdbfe;color:#1e3a8a;font-size:14px;line-height:1.8;"">
                <strong>Ozel teklif notu:</strong> Bu e-postadaki kampanya gorselinde yer alan kupon ile secili araclarda %30'a varan indirim firsatindan yararlanabilirsiniz.
                Onaylanan talebiniz uzerinden iletisime gecerek son fiyatlandirmayi tamamlayabilirsiniz.
              </div>
            </td>
          </tr>
          <tr>
            <td style=""padding:20px 24px 30px;background:#f8fafc;border-top:1px solid #e5e7eb;font-size:12px;color:#64748b;line-height:1.8;"">
              Bu e-posta Carola admin panelinden booking onayi sonrasinda otomatik gonderilmistir.<br>
              Sorulariniz icin bizimle iletisime gecebilirsiniz.
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>";
        }
    }
}
