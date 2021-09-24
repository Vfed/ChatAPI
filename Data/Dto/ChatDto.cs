using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Data.Dto
{
    public class ChatDto
    {
        public string ChatName { get; set; }
        public List<Guid> ChatUsersIds { get; set; }
        public DateTime LastData { get; set; }
    }
}
