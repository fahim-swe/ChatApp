using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;

namespace API.Interface
{
    public interface IAccountRepository
    {
        Task AddUser(AppUser user);

        Task<Boolean> IsUsernameExits(string UserName);
        Task<Boolean> IsEmailExits(string Email);

        Task<AppUser?> GetUserByName(string UserName);
        Task<AppUser?> GetUserById (Guid Id);

        Task<Boolean> Commit();
    }
}