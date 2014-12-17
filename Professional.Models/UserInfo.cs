using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public partial class User
    {
        private ICollection<Message> messages;

        public virtual ICollection<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }
    }
}
