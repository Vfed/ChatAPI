using Microsoft.AspNetCore.Http;
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
using Microsoft.AspNetCore.Authorization;

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

        //[Authorize]

        [HttpGet]
        public IEnumerable<ChatUser> GetChatUser()
        {
            return _chatUserAction.GetChatUser();
        }

        //

        //Check if user( username ) exists - result : bool
        
        [HttpGet("checkuser")]
        public bool CheckUserExistens(string username)
        {
            return _chatUserAction.CheckUserExistens(username);
        }

        //

        //[Authorize]
        
        [HttpGet("getallexeptme")]
        public IEnumerable<ChatUser> FindAllExeptMeUser([FromQuery]  ChatUserDto userDto)
        {
            return _chatUserAction.FindAllExeptMeUser(userDto);
        }

        //

        //Create New ChatUser via ChatUserLoginDto{username, password} 

        [HttpPost("add")]
        public void AddChatUser(ChatUserLoginDto dto)
        {
            _chatUserAction.AddChatUser(dto);
            return;
        }

        //

        [HttpPost("find")]
        public ChatUser FindUser(ChatUserDto dto)
        {
            return _chatUserAction.FindUser(dto);
        }

        //Login user via ChatUserLoginDto give him TokenResult
        [HttpPost("login")]
        public IActionResult Token(ChatUserLoginDto dto)
        {
            var responce =  _chatUserAction.Login(dto);
            if (responce == null)
            {
                return null;
            }
            else 
            {
                return Ok(responce);
            }
        }
        //
    }
}