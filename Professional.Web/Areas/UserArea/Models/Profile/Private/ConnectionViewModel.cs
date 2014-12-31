using AutoMapper;
using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Areas.UserArea.Models.DatabaseVeiwModels
{
    public class ConnectionViewModel : IMapFrom<Connection>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string ConnectionUserName { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Connection, ConnectionViewModel>()
                .ForMember(p => p.ConnectionUserName,
                options => options.MapFrom(u => u.FirstUser.FirstName + " " + u.FirstUser.LastName));
        }
    }
}