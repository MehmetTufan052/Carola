using Carola.WebUI.Models;
using Carola.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Controllers
{
    [ApiController]
    [Route("api/ai-chat")]
    public class AiChatController : ControllerBase
    {
        private readonly IAiChatService _aiChatService;

        public AiChatController(IAiChatService aiChatService)
        {
            _aiChatService = aiChatService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AiChatRequest request, CancellationToken cancellationToken)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new AiChatResponse
                {
                    Message = "Lütfen önce bir mesaj yazın."
                });
            }

            var answer = await _aiChatService.GetAssistantReplyAsync(request.Message.Trim(), cancellationToken);
            return Ok(new AiChatResponse
            {
                Message = answer
            });
        }
    }
}
