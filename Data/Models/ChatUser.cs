using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Data.Models
{
    [Serializable]
    public class ChatUser
    {
        [Key] public Guid Id { get; set; }
        public string Username { get; set; }
        public List<ChatsList> Chats { get; set; }
    }
}
