namespace Professional.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Professional.Data.Contracts;

    public class Message : AuditInfo, IDeletableEntity
    {
        public int ID { get; set; }

        public string FromUserId { get; set; }

        public virtual User FromUser { get; set; }

        public string ToUserId { get; set; }

        public virtual User ToUser { get; set; }

        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

        public bool IsRead { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
