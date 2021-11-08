using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;
using System.Security.Claims;

namespace ChatAPI.Servises.Abstract
{
    public interface IChatUserAction
    {
        public void AddChatUser(ChatUserLoginDto dto);
        IEnumerable<ChatUser> FindAllExeptMeUser(ChatUserDto userDto);
        public ChatUser FindUser(ChatUserDto dto);
        public IEnumerable<ChatUser> GetChatUser();
        public bool CheckUserExistens(string username);
        public TokenReturn Login(ChatUserLoginDto dto);

    }
}
