using Professional.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace Professional.Data
{
    public interface IApplicationDbContext
    {
        System.Data.Entity.IDbSet<Company> Companies { get; set; }
        System.Data.Entity.IDbSet<EndorsementOfPost> EndorsementsOfPosts { get; set; }
        System.Data.Entity.IDbSet<EndorsementOfUser> EndorsementsOfUsers { get; set; }
        System.Data.Entity.IDbSet<FieldOfExpertise> FieldsOfExpertise { get; set; }
        System.Data.Entity.IDbSet<Occupation> Occupations { get; set; }
        System.Data.Entity.IDbSet<Post> Posts { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}


