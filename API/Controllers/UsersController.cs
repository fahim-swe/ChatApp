using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Extensions;
using API.Helper;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
     [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]PaginationFilter pagination)
        {
            var members = await _unitOfWork.UserRepository.GetUsers(new PaginationFilter(pagination.PageNumber, pagination.PageSize), User.GetUsername());

            var totalRecords = await _unitOfWork.UserRepository.TotallRecords();

            return Ok(new PagedResponse<IEnumerable<AppUserDto>>( members, pagination.PageNumber, pagination.PageSize, totalRecords ));
        }
    }
}