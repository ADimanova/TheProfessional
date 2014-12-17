using Newtonsoft.Json;
using Professional.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public class Message: AuditInfo, IDeletableEntity
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
