namespace Professional.Web.Areas.UserArea.Models.CreateItem
{
    using AutoMapper;
    using Professional.Models;
    using Professional.Web.Infrastructure.Mappings;

    public class PostEndorsementInputModel : EndorsementInputModel, IMapFrom<EndorsementOfPost>, IHaveCustomMappings
    {
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PostEndorsementInputModel, EndorsementOfPost>()
                .ForMember(p => p.EndorsedPostID, options => options.MapFrom(u => u.EndorsedID));
        }
    }
}