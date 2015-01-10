namespace Professional.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Professional.Models;

    public interface IApplicationDbContext
    {
        IDbSet<Company> Companies { get; set; }

        IDbSet<EndorsementOfPost> EndorsementsOfPosts { get; set; }

        IDbSet<EndorsementOfUser> EndorsementsOfUsers { get; set; }

        IDbSet<FieldOfExpertise> FieldsOfExpertise { get; set; }

        IDbSet<Occupation> Occupations { get; set; }

        IDbSet<Connection> Connections { get; set; }

        IDbSet<Notification> Notifications { get; set; }

        IDbSet<Post> Posts { get; set; }

        IDbSet<Image> Images { get; set; }

        IDbSet<Message> Messages { get; set; }
        IDbSet<ChatGroup> ChatGroups { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
