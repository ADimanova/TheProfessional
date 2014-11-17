using Professional.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Web.Infrastructure.Services.Contracts
{
    public interface IHomeServices
    {
        IQueryable<NavigationItem> GetFields();
        IQueryable<PostSimpleViewModel> GetTopPosts();
        IQueryable<UserSimpleViewModel> GetFeatured();
    }
}
