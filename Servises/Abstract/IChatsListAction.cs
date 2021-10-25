using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;

namespace ChatAPI.Servises.Abstract
{
    public interface IChatsListAction
    {
        public IEnumerable<ChatsList> GetUserChats();
        public DateTime GetCurrentDataChats(Guid userId, Guid chatId);
    }
}
