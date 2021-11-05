using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Servises.Abstract;
using ChatAPI.Data.Dto;
using ChatAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Servises.Specific
{
    public class ChatUserAction : IChatUserAction
    {
        private readonly DbService _dbService;
        public ChatUserAction(DbService dbService)
        {
            _dbService = dbService;
        }

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
        public ChatUser FindUser(string username)
        {
            var user = _dbService.ChatUsers
                .FirstOrDefault(x => x.Username == username);
            return user;
        }
        public IEnumerable<ChatUser> FindAllExeptMeUser(ChatUserDto userDto)
        {
            var user = _dbService.ChatUsers
                .Where(x => x.Username != userDto.Username);
            return user;
        }
        public IEnumerable<ChatUser> GetChatUser()
        {
            return _dbService.ChatUsers.Include(x => x.ChatLists);
        }
    }
}
