using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public partial class User
    {
        public string FullName 
        { 
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
    }
}
