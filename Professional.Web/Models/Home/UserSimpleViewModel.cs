namespace Professional.Web.Models.Home
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;

    using Professional.Models;
    using Professional.Web.Infrastructure.Mappings;
    using Professional.Web.Helpers;

    // Used to display featured users on the home page
    public class UserSimpleViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string ID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Fields of Expertise")]
        public ICollection<string> FieldList { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserSimpleViewModel>()
               .ForMember(
               p => p.FullName,
               options => options.MapFrom(u => u.FirstName + " " + u.LastName));

             configuration.CreateMap<User, UserSimpleViewModel>()
                .ForMember(
                    p => p.FieldList,
                    options => options
                    .MapFrom(
                        u => u.FieldsOfExpertise
                        .Where(f => f.IsDeleted == false)
                        .Select(f => f.Name)));

            configuration.CreateMap<User, UserSimpleViewModel>()
                .ForMember(
                    p => p.ImageUrl,
                    options => options
                    .MapFrom(
                        u => u.ProfileImage == null ?
                            WebConstants.DefaultImage : 
                            WebConstants.GetImagePageRoute + u.ProfileImageId));
        }
    }
}