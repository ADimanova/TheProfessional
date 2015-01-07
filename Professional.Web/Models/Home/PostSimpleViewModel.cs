namespace Professional.Web.Models.Home
{
    using Professional.Models;
    using Professional.Web.Infrastructure.Mappings;

    public class PostSimpleViewModel : IMapFrom<Post>
    {
        public int ID { get; set; }

        public string ShortTitle { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }
    }
}