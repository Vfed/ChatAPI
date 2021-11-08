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
    public class ChatAction : IChatAction
    {
        private readonly DbService _dbService;
        public ChatAction(DbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<Chat> GetUserChats()
        {
            List<Chat> chats = null;
            chats = _dbService.Chats.Include(x => x.ChatsList).ToList();
            return chats;
        }
        public Chat AddChat(ChatAddDto dto)
        {
            Chat chat = null;
            List<Chat> chats1 = _dbService.ChatsList.Include(x => x.ChatUser).Include(x => x.Chat).Where(x => x.ChatUser.Id == dto.UserId1).Select(x => x.Chat).ToList();
            List<Chat> chats2 = _dbService.ChatsList.Include(x => x.ChatUser).Include(x => x.Chat).Where(x => x.ChatUser.Id == dto.UserId2).Select(x => x.Chat).ToList();
            bool chkiquel = false;
            foreach (Chat item1 in chats1)
            {
                foreach (Chat item2 in chats2)
                {
                    if (item1.ChatId == item2.ChatId)
                    {
                        chkiquel = true;
                        break;
                    }
                }
                if (chkiquel) break;
            }

            if (!chkiquel)
            {
                ChatUser user1 = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId1);
                ChatUser user2 = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId2);
                chat = new Chat
                {
                    ChatId = Guid.NewGuid(),
                    ChatName = user1.Username + " / " + user2.Username,
                    LastData = DateTime.Today
                };
                _dbService.Chats.Add(chat);
                _dbService.SaveChanges();

                ChatsList chatlist1 = new ChatsList
                {
                    Id = Guid.NewGuid(),
                    ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == dto.UserId1),
                    Chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == chat.ChatId),
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
            }
            return chat;
        }
        public Chat AddUserToChatChat(Guid chatId, Guid userId)
        {
            Chat chat = _dbService.Chats.FirstOrDefault(x => x.ChatId == chatId);
            chat.ChatsList.Add(new ChatsList
            {
                Id = Guid.NewGuid(),
                ChatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Id == userId),
                Chat = chat
            });
            return chat;
        }
        public List<Chat> GetUserChats(Guid userId)
        {
            List<Chat> chats = _dbService.ChatsList.Where(x => x.ChatUser.Id == userId).Select(x => x.Chat).ToList();
            return chats;
        }
        public IEnumerable<ChatUser> GetChatUsersChats(Guid chatId)
        {
            List<ChatUser> chats =  _dbService.ChatsList.Where(x => x.Chat.ChatId == chatId).Select(x => x.ChatUser).ToList();
            return chats;
        }
        //Change Chat name
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
    }
}
