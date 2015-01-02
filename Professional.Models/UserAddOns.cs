namespace Professional.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        [NotMapped]
        public string FullName 
        { 
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
    }
}
