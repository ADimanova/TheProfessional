namespace Professional.Web.Infrastructure.Services.Contracts
{
    using System.Linq;

    using Professional.Models;

    public interface IHomeServices
    {
        IQueryable<FieldOfExpertise> GetFields();

        IQueryable<Post> GetTopPosts();

        IQueryable<User> GetFeatured();
    }
}
