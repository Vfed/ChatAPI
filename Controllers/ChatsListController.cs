﻿using Microsoft.AspNetCore.Http;
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
    public class ChatsListController : ControllerBase
    {
        private readonly DbService _dbService;
        public ChatsListController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IEnumerable<ChatsList> GetUserChats()
        {
            List<ChatsList> chats = new List<ChatsList>();
            chats = _dbService.ChatsList.ToList();
            return chats;
        }
        [HttpGet("gettime")]
        public DateTime GetCurrentDataChats([FromQuery]Guid userId,Guid chatId)
        {
            DateTime date = new DateTime();
            date = _dbService.ChatsList.FirstOrDefault(x => x.Chat.ChatId == chatId && x.ChatUser.Id == userId).Current;
            return date;
        }
        [HttpPost("add")]
        public void AddUserToChat(ChatAddNewUserDto dto)
        {
            ChatsList chatlist = new ChatsList();

            chatlist = _dbService.ChatsList.FirstOrDefault(x => x.ChatUser.Id == dto.UserId && x.Chat.ChatId == dto.ChatId);

            if (chatlist != null)
            {
                return;
            }
            else 
            {
                chatlist = new ChatsList()
                {
                    Id = Guid.NewGuid(),
                    Chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == dto.ChatId),
                    ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId),
                    Current = DateTime.Now
                };
                _dbService.ChatsList.Add(chatlist);
                _dbService.SaveChanges();

                ChatUser chatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId);
                chatUser.ChatLists.Add(chatlist);
                _dbService.ChatUsers.Update(chatUser);
                _dbService.SaveChanges();
            }
            //{
            //    ChatUser user = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId);

            //    if (user.ChatLists == null)
            //    {
            //        user.ChatLists = new List<ChatsList>();

            //        _dbService.ChatUsers.Update(user);
            //        _dbService.SaveChanges();
            //    }

            //    chatlist = new ChatsList()
            //    {
            //        Id = Guid.NewGuid(),
            //        Chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == dto.ChatId),
            //        ChatUser = user,
            //        Current = DateTime.Now
            //    };
            //    _dbService.ChatsList.Add(chatlist);
            //    _dbService.SaveChanges();
            //    user.ChatLists.Add(chatlist);
            //    _dbService.ChatUsers.Update(user);
            //    _dbService.SaveChanges();
            //}
            return;
        }

    }
}