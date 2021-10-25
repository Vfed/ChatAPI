using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;

namespace ChatAPI.Servises.Abstract
{
    public interface IChatUserAction
    {
        public void AddChatUser(ChatUserDto dto);
        IEnumerable<ChatUser> FindAllExeptMeUser(ChatUserDto userDto);
        public ChatUser FindUser(string username);
        public IEnumerable<ChatUser> GetChatUser();
    }
}
