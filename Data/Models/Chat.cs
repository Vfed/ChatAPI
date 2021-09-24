using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Data.Models
{
    public class Chat
    {
        [Key] public Guid ChatId { get; set; }
        public string ChatName { get; set; }
        public DateTime LastData { get; set; }
        public List<ChatsList> ChatsList { get; set; }
        public List<Message> Messages { get; set; }
    }
}
