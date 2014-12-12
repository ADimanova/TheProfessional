using Professional.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Professional.Models;

namespace Professional.Web.Infrastructure.Services.Contracts
{
    public interface IHomeServices
    {
        IQueryable<FieldOfExpertise> GetFields();
        IQueryable<Post> GetTopPosts();
        IQueryable<User> GetFeatured();
    }
}
