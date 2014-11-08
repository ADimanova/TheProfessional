using Professional.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Data
{
    public class ApplicationData : IApplicationData
    {
        private readonly DbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public ApplicationData()
            : this(new ApplicationDbContext())
        {
        }

        public ApplicationData(DbContext context)
        {
            this.context = context;
        }

        public IRepository<User> Users
        {
            get
            {
                return GetRepository<User>();
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                return this.GetRepository<Post>();
            }
        }

        public IRepository<EndorsementOfPost> EndorsementsOfPosts
        {
            get
            {
                return this.GetRepository<EndorsementOfPost>();
            }
        }

        public IRepository<EndorsementOfUser> EndorsementsOfUsers
        {
            get
            {
                return this.GetRepository<EndorsementOfUser>();
            }
        }

        public IRepository<FieldOfExpertise> FieldsOfExpertise
        {
            get
            {
                return this.GetRepository<FieldOfExpertise>();
            }
        }

        public IRepository<Occupation> Occupations
        {
            get
            {
                return this.GetRepository<Occupation>();
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

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
        public IRepository<Company> Companies
        {
            get
            {
                return this.GetRepository<Company>();
            }
        }
    }
}
