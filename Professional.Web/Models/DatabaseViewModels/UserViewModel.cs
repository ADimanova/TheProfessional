using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Models.DatabaseViewModels
{
    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
        }
    }
}