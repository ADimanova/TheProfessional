namespace Professional.Web.Areas.UserArea.Models.CreateItem
{
    using AutoMapper;
    using Professional.Models;
    using Professional.Web.Infrastructure.Mappings;

    public class UserEndorsementInputModel : EndorsementInputModel, IMapFrom<EndorsementOfUser>, IHaveCustomMappings
    {
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<UserEndorsementInputModel, EndorsementOfUser>()
               .ForMember(p => p.EndorsedUserID, options => options.MapFrom(u => u.EndorsedID));
        }
    }
}