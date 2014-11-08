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
        public string Description { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
             // Can't use string.Format() with Linq - instead using string concatenation
            configuration.CreateMap<User, UserSimpleViewModel>()
               .ForMember(p => p.FullName,
               options => options.MapFrom(u => u.FirstName + " " + u.LastName));

             configuration.CreateMap<User, UserSimpleViewModel>()
                .ForMember(p => p.Description,
                options => options.MapFrom(u => "added description"));
        }

        //public class CustomFullNameResolver : ValueResolver<User, string>
        //{
        //    protected override string ResolveCore(User source)
        //    {
        //        var fullName = string.Format("{0} {1}", source.FirstName, source.LastName);
        //        return fullName;
        //    }
        //}
    }
}