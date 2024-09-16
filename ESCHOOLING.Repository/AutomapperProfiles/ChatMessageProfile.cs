using ECOMSYSTEM.Shared.Models;
using ESCHOOLING.DataAccess.EntityModel;
using ESCHOOLING.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace ESCHOOLING.Repository.AutomapperProfiles
{
    public class ChatMessageProfile : Profile
    {
        public ChatMessageProfile()
        {
            CreateMap<TblChatMessage, ChatMessage>();
            CreateMap<ChatMessage, TblChatMessage>();
        }
    }
}
