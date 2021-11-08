using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;

namespace ChatAPI.Servises.Abstract
{
    public interface IChatAction
    {
        public IEnumerable<Chat> GetUserChats();
        public Chat AddChat(ChatAddDto dto);
        public Chat AddUserToChatChat(Guid chatId, Guid userId);
        public List<Chat> GetUserChats(Guid userId);
        public IEnumerable<ChatUser> GetChatUsersChats(Guid chatId);
        public void SetChatName(ChatChangeNameDto dto);
    }
}
