namespace Professional.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Microsoft.AspNet.Identity;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Areas.UserArea.Models.CreateItem;
    using Professional.Web.Models.Post;

    public class PostController : BaseController
    {
        public PostController(IApplicationData data)
            : base(data)
        {
        }

        // GET: Post/Info
        public ActionResult Info(int id)
        {
            var post = this.data.Posts.All()
                .Where(p => p.ID == id)
                .FirstOrDefault();

            if (post == null)
            {
                return this.HttpNotFound("This post does not exist");
            }

            Mapper.CreateMap<Post, PostViewModel>();
            var postInfoForView = Mapper.Map<PostViewModel>(post);

            var loggedUserId = this.GetLoggedUserId();
            var isEndorsed = this.data.EndorsementsOfPosts.All()
                .Where(e => e.EndorsingUserID == loggedUserId)
                .Any(e => e.EndorsedPostID == id);

            if (!isEndorsed || postInfoForView.CreatorID == loggedUserId)
            {
                ViewBag.IsEndorsed = "true";
            }
            else
            {
                var endorseInfo = new EndorsementInputModel();
                endorseInfo.EndorsedID = id.ToString();
                endorseInfo.EndorseAction = "EndorsementOfPost";
                postInfoForView.EndorseFunctionality = endorseInfo;
            }

            return this.View(postInfoForView);
        }
    }
}