using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Professional.Web.Models.DatabaseViewModels
{
    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        //public DateTime DateOfBirth { get; set; }
        //public bool IsMale { get; set; }

        //[DataType("TextInput")]
        //[UIHint("TextArea")]
        //public string PersonalHistory { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
        }
    }
}