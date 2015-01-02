namespace Professional.Models
{
    using System;
    using System.Collections.Generic;

    using Professional.Data.Contracts;

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
