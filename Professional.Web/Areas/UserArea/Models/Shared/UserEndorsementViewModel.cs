﻿using AutoMapper;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models.ListingViewModels;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models.Profile.Public
{
    public class UserEndorsementViewModel : EndorsementViewModel, IMapFrom<EndorsementOfUser>, IHaveCustomMappings
    {
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<EndorsementOfUser, UserEndorsementViewModel>()
               .ForMember(p => p.EndorsingUserName,
               options => options.MapFrom(u => 
                   u.EndorsingUser.FirstName + " " + u.EndorsingUser.LastName));
        }
    }
}