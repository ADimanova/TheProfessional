using Professional.Models;
using Professional.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Web.Infrastructure.Services.Contracts
{
    public interface IListingServices
    {
        IQueryable<User> GetUsers(string filter);
        IQueryable<Post> GetPosts(string filter, string user);
        IQueryable<EndorsementOfUser> GetEndorsements(string userID);
    }
}