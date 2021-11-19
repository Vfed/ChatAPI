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
        public List<ChatsList> ChatLists { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime LastActionTime { get; set; }

    }
}
