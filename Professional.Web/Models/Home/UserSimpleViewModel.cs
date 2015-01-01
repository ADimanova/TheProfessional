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
    // Used to display featured users on the home page
    public class UserSimpleViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string ID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Fields of Expertise")]
        public ICollection<string> FieldList { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserSimpleViewModel>()
               .ForMember(p => p.FullName,
               //options => options.MapFrom(u => u.FullName));
               options => options.MapFrom(u => u.FirstName + " " + u.LastName));

             configuration.CreateMap<User, UserSimpleViewModel>()
                .ForMember(p => p.FieldList,
                options => options
                    .MapFrom(u => u.FieldsOfExpertise
                        .Where(f => f.IsDeleted == false)
                        .Select(f => f.Name)));
        }
    }
}