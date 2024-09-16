using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCHOOLING.Shared.Models
{
    public class ChatMessage
    {
        public long Id { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public DateTime TimeSent { get; set; }
        public bool HasRecieverRed { get; set; }
        public string MessageText { get; set; }
    }
}
