using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Data.Models;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly DbService _dbService;
        public ChatController(DbService dbService)
        {
            _dbService = dbService;
        }
        [HttpPost("add")]
        public Chat AddChat(Guid UserWriterId, Guid UserReaderId)
        {
            Chat chat = new Chat
            {
                ChatId = Guid.NewGuid(),
                ChatName = "",
                LastData = DateTime.Today
            };
            chat.ChatsList.Add(new ChatsList
            {
                Id = Guid.NewGuid(),
                ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == UserWriterId),
                Chat = chat
            });
            chat.ChatsList.Add(new ChatsList
            {
                Id = Guid.NewGuid(),
                ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == UserReaderId),
                Chat = chat
            });
            _dbService.Chats.Add(chat);
            _dbService.SaveChanges();
            return chat;
        }
        [HttpPost("adduser")]
        public Chat AddUserToChatChat(Guid ChatId, Guid UserId)
        {

            Chat chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == ChatId);
            chat.ChatsList.Add(new ChatsList
            {
                Id = Guid.NewGuid(),
                ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == UserId),
                Chat = chat
            });
            return chat;
        }
        [HttpPost("get")]
        public IEnumerable<Chat> GetUserChats(Guid UserId)
        {
            List<Chat> chats = new List<Chat>();
            chats.Union(_dbService.ChatsList.Where(x => x.ChatUser.Id == UserId).Select(x => x.Chat).ToList());
            return chats;
        }
    }
}
