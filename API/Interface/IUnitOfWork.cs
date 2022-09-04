using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository {get; }
        IUserRepository UserRepository {get;}
        IMessageRespository MessageRespository {get;}
        Task<Boolean> Commit();
        Boolean HasChanges();
    }
}