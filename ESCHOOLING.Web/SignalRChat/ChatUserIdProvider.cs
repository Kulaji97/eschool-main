using Microsoft.AspNetCore.SignalR;

namespace ESCHOOLING.Web.SignalRChat
{
    public class ChatUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var strUserId = connection.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            return strUserId;
        }
    }
}
