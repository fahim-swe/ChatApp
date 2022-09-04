using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Helper;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
     [Authorize]
    public class MessageController : BaseApiController
    {
       
        private readonly IUnitOfWork _unitOfWork;

        public MessageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {
            
           if(!await _unitOfWork.AccountRepository.IsUsernameExits(createMessageDto.RecipientUsername))
                return BadRequest("Not a valid user");
            
           var SenderUsername = User.GetUsername();
           if(SenderUsername == createMessageDto.RecipientUsername)
                return BadRequest("You can't send message yourself");
            
           var message = new Message
           {
             SenderUsername = SenderUsername,
             RecipientUsername = createMessageDto.RecipientUsername,
             Content = createMessageDto.Content
           };

           await _unitOfWork.MessageRespository.AddMessage(message);
           if(await _unitOfWork.Commit())
           {
            return Ok(message);
           }

           return BadRequest("Failed To Send Message");
        }


        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery]MessageParamDto messageParam)
        {
            var messageFilter = new MessageFilter
            {
                SenderUsername = User.GetUsername(),
                RecipientUsername = messageParam.RecipientUsername,
                PageNumber = messageParam.PageNumber,
                PageSize = messageParam.PageSize
            };
            messageFilter.SenderUsername = User.GetUsername();
            var messages = await _unitOfWork.MessageRespository.GetMessage(messageFilter);

            return Ok(new PagedResponse<IEnumerable<Message>>( messages, messageFilter.PageNumber, messageFilter.PageSize, 0 ));
        }
    }
}