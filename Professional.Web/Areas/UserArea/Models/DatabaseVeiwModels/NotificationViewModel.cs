using AutoMapper;
using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models.DatabaseVeiwModels
{
    public class NotificationViewModel : IMapFrom<Notification>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual string CreatorID { get; set; }
        public virtual string CreatorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Notification, NotificationViewModel>()
                .ForMember(p => p.CreatorName,
                options => options.MapFrom(u => u.Creator.FullName));
        }
    }
}