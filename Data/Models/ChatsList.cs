using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Data.Models
{
    public class ChatsList
    {
        [Key] public Guid Id { get; set; }
        public ChatUser ChatUser { get; set; }
        public Chat Chat { get; set; }
    }
}
