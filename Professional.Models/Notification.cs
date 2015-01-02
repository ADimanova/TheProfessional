namespace Professional.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Professional.Data.Contracts;

    public class Notification : AuditInfo, IDeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public NotificationType ReceiverType { get; set; }

        [Required]
        public string CreatorID { get; set; }

        public virtual User Creator { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
