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
    public class MessageAction : IMessageAction
    {
        private readonly DbService _dbService;
        public MessageAction(DbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<Chat> GetUserChats()
        {
            List<Chat> chats = null;
            chats = _dbService.Chats.Include(x => x.ChatsList).ToList();
            return chats;
        }
        public IEnumerable<Message> GetMessagesUser(Guid chatId, DateTime dateTime)
        {
            List<Message> messages = new List<Message>();
            messages = _dbService.Messages.Where(x => x.Chat.ChatId == chatId).ToList();
            return messages;
        }
        public Message AddMessagesUser(MassegeDto messageDto)
        {
            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                Chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == messageDto.ChatId),
                Massege = messageDto.Massege,
                CurrentTime = messageDto.CurrentTime,
                UserName = messageDto.UserName
            };
            _dbService.Messages.Add(message);
            Chat chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == messageDto.ChatId);
            if (chat.LastData > messageDto.CurrentTime)
            {
                chat.LastData = messageDto.CurrentTime;
            }
            _dbService.Chats.Update(chat);
            _dbService.SaveChanges();
            return message;
        }
    }
}
