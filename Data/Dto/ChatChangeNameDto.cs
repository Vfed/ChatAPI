using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Data.Dto
{
    public class ChatChangeNameDto
    {
        public string ChatName { get; set; }
        public Guid ChatId { get; set; }
    }
}
