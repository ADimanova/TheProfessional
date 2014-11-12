using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;

namespace Professional.Web.Models
{
    public class UserSimpleViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string ID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Fields of Expertise")]
        public ICollection<string> FieldList { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            // TODO: Try to get around using string concatenation - use model GetFullName method
            configuration.CreateMap<User, UserSimpleViewModel>()
               .ForMember(p => p.FullName,
               options => options.MapFrom(u => u.FirstName + " " + u.LastName));

             configuration.CreateMap<User, UserSimpleViewModel>()
                .ForMember(p => p.FieldList,
                options => options.MapFrom(u => u.FieldsOfExpertise.Select(f => f.Name)));
        }
    }
}