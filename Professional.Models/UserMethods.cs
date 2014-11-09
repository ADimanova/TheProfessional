using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public partial class User
    {
        public string GetFullName()
        {
            // TODO: Add middle name; chack/fix casing
            var fullName = string.Format("{0} {1}", this.FirstName, this.LastName);
            return fullName;
        }
    }
}
