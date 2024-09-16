using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCHOOLING.DataAccess.EntityModel
{
    public class TblChatMessage
    {
        public long Id { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public DateTime TimeSent { get; set; }
        public bool HasRecieverRed { get; set; }
        public string MessageText { get; set; }

        //public virtual TblUserRegistration? FromUser { get; set; }
        //public virtual TblUserRegistration? ToUser { get; set; }
    }
}
