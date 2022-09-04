using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helper;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<AppUserDto>> GetUsers(PaginationFilter paginationFilter, string username)
        {
            var members = await _context.Users
                .ProjectTo<AppUserDto>(_mapper.ConfigurationProvider)
                .Where( x => x.UserName != username)
                .Skip( (paginationFilter.PageNumber - 1) * paginationFilter.PageSize )
                .Take(paginationFilter.PageSize)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
            
            
            return members;
        }

       
        public async Task<int> TotallRecords()
        {
            return await _context.Users.CountAsync();
        }
    }
}