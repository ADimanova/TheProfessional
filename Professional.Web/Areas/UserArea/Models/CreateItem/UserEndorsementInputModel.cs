using AutoMapper;
using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using Professional.Web.Models.InputViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models.CreateItem
{
    public class UserEndorsementInputModel : EndorsementInputModel, IMapFrom<EndorsementOfUser>, IHaveCustomMappings
    {
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<UserEndorsementInputModel, EndorsementOfUser>()
                .ForMember(p => p.EndorsedUserID,
                options => options.MapFrom(u => u.EndorsedID));
        }
    }
}