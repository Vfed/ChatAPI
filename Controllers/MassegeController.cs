using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChatAPI.Data.Dto;
using ChatAPI.Data.Models;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MassegeController : ControllerBase
    {
        private readonly DbService _dbService;
        public MassegeController(DbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet("get")]
        public IEnumerable<Message> GetMessagesUser(Guid ChatId, DateTime dateTime)
        {
            List<Message> messages = new List<Message>();
            messages.Union(_dbService.Messages.Where(x => x.Chat.ChatId == ChatId));

            return messages;
        }
        [HttpPost("add")]
        public Message AddMessagesUser(MassegeDto messageDto)
        {
            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                Chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == messageDto.ChatId),
                Massege = messageDto.Massege,
                CurrentTime = messageDto.CurrentTime,
                UserName = messageDto.UserName
            };
            _dbService.Messages.Add(message);
            _dbService.SaveChanges();
            return message;
        }
    }
}
