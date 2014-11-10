using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Professional.Web.Models
{
    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public String Creator { get; set; }
        public string FieldName { get; set; }
        public IEnumerable<string> Fields { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(p => p.Creator, 
                options => options.MapFrom(c => c.Creator.FirstName + " " +  c.Creator.LastName));
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(p => p.Fields,
                options => options.MapFrom(c => c.Creator
                    .FieldsOfExpertise
                    .Select(f => f.Name)
                    .ToList()));
        }
    }
}