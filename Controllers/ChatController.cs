using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;
using Microsoft.EntityFrameworkCore;

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
            chats = _dbService.Chats.Include(x => x.ChatsList).ToList();
            return chats;
        }

        [HttpPost("add")]
        public Chat AddChat(ChatAddDto dto)
        {
            Chat chat = null;
            
            
            
                ChatUser user1 = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId1);
                ChatUser user2 = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId2);
                chat = new Chat
                {
                    ChatId = Guid.NewGuid(),
                    ChatName = user1.Username + " / " + user2.Username,
                    LastData = DateTime.Now
                };
                _dbService.Chats.Add(chat);
                _dbService.SaveChanges();

                ChatsList chatlist1 = new ChatsList
                {
                    Id = Guid.NewGuid(),
                    ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId1),
                    Chat = _dbService.Chats.FirstOrDefault(x=> x.ChatId == chat.ChatId),
                    Current = DateTime.Now
                };
                _dbService.ChatsList.Add(chatlist1);
                ChatsList chatlist2 = new ChatsList
                {
                    Id = Guid.NewGuid(),
                    ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId2),
                    Chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == chat.ChatId),
                    Current = DateTime.Now
                };
                if (user1.ChatLists == null) 
                {
                    user1.ChatLists = new List<ChatsList>();
                }
                if (user2.ChatLists == null)
                {
                    user2.ChatLists = new List<ChatsList>();
                }
                _dbService.ChatUsers.Update(user1);
                _dbService.ChatUsers.Update(user2);
                _dbService.SaveChanges();

                user1.ChatLists.Add(chatlist1);
                user2.ChatLists.Add(chatlist2);

                _dbService.ChatsList.Add(chatlist2);
                _dbService.ChatUsers.Update(user1);
                _dbService.ChatUsers.Update(user2);
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
        [HttpGet("get")]
        public IEnumerable<Chat> GetUserChats(Guid UserId)
        {
            List<Chat> chats = new List<Chat>();
            chats = _dbService.ChatsList.Where(x => x.ChatUser.Id == UserId).Select(x => x.Chat).ToList();
            return chats;
        }
        [HttpGet("getchatusers")]
        public IEnumerable<ChatUser> GetChatUsersChats(Guid ChatId)
        {
            List<ChatUser> chats = null;
            chats= _dbService.ChatsList.Where(x => x.Chat.ChatId == ChatId).Select(x => x.ChatUser).ToList();
            return chats;
        }
        //Change Chat name
        [HttpPost("chatname")]
        public void SetChatName(ChatChangeNameDto dto)
        {
            if (dto.ChatName.Length > 5)
            {
                Chat chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == dto.ChatId);
                chat.ChatName = dto.ChatName;
                _dbService.Chats.Update(chat);
                _dbService.SaveChanges();
            }
            return;
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
