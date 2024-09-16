using ESCHOOLING.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCHOOLING.Shared.Interfaces
{
    public interface IChatRepository
    {
        Task<List<ChatMessage>> GetChatHistory(int userId); 
        Task Add(ChatMessage message);
    }
}
