using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddUser(AppUser user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<AppUser?> GetUserById(Guid Id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<AppUser?> GetUserByName(string UserName)
        {
            return await _context.Users.FirstOrDefaultAsync( x => x.UserName == UserName);
        }

     

        public async Task<bool> IsUsernameExits(string UserName)
        {
            return await _context.Users.AnyAsync( x => x.UserName == UserName);
        }

        public async Task<bool> IsEmailExits(string Email)
        {
            return await _context.Users.AnyAsync(x => x.Email == Email);
        }
    }
}