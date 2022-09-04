using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Helper;

namespace API.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUserDto>>  GetUsers(PaginationFilter paginationFilter, string username);
        Task<int> TotallRecords();
    }
}