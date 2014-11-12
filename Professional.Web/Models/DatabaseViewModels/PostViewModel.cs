using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Professional.Web.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Professional.Web.Models
{
    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [Display(Name = "Created on")]
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public string Creator { get; set; }
        public string CreatorID { get; set; }

        [Display(Name = "Field of Expertise")]
        public string FieldName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(p => p.Creator, 
                options => options.MapFrom(c => c.Creator.FirstName + " " +  c.Creator.LastName));
        }
    }
}