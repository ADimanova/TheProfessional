using AutoMapper;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models.ListingViewModels;
using Professional.Web.Infrastructure.Mappings;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models
{
    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string ID { get; set; }
        public bool IsPrivate { get; set; }
        public string FullName { get; set; }

        [UIHint("MaleFemale")]
        public bool? IsMale { get; set; }
        public string PersonalHistory { get; set; }

        [UIHint("FormatedDate")]
        public DateTime DateOfBirth { get; set; }

        public int? ProfileImageId { get; set; }
        public string ProfileImageUrl { get; set; }
        public IEnumerable<string> Occupations { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public IEnumerable<EndorsementViewModel> Endorsements { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(p => p.FullName,
                options => options.MapFrom(u => u.FirstName + " " + u.LastName));

            configuration.CreateMap<User, UserViewModel>()
                .ForMember(p => p.Occupations,
                options => options.MapFrom(u => u.Occupations.Select(o => o.Title)));

            configuration.CreateMap<User, UserViewModel>()
                .ForMember(p => p.Fields,
                options => options.MapFrom(u => u.FieldsOfExpertise.Select(o => o.Name)));

            configuration.CreateMap<User, UserViewModel>()
                .ForMember(p => p.Endorsements,
                    options => options.MapFrom(u => u.UsersEndorsements.Select(e => e.Comment
                    )));
        }
    }
}