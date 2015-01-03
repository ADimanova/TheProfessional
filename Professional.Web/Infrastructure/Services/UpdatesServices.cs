namespace Professional.Web.Infrastructure.Services
{
    using System.Linq;
    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Infrastructure.Caching;
    using Professional.Web.Infrastructure.Services.Base;
    using Professional.Web.Infrastructure.Services.Contracts;

    public class UpdatesServices : BaseServices, IUpdatesServices
    {
        public UpdatesServices(IApplicationData data, ICacheService cache)
            : base(data, cache)
        {
        }

        public Connection GetUsersConnection(string firstUserId, string secondUserId)
        {
            var connection = this.Data.Connections.All()
                .FirstOrDefault(c => ((c.FirstUserId == firstUserId || c.SecondUserId == firstUserId) &&
                (c.FirstUserId == secondUserId || c.SecondUserId == secondUserId) &&
                firstUserId != secondUserId));

            return connection;
        }

        public bool IsAuthorised(int connectionId, string userId)
        {
            var connection = this.Data.Connections.GetById(connectionId);
            if (connection == null)
            {
                return false;
            }

            var result = userId == connection.FirstUserId || userId == connection.SecondUserId;

            return result;
        }
    }
}