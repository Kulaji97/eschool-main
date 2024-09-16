using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCHOOLING.Shared.Models
{
    public class ChatUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsYou { get; set; }
        public bool IsOnline { get; set; }

    }
}
