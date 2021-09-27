using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatUserController : ControllerBase
    {
        private readonly DbService _dbService;
        public ChatUserController(DbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IEnumerable<ChatUser> GetChatUser()
        {
            return _dbService.ChatUsers.Include(x => x.ChatLists).ToList();
        }
        [HttpGet("getallexeptme")]
        public IEnumerable<ChatUser> FindAllExeptMeUser([FromQuery]  ChatUserDto userDto)
        {
            var user = _dbService.ChatUsers
                .Where(x => x.Username != userDto.Username);
            return user;
        }

        [HttpPost("add")]
        public void AddChatUser(ChatUserDto dto)
        {
            ChatUser newUser;
            var user = _dbService.ChatUsers
                .FirstOrDefault(x => x.Username == dto.Username);
            if (user == null) 
            {
                newUser = new ChatUser
                {
                    Id = Guid.NewGuid(),
                    Username = dto.Username,
                    ChatLists = new List<ChatsList>()
                };

                _dbService.ChatUsers.Add(newUser);
                _dbService.SaveChanges();
            }
            return;
        }
        [HttpGet("find")]
        public ChatUser FindUser([FromQuery] string Username)
        {
            var user = _dbService.ChatUsers
                .FirstOrDefault(x => x.Username == Username);
            return user;
        }
        

    }
}
