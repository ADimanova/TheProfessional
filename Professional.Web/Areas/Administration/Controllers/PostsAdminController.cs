using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Professional.Data;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.Administration.Controllers
{
    public class PostsAdminController : AdminController
    {
        public PostsAdminController(IApplicationData data)
            :base(data)
        {
        }

        // GET: Administration/PostsAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts_Read([DataSourceRequest]DataSourceRequest request)
        {
            var posts = this.data.Posts.All()
                .Project().To<PostViewModel>();

            return Json(posts.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }
    }
}