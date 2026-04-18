namespace Carola.WebUI.Services
{
    public interface IAiChatService
    {
        Task<string> GetAssistantReplyAsync(string userMessage, CancellationToken cancellationToken = default);
    }
}
