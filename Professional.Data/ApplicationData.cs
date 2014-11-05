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

        public IRepository<Post> Posts
        {
            get { throw new NotImplementedException(); }
        }

        public IRepository<PostEndorsement> PostEndorsements
        {
            get { throw new NotImplementedException(); }
        }

        public IRepository<UserEndorsement> UserEndorsements
        {
            get { throw new NotImplementedException(); }
        }
    }
}
