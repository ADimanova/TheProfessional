namespace Professional.Web.Infrastructure.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Professional.Models;

    public interface IListingServices
    {
        IList<string> FirstLetters { get; }
        IQueryable<User> GetUsers(string filter, string user);

        IQueryable<Post> GetPosts(string filter, string user);

        IQueryable<EndorsementOfUser> GetEndorsements(string userID);

        IList<string> GetLetters<T>(string columnName) where T : class;

        IQueryable<string> GetFeilds();
    }
}