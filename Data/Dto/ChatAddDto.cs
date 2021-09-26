using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Data.Dto
{
    public class ChatAddDto
    {
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }
    }
}
