using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Linq.Expressions;

namespace Professional.Web.Models
{
    public class UserSimpleViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public ICollection<string> FieldList { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            // TODO: Try to get aroud using string concatenation - use model GetFullName method
            configuration.CreateMap<User, UserSimpleViewModel>()
               .ForMember(p => p.FullName,
               options => options.MapFrom(u => u.FirstName + " " + u.LastName));

             configuration.CreateMap<User, UserSimpleViewModel>()
                .ForMember(p => p.FieldList,
                options => options.MapFrom(u => u.FieldsOfExpertise.Select(f => f.Name)));
        }
    }
}