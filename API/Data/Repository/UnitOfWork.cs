using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interface;
using AutoMapper;

namespace API.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IAccountRepository AccountRepository =>  new AccountRepository(_context);

        

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);

        public IMessageRespository MessageRespository => new MessageRepository(_context);

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}