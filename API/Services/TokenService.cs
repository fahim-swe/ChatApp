using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
       

        public TokenService(IConfiguration config, IMapper mapper)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _config = config;
            _mapper = mapper;
        }

        public UserDto CreateToken(AppUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            authClaims.Add(new Claim(ClaimTypes.Role, user.UserName));
            authClaims.Add(new Claim(ClaimTypes.Role, user.Email));
            authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            
            return CreateToken(authClaims, user);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _config["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
       
        public UserDto CreateToken(List<Claim> authClaims, AppUser user)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _config["JWT:Secret"]));
            _ = int.TryParse( _config["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var refreshToken = GenerateRefreshToken();
          
            _ = int.TryParse(_config["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);


            var userDto = _mapper.Map<UserDto>(user);

            userDto.Token = new JwtSecurityTokenHandler().WriteToken(token);
            userDto.RefreshToken = refreshToken;
            userDto.ExpiredTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            return userDto;    
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}