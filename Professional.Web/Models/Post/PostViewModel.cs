namespace Professional.Web.Models.Post
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Professional.Models;
    using Professional.Web.Areas.UserArea.Models.CreateItem;
    using Professional.Web.Infrastructure.Mappings;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Title { get; set; }

        [Display(Name = "Created on")]
        public DateTime DateCreated { get; set; }

        public string Content { get; set; }

        public string Creator { get; set; }

        public string CreatorID { get; set; }

        public EndorsementInputModel EndorseFunctionality { get; set; }

        [Display(Name = "Field of Expertise")]
        public string FieldName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(
                p => p.Creator, 
                options => options.MapFrom(c => c.Creator.FullName));
        }
    }
}