using ESCHOOLING.Shared;
using ESCHOOLING.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCHOOLING.Shared.Interfaces
{
    public interface IChatService
    {
        Task<ChatHistory> GetChatHistory(int userId);
    }
}
