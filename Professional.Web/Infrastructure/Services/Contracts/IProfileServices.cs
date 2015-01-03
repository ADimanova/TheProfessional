namespace Professional.Web.Infrastructure.Services.Contracts
{
    using System.Linq;

    using Professional.Models;

    public interface IProfileServices
    {
        IQueryable<Post> GetAllPosts();

        IQueryable<Post> GetTopPosts(string currentUserId);

        IQueryable<Post> GetRecentPosts(string currentUserId);

        IQueryable<Post> GetFilteredPosts(string query, string condition);

        IQueryable<FieldOfExpertise> GetUserFields(string currentUserId);

        bool IsEndorsed(string userId, string loggedUserId);

        IQueryable<EndorsementOfUser> GetUserEndorsements(string currentUserId);

        bool IsConnected(string userId, string loggedUserId);

        IQueryable<Message> GetUserMessages(string currentUserId);

        IQueryable<Connection> GetUserConnectionRequests(string currentUserId);

        IQueryable<Notification> GetUserNotifications(string currentUserId);
    }
}
