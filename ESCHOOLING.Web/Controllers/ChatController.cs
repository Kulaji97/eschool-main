using ECOMSYSTEM.Shared;
using ECOMSYSTEM.Web.Controllers;
using ECOMSYSTEM.Web.Services;
using ESCHOOLING.Repository;
using ESCHOOLING.Shared.Interfaces;
using ESCHOOLING.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace ESCHOOLING.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chatService;
        private readonly IApplicationUserService _applicationUserService;
        public ChatController(ILogger<ChatController> logger, IChatService chatService, IApplicationUserService applicatioUserService)
        {
            _logger = logger;
            _chatService = chatService;
            _applicationUserService = applicatioUserService;

        }

        [HttpGet]
        [Route("GetChatHistory/{userId}")]
        public async Task<ChatHistory> GetChatHistory(int userId)
        {
            var chatHistory= await _chatService.GetChatHistory(userId);
            return chatHistory;
        }

    }
}
