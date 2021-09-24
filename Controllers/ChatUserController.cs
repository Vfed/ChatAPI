using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;


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
            return _dbService.ChatUsers.ToList();
        }
        [HttpGet("serial")]
        public string SerializeChatUser<T>(T text)
        {
            string value = JsonSerializer.Serialize(text);
            return value;
        }

        [HttpPost("add")]
        public IEnumerable<ChatUser> AddChatUser(ChatUserDto dto)
        {
            ChatUser newUser;
            var user = _dbService.ChatUsers
                .FirstOrDefault(x => x.Username == dto.Username);
            if (user == null) 
            {
                newUser = new ChatUser
                {
                    Id = Guid.NewGuid(),
                    Username = dto.Username
                };

                _dbService.ChatUsers.Add(newUser);
                _dbService.SaveChanges();
            }
            return _dbService.ChatUsers.ToList();
        }
        [HttpGet("find")]
        public string FindUser([FromQuery] string Username)
        {
            var user = _dbService.ChatUsers
                .FirstOrDefault(x => x.Username == Username);
            return SerializeChatUser(user);
        }

    }
}
