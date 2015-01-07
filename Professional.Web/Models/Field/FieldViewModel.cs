namespace Professional.Web.Models.Field
{
    using System.Collections.Generic;
    using System.Linq;

    using Professional.Models;
    using Professional.Web.Infrastructure.Mappings;
    using Professional.Web.Models.Shared;

    public class FieldViewModel : IMapFrom<FieldOfExpertise>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string FieldInfo { get; set; }

        public int Rank { get; set; }

        public ICollection<NavigationItem> Featured { get; set; }
    }
}
