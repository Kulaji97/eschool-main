using ECOMSYSTEM.Repository.ApplicationUsers;
using ESCHOOLING.DataAccess.EntityModel;
using ESCHOOLING.Repository;
using ESCHOOLING.Shared.Interfaces;
using ESCHOOLING.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace ESCHOOLING.Web.SignalRChat
{
    [Authorize]
    public class CounsellingHub:Hub
    {

        IApplicationUserRepository _applicationUserRepository;
        IChatRepository _chatRepository;
        public CounsellingHub(IApplicationUserRepository applicationUserRepository, IChatRepository chatRepository)
        {
            _applicationUserRepository = applicationUserRepository;
            _chatRepository = chatRepository;
        }

        private static List<OnlineUser> chatUsers=new List<OnlineUser>();
        public async override Task OnConnectedAsync()
        {
            var userIdentifier=Context.UserIdentifier;
            var conId = Context.ConnectionId;
            var strUserId=Context.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            var role = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            var userId=Convert.ToInt32(strUserId);
            var newOnlineUser = new OnlineUser() { ConnectionId = conId, UserId = userId, Role = role??"" };
            chatUsers.Add(newOnlineUser);
            List<string> connectionsToInform=new List<string>();
            if(role== "Doctor")
                connectionsToInform=chatUsers.Where(c=>c.Role== "Parent"||role== "Student").Select(u=>u.ConnectionId).ToList();
            if(role== "Parent"|| role == "Student")
                connectionsToInform = chatUsers.Where(c => c.Role == "Doctor").Select(u => u.ConnectionId).ToList();

            await _applicationUserRepository.SetUserOnline(Convert.ToInt64(userId));

            await Clients.Users(connectionsToInform).SendAsync("UserConnected",userId);

        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            var offlineUser = chatUsers.FirstOrDefault(x => x.UserId == Convert.ToInt64(userId));
            chatUsers.Remove(offlineUser);
            await _applicationUserRepository.SetUserOffline(Convert.ToInt64(userId));
        }

        public async Task SendMessage(long fromUserId, long toUserId, string message)
        {
            var chatMessage = new ChatMessage { FromUserId = fromUserId, ToUserId = toUserId, TimeSent = DateTime.Now, HasRecieverRed = false, MessageText = message };
            await _chatRepository.Add(chatMessage);

            //var toUser = chatUsers.FirstOrDefault(x => x.UserId == toUserId);
            //if (toUser != null)
            //{
            //    await Clients.User(toUserId).SendAsync("ReceiveMessage", fromUserId, DateTime.Now, message);
            //}
            //var chatMessage=new ChatMessage() { FromUserId= fromUserId,ToUserId=toUserId, HasRecieverRed=false, TimeSent=DateTime.Now, MessageText= message };
            await Clients.User(toUserId.ToString()).SendAsync("ReceiveMessage", chatMessage);
            
        }
    }
}
