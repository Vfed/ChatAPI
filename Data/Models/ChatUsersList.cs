using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Data.Models
{
    public class ChatUsersList
    {
        [Key] public Guid Id { get; set; }
        public Guid ChatUser { get; set; }
        public Guid ChatId { get; set; }

    }
}
