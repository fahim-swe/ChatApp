using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;

namespace API.Interface
{
    public interface ITokenService
    {
        UserDto CreateToken(AppUser user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        UserDto CreateToken(List<Claim> authClaims, AppUser user);
    }
}