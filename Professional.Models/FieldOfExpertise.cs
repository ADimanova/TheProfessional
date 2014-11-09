using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public class FieldOfExpertise
    {
        private ICollection<User> holders;
        public FieldOfExpertise()
		{
            this.holders = new HashSet<User>();
		}
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Rank { get; set; }

        public virtual ICollection<User> Holders
        {
            get { return this.holders; }
            set { this.holders = value; }
        }
    }
}
