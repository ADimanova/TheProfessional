namespace Professional.Data
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Professional.Data.Contracts;
    using Professional.Data.Repositories;
    using Professional.Data.Repositories.Base;
    using Professional.Models;

    public class ApplicationData : IApplicationData
    {
        private readonly IApplicationDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public ApplicationData(IApplicationDbContext context)
        {
            this.context = context;
        }
       
        public IApplicationDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<User> Users
        {
            get 
            { 
                return this.GetRepository<User>();
            }
        }

        public IRepository<IdentityRole> Roles
        {
            get 
            { 
                return this.GetRepository<IdentityRole>();
            }
        }

        public IDeletableEntityRepository<Post> Posts
        {
            get 
            {
                return this.GetDeletableEntityRepository<Post>();
            }
        }

        public IDeletableEntityRepository<EndorsementOfPost> EndorsementsOfPosts
        {
            get 
            { 
                return this.GetDeletableEntityRepository<EndorsementOfPost>();
            }
        }

        public IDeletableEntityRepository<EndorsementOfUser> EndorsementsOfUsers
        {
            get
            { 
                return this.GetDeletableEntityRepository<EndorsementOfUser>();
            }
        }

        public IDeletableEntityRepository<FieldOfExpertise> FieldsOfExpertise
        {
            get
            { 
                return this.GetDeletableEntityRepository<FieldOfExpertise>();
            }
        }

        public IDeletableEntityRepository<Occupation> Occupations
        {
            get
            { 
                return this.GetDeletableEntityRepository<Occupation>();
            }
        }

        public IDeletableEntityRepository<Connection> Connections
        {
            get
            { 
                return this.GetDeletableEntityRepository<Connection>();
            }
        }

        public IDeletableEntityRepository<Notification> Notifications
        {
            get
            { 
                return this.GetDeletableEntityRepository<Notification>();
            }
        }

        public IDeletableEntityRepository<Image> Images
        {
            get
            { 
                return this.GetDeletableEntityRepository<Image>();
            }
        }

        public IDeletableEntityRepository<Message> Messages
        {
            get
            { 
                return this.GetDeletableEntityRepository<Message>();
            }
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
