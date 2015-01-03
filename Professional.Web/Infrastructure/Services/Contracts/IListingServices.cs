namespace Professional.Web.Infrastructure.Services.Contracts
{
    using System.Linq;

    using Professional.Models;

    public interface IListingServices
    {
        IQueryable<User> GetUsers(string filter, string user);

        IQueryable<Post> GetPosts(string filter, string user);

        IQueryable<EndorsementOfUser> GetEndorsements(string userID);

        IQueryable<string> GetLetters();

        IQueryable<string> GetFeilds();
    }
}