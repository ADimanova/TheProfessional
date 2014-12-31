using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Models.DatabaseViewModels
{
    public class FieldViewModel : IMapFrom<FieldOfExpertise>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }

        public ICollection<string> Holders;

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<FieldOfExpertise, FieldViewModel>()
                .ForMember(p => p.Holders,
                options => options.MapFrom(u => u.Holders.Select(h => h.FirstName + " " + h.LastName)));
        }
    }
}
