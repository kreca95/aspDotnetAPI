using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dealer_router
{
    public class Message
    {
        public string Nick { get; set; }
        public string MessageContent { get; set; }

        public Message(string nick,string message)
        {
            this.Nick = nick;
            this.MessageContent = message;
        }

        public override string ToString()
        {
            return this.Nick + "," + this.MessageContent;
        }
    }
}
