using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;

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

        [HttpGet]
        public IEnumerable<Chat> GetUserChats()
        {
            List<Chat> chats = null;
            chats = _dbService.Chats.ToList();
            foreach (var chat in chats)
                chat.ChatsList = _dbService.ChatsList.Where(x => x.Chat == chat).ToList();
            return chats;
        }

        [HttpPost("add")]
        public void AddChat(ChatAddDto dto)
        {
            List<Chat> chats1 = _dbService.ChatsList.Where(x => x.ChatUser.Id == dto.UserId1).Select(x => x.Chat).ToList();
            List<Chat> chats2 = _dbService.ChatsList.Where(x => x.ChatUser.Id == dto.UserId2).Select(x => x.Chat).ToList();
            var united = chats1.Intersect(chats2);
            if (united == null)
            {
                Chat chat = new Chat
                {
                    ChatId = Guid.NewGuid(),
                    ChatName = "",
                    LastData = DateTime.Today
                };
                _dbService.Chats.Add(chat);
                _dbService.SaveChanges();

                ChatsList chatlist1 = new ChatsList
                {
                    Id = Guid.NewGuid(),
                    ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId1),
                    Chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == chat.ChatId)
                };
                _dbService.ChatsList.Add(chatlist1);
                ChatsList chatlist2 = new ChatsList
                {
                    Id = Guid.NewGuid(),
                    ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId2),
                    Chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == chat.ChatId)
                };
                _dbService.ChatsList.Add(chatlist2);
                _dbService.SaveChanges();
            }
            return;
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
        [HttpGet("get")]
        public IEnumerable<Chat> GetUserChats(Guid UserId)
        {
            List<Chat> chats = new List<Chat>();
            chats.Union(_dbService.ChatsList.Where(x => x.ChatUser.Id == UserId).Select(x => x.Chat).ToList());
            return chats;
        }
        [HttpGet("getchatusers")]
        public IEnumerable<ChatUser> GetChatUsersChats(Guid ChatId)
        {
            List<ChatUser> chats = null;
            chats.Union(_dbService.ChatsList.Where(x => x.Chat.ChatId == ChatId).Select(x => x.ChatUser).ToList());
            return chats;
        }
        //[HttpPost("deletechat")]
        //public Chat DeleteChat(Guid ChatId)
        //{
            
        //    Chat chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == ChatId);
        //    chat.ChatsList.Add(new ChatsList
        //    {
        //        Id = Guid.NewGuid(),
        //        ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == UserId),
        //        Chat = chat
        //    });
        //    return chat;
        //}
    }
}
