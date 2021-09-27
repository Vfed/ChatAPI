using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Data.Dto
{
    public class ChatAddNewUserDto
    {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
    }
}
