namespace Professional.Web.Infrastructure.Services.Contracts
{
    using Professional.Models;

    public interface IUpdatesServices
    {
        Connection GetUsersConnection(string firstUserId, string secondUserId);

        bool IsAuthorised(int connectionId, string userId);
    }
}
