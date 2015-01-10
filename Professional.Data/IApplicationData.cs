namespace Professional.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Professional.Data.Contracts;

    using Professional.Models;

    public interface IApplicationData
    {
        IApplicationDbContext Context { get; }

        IRepository<User> Users { get; }

        IRepository<IdentityRole> Roles { get; }

        IDeletableEntityRepository<Post> Posts { get; }

        IDeletableEntityRepository<EndorsementOfPost> EndorsementsOfPosts { get; }

        IDeletableEntityRepository<EndorsementOfUser> EndorsementsOfUsers { get; }

        IDeletableEntityRepository<FieldOfExpertise> FieldsOfExpertise { get; }

        IDeletableEntityRepository<Occupation> Occupations { get; }

        IDeletableEntityRepository<Connection> Connections { get; }

        IDeletableEntityRepository<Notification> Notifications { get; }

        IDeletableEntityRepository<Image> Images { get; }

        IDeletableEntityRepository<Message> Messages { get; }

        IDeletableEntityRepository<ChatGroup> ChatGroups { get; }

        int SaveChanges();
    }
}
