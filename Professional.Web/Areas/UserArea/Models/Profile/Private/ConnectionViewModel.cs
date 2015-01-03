namespace Professional.Web.Areas.UserArea.Models.Profile.Private
{
    using AutoMapper;

    using Professional.Models;
    using Professional.Web.Infrastructure.Mappings;

    public class ConnectionViewModel : IMapFrom<Connection>, IHaveCustomMappings
    {
        public int ID { get; set; }

        public string ConnectionUserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Connection, ConnectionViewModel>()
                .ForMember(
                p => p.ConnectionUserName,
                options => options.MapFrom(u => u.FirstUser.FirstName + " " + u.FirstUser.LastName));
        }
    }
}