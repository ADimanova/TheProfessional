using Professional.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public class Image : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string FileExtension { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
