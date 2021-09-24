using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Data.Dto
{
    public class MassegeDto
    {
        public Guid ChatId { get; set; }
        public string UserName { get; set; }
        public string Massege { get; set; }
        public DateTime CurrentTime { get; set; }
    }
}
