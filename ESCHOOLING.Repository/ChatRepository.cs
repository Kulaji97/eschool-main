using AutoMapper;
using ECOMSYSTEM.Shared.Models;
using ESCHOOLING.DataAccess.EntityModel;
using ESCHOOLING.Shared.Interfaces;
using ESCHOOLING.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCHOOLING.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ECOM_WebContext _dbContext = new ECOM_WebContext();
        private readonly IMapper _mapper;

        public ChatRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Add(ChatMessage message)
        {
            var tblChatMessage=_mapper.Map<TblChatMessage>(message);
            await _dbContext.TblChatMessages.AddAsync(tblChatMessage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ChatMessage>> GetChatHistory(int userId)
        {
            try
            {
                var messages =await _dbContext.TblChatMessages.Where(m=>m.FromUserId==userId|| m.ToUserId==userId).OrderBy(m=>m.Id).ToListAsync();
                var mappedMessages = _mapper.Map<List<ChatMessage>>(messages);

                return mappedMessages;
            }
            catch (Exception eX)
            {
                return new List<ChatMessage>();
            }
        }
    }
}
