using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helper;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class MessageRepository : IMessageRespository
    {

        private readonly DataContext _context;

        public MessageRepository(DataContext context)
        {
            _context = context;
        }


        public async Task AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
        }


        public async Task<IEnumerable<Message>> GetMessage(MessageFilter filter)
        {
            
            var messages = await _context.Messages
                .Where( m => m.SenderUsername == filter.SenderUsername && m.RecipientUsername == filter.RecipientUsername
                || 
                m.SenderUsername == filter.RecipientUsername && m.RecipientUsername == filter.SenderUsername)

                .OrderBy( m => m.MessageSent)
                .Skip( (filter.PageNumber - 1) * filter.PageSize )
                .Take(filter.PageSize)
                .ToListAsync();

            var unreadMessages = messages.Where( m => m.MessageRead == null && m.RecipientUsername == filter.SenderUsername).ToList();

            if(unreadMessages.Any())
            {
                foreach(var message in unreadMessages){
                    message.MessageRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }

            return messages;
        }
    
       
    
    }
}