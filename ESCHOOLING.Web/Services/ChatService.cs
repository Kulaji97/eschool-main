using ECOMSYSTEM.Repository.ApplicationUsers;
using ESCHOOLING.Repository;
using ESCHOOLING.Shared.Interfaces;
using ESCHOOLING.Shared.Models;

namespace ESCHOOLING.Web.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ChatService(IChatRepository chatRepository, IApplicationUserRepository applicationUserRepository)
        {
            _chatRepository = chatRepository;
            _applicationUserRepository = applicationUserRepository;
        }
        public async Task<ChatHistory> GetChatHistory(int userId)
        {
            var chatHistory =new ChatHistory();
            chatHistory.ChatMessages = await _chatRepository.GetChatHistory(userId);
            List<long> userIds = new List<long>();
            foreach (var message in chatHistory.ChatMessages)
            {
                if (!userIds.Contains(message.FromUserId))
                {
                    userIds.Add(message.FromUserId);
                }
                if (!userIds.Contains(message.ToUserId))
                {
                    userIds.Add(message.ToUserId);
                }
            }
            foreach (var chatUserId in userIds)
            {
                var user = await _applicationUserRepository.GetById(chatUserId);
                if (user != null)
                {
                    ChatUser chatUser = new ChatUser { UserId = (int)user.UserId, UserName = user.Username, ImageUrl = user.ImageUrl, IsYou = user.UserId == userId, IsOnline=user.IsOnline??false };
                    chatHistory.ChatUsers.Add(chatUser);
                }
            }

            return chatHistory;
        }
    }
}
