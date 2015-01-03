namespace Professional.Web.Models.Field
{
    using System.Collections.Generic;
    using System.Linq;

    using Professional.Models;
    using Professional.Web.Infrastructure.Mappings;

    public class FieldViewModel : IMapFrom<FieldOfExpertise>, IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Rank { get; set; }

        public ICollection<string> Holders { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<FieldOfExpertise, FieldViewModel>()
                .ForMember(
                p => p.Holders,
                options => options.MapFrom(u => u.Holders.Select(h => h.FirstName + " " + h.LastName)));
        }
    }
}
