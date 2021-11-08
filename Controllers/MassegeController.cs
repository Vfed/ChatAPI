using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChatAPI.Data.Dto;
using ChatAPI.Data.Models;
using ChatAPI.Servises.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MassegeController : ControllerBase
    {
        private readonly IMessageAction _messageAction;
        public MassegeController(IMessageAction messageAction)
        {
            _messageAction = messageAction;
        }

        [HttpGet("get")]
        public IEnumerable<Message> GetMessagesUser(Guid chatId, DateTime dateTime)
        {
            return _messageAction.GetMessagesUser(chatId,dateTime);
        }
        [HttpPost("add")]
        public Message AddMessagesUser(MassegeDto messageDto)
        {
            return _messageAction.AddMessagesUser(messageDto);
        }
    }
}
