namespace Professional.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Professional.Data;
    using Professional.Web.Helpers;
    using Professional.Web.Infrastructure.Services.Contracts;
    using Professional.Web.Models;
    using Professional.Web.Models.Shared;
    using Professional.Web.Models.Home;

    public class HomeController : BaseController
    {
        private const string FieldsNavBarTitle = "Our top fields";
        private const int FieldsCount = 9;
        private const int PostCount = 3;
        private const int FeaturedCount = 3;

        private IHomeServices homeServices;

        public HomeController(IApplicationData data, IHomeServices homeServices)
            : base(data)
        {
            this.homeServices = homeServices;
        }

        public ActionResult Index()
        {
            var fields = this.homeServices.GetFields()
                .Select(f => new NavigationItem
                {
                    Content = f.Name,
                    Url = WebConstants.FieldInfoPageRoute + f.Name
                })
                .ToList();

            var posts = this.homeServices.GetTopPosts()
                .Project().To<PostSimpleViewModel>()
                .ToList();

            posts.ForEach(p => p.ShortTitle = StringManipulations.GetSubstring(p.Title, 0, WebConstants.TitleLength));

            var featured = this.homeServices.GetFeatured()
                .Project().To<UserSimpleViewModel>()
                .ToList();

            var fieldsView = new HorizontalNavbarViewModel();
            fieldsView.Title = FieldsNavBarTitle;
            fieldsView.ListItems = fields;
            fieldsView.MoreInfoUrl = WebConstants.FieldsListingPageRoute;

            var vielModel = new IndexViewModel();

            vielModel.FieldsListing = fieldsView;
            vielModel.Posts = posts.ToList<PostSimpleViewModel>();
            vielModel.Featured = featured.ToList<UserSimpleViewModel>();

            return this.View(vielModel);
        }
 
        public ActionResult Error()
        {
            return this.View();
        }

        public ActionResult About()
        {
            return this.View();
        }

        public ActionResult Contact()
        {
            return this.View();
        }
    }
}