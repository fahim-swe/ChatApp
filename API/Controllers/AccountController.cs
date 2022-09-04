using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }



        [HttpPost("signup")]
        public async Task<IActionResult> CreateAccount(CreateAccountDto createAcountDto)
        {
            if(!ModelState.IsValid) return BadRequest("Not valid Data");

            if(await _unitOfWork.AccountRepository.IsUsernameExits(createAcountDto.UserName))
                return BadRequest("Username Already Exits");
            
            if(await _unitOfWork.AccountRepository.IsEmailExits(createAcountDto.Email))
                return BadRequest("Email is Alread Exits");
            
            using var hmac = new HMACSHA512();
            var user = _mapper.Map<AppUser>(createAcountDto);

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(createAcountDto.Password));
            user.PasswordSalt = hmac.Key;

            await _unitOfWork.AccountRepository.AddUser(user);
            
            if(!await _unitOfWork.Commit()) 
                return BadRequest("Failed To Create Account");
            

            return Ok(_tokenService.CreateToken(user));
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAccount(LoginDto loginDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest("Not Valid data");
            
            var user = await _unitOfWork.AccountRepository.GetUserByName(loginDto.UserName);
            if(user == null) return BadRequest("Invaid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i])
                {
                    return BadRequest("Wrong Password");
                }
            }

            return Ok(_tokenService.CreateToken(user));
        }
    }
}