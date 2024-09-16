using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCHOOLING.Shared.Models
{
    public class OnlineUser
    {
        public string ConnectionId { get; set; }
        public long  UserId { get; set; }
        public string  Role { get; set; }
    }
}
