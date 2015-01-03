using Professional.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Web.Infrastructure.Services.Contracts
{
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
