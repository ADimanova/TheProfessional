namespace Professional.Web.Infrastructure.Services
{
    using System;
    using System.Linq;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Helpers;
    using Professional.Web.Infrastructure.Caching;
    using Professional.Web.Infrastructure.Services.Base;
    using Professional.Web.Infrastructure.Services.Contracts;

    public class ProfileServices : BaseServices, IProfileServices
    {
        public ProfileServices(IApplicationData data, ICacheService cache)
            : base(data, cache)
        {
        }

        public IQueryable<Post> GetAllPosts()
        {
            var post = this.Data.Posts.All()
                .OrderBy(p => p.Title);

            return post;
        }

        // Public 
        public IQueryable<Post> GetTopPosts(string currentUserId)
        {
            var topPost = this.Data.Posts.All()
                .Where(p => p.CreatorID == currentUserId)
                .OrderBy(p => p.Rank)
                .Take(WebConstants.ListPanelCount);

            return topPost;
        }

        public IQueryable<Post> GetRecentPosts(string currentUserId)
        {
            var recentPost = this.Data.Posts.All()
                .Where(p => p.CreatorID == currentUserId)
                .OrderBy(p => p.CreatedOn)
                .Take(WebConstants.ListPanelCount);

            return recentPost;
        }

        public IQueryable<Post> GetFilteredPosts(string query, string condition)
        {
            var posts = this.GetAllPosts();

            if (query != "All")
            {
                posts = posts
                .Where(p => p.Field.Name == query);
            }

            if (condition == "Recent")
            {
                posts = posts
                .OrderBy(p => p.CreatedOn);
            }
            else if (condition == "Top")
            {
                posts = posts
                .OrderBy(p => p.Rank);
            }
            else
            {
                throw new ArgumentException("The passed condition is not supported");
            }

            return posts;
        }

        public IQueryable<FieldOfExpertise> GetUserFields(string currentUserId)
        {
            var fields = this.Data.Users.All()
            .FirstOrDefault(u => u.Id == currentUserId)
            .FieldsOfExpertise.AsQueryable();

            return fields;
        }
        
        public bool IsEndorsed(string userId, string loggedUserId)
        {
            var isEndorsed = this.Data.EndorsementsOfUsers.All()
            .Where(e => e.EndorsingUserID == loggedUserId)
            .Any(e => e.EndorsedUserID == userId);

            return isEndorsed;
        }

        public IQueryable<EndorsementOfUser> GetUserEndorsements(string currentUserId)
        {
            var endorsements = this.Data.EndorsementsOfUsers.All()
                .Where(e => e.EndorsedUserID == currentUserId);

            return endorsements;
        }

        public Connection GetConnection(string userId, string loggedUserId)
        {
            var connection = this.Data.Connections.All()
                .FirstOrDefault(c => ((c.FirstUserId == userId || c.SecondUserId == userId) &&
                (c.FirstUserId == loggedUserId || c.SecondUserId == loggedUserId) &&
                userId != loggedUserId));

            return connection;
        }

        // Private
        public IQueryable<Message> GetUserMessages(string currentUserId)
        {
            var messages = this.Data.Messages.All()
                .Where(m => m.ToUserId == currentUserId)
                .OrderByDescending(n => n.CreatedOn);

            return messages;
        }

        public IQueryable<Connection> GetUserConnectionRequests(string currentUserId)
        {
            var requests = this.Data.Connections.All()
                .Where(c => c.SecondUserId == currentUserId)
                .Where(c => !c.IsAccepted)
                .OrderByDescending(n => n.CreatedOn);

            return requests;
        }

        public IQueryable<Notification> GetUserNotifications(string currentUserId)
        {
            var notifications = this.Data.Notifications.All()
                .OrderByDescending(n => n.CreatedOn);

            return notifications;
        }
    }
}