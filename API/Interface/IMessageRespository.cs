using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helper;

namespace API.Interface
{
    public interface IMessageRespository
    {
        Task AddMessage(Message message);
        Task<IEnumerable<Message>> GetMessage(MessageFilter filter);
    }
}