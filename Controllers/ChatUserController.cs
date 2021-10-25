﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;

using ChatAPI.Servises.Abstract;
using ChatAPI.Servises.Specific;

using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatUserController : ControllerBase
    {
        private readonly IChatUserAction _chatUserAction;
        public ChatUserController(IChatUserAction chatUserAction)
        {
            _chatUserAction = chatUserAction;
        }
        [HttpGet]
        public IEnumerable<ChatUser> GetChatUser()
        {
            return _chatUserAction.GetChatUser();
        }
        [HttpGet("getallexeptme")]
        public IEnumerable<ChatUser> FindAllExeptMeUser([FromQuery]  ChatUserDto userDto)
        {
            return _chatUserAction.FindAllExeptMeUser(userDto);
        }

        [HttpPost("add")]
        public void AddChatUser(ChatUserDto dto)
        {
            _chatUserAction.AddChatUser(dto);
            return;
        }
        [HttpGet("find")]
        public ChatUser FindUser([FromQuery] string Username)
        {
            return _chatUserAction.FindUser(Username);
        }
        

    }
}
