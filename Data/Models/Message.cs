using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Data.Models
{
    public class Message
    {
        [Key]public Guid Id { get; set; }
        public Chat Chat { get; set; }
        public string UserName { get; set; }
        public string Massege { get; set; }
        public DateTime CurrentTime { get; set; }
    }
}
