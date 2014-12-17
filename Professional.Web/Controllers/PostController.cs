using AutoMapper;
using Professional.Data;
using Professional.Models;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Professional.Web.Models.InputViewModels;

namespace Professional.Web.Controllers
{
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

            var userID = User.Identity.GetUserId();
            var postID = id.ToString();
            var isEndorsed = this.data.EndorsementsOfPosts.All()
                .Where(e => e.EndorsingUserID == userID)
                .Any(e => e.EndorsedPostID == postID);

            if (!isEndorsed)
            {
                var endorseInfo = new EndorsementInputModel();
                endorseInfo.EndorsedID = postID;
                endorseInfo.EndorseAction = "EndorsementOfPost";
                postInfoForView.EndorseFunctionality = endorseInfo;
            }
            else
            {
                ViewBag.IsEndorsed = "true";
            }

            return View(postInfoForView);
        }
    }
}