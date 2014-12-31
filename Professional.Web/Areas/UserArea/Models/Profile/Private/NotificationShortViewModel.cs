using AutoMapper;
using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models.DatabaseVeiwModels
{
    public class NotificationShortViewModel : IMapFrom<Notification>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Notification, NotificationShortViewModel>()
                .ForMember(p => p.Preview,
                options => options.MapFrom(u => u.Content.Substring(0, 20) + "..."));
        }
    }
}