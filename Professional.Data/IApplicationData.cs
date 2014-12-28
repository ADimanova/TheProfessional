using Professional.Data;
using Professional.Data.Contracts;
using Professional.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Data
{
    public interface IApplicationData
    {
        IApplicationDbContext Context { get; }
        IRepository<User> Users { get; }
        IDeletableEntityRepository<Post> Posts { get; }
        IDeletableEntityRepository<EndorsementOfPost> EndorsementsOfPosts { get; }
        IDeletableEntityRepository<EndorsementOfUser> EndorsementsOfUsers { get; }
        IDeletableEntityRepository<FieldOfExpertise> FieldsOfExpertise { get; }
        IDeletableEntityRepository<Occupation> Occupations { get; }
        IDeletableEntityRepository<Connection> Connections { get; }
        IDeletableEntityRepository<Notification> Notifications { get; }
        IDeletableEntityRepository<Image> Images { get; }
        IDeletableEntityRepository<Message> Messages { get; }
        int SaveChanges();
    }
}
