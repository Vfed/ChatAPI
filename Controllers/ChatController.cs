using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;
using Microsoft.EntityFrameworkCore;

using ChatAPI.Servises.Abstract;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatAction _chatAction;
        public ChatController(IChatAction chatAction)
        {
            _chatAction = chatAction;
        }

        [HttpGet]
        public IEnumerable<Chat> GetUserChats()
        {
            return _chatAction.GetUserChats();
        }

        [HttpPost("add")]
        public Chat AddChat(ChatAddDto dto)
        {
            return _chatAction.AddChat(dto);
        }

        [HttpPost("adduser")]
        public Chat AddUserToChatChat(Guid chatId, Guid userId)
        {
            return _chatAction.AddUserToChatChat(chatId,userId);
        }

        [HttpGet("get")]
        public IEnumerable<Chat> GetUserChats(Guid userId)
        {
            return GetUserChats(userId);
        }

        [HttpGet("getchatusers")]
        public IEnumerable<ChatUser> GetChatUsersChats(Guid chatId)
        {
            return _chatAction.GetChatUsersChats(chatId);
        }

        //Change Chat name
        [HttpPost("chatname")]
        public void SetChatName(ChatChangeNameDto dto)
        {
            _chatAction.SetChatName(dto);
            return;
        }
    }
}
