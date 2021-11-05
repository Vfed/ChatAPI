using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Servises.Abstract;
using ChatAPI.Data.Dto;
using ChatAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Servises.Specific
{
    public class ChatsListAction : IChatsListAction
    {
        private readonly DbService _dbService;
        public ChatsListAction(DbService dbService)
        {
            _dbService = dbService;
        }
        public IEnumerable<ChatsList> GetUserChats()
        {
            List<ChatsList> chats = _dbService.ChatsList.ToList();
            return chats;
        }
        public DateTime GetCurrentDataChats(Guid userId, Guid chatId)
        {
            DateTime date = new DateTime();
            date = _dbService.ChatsList.FirstOrDefault(x => x.Chat.ChatId == chatId && x.ChatUser.Id == userId).Current;
            return date;
        }
    }
}
