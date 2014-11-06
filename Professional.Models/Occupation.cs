using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public class Occupation
    {
        private ICollection<User> holders;

        public Occupation()
        {
            this.holders = new HashSet<User>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<User> Holders
        {
            get { return this.holders; }
            set { this.holders = value; }
        }
    }
}
