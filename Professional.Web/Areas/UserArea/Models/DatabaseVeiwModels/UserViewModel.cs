using AutoMapper;
using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models
{
    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        private const string DefaultHistory = "No personal history has been added.";
        public string ID { get; set; }
        public string FullName { get; set; }

        [UIHint("MaleFemale")]
        public bool? IsMale { get; set; }
        public string PersonalHistory { get; set; }

        [UIHint("FormatedDate")]
        public DateTime DateOfBirth { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(p => p.FullName,
                options => options.MapFrom(u => u.FirstName + " " + u.LastName));
        }

        public string GetPersonalHistory()
        {
            if (this.PersonalHistory == null)
            {
                return DefaultHistory;
            }
            else
            {
                return this.PersonalHistory;
            }
        }
    }
}