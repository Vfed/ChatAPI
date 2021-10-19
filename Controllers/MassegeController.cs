﻿using Microsoft.AspNetCore.Http;
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
            messages = _dbService.Messages.Where(x => x.Chat.ChatId == ChatId).ToList();
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
            Chat chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == messageDto.ChatId);
            if (chat.LastData > messageDto.CurrentTime)
            {
                chat.LastData = messageDto.CurrentTime;
            }
            _dbService.Chats.Update(chat);
            _dbService.SaveChanges();
            return message;
        }
    }
}
