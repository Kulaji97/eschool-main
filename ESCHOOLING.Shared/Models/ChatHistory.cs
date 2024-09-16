using ECOMSYSTEM.Shared.Models;

namespace ESCHOOLING.Shared.Models
{
    public class ChatHistory
    {
        public ChatHistory()
        {
            ChatMessages=new List<ChatMessage>();
            ChatUsers=new List<ChatUser>();
        }
        public List<ChatMessage> ChatMessages { get; set; }
        public List<ChatUser> ChatUsers { get; set; }
    }
}
