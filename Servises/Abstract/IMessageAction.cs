using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;

namespace ChatAPI.Servises.Abstract
{
    public interface IMessageAction
    {
        public IEnumerable<Message> GetMessagesUser(Guid chatId, DateTime dateTime);
        public Message AddMessagesUser(MassegeDto messageDto);
    }
}
