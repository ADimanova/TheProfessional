﻿using Professional.Data;
using Professional.Models;
using Professional.Web.Areas.UserArea.Models;
using Professional.Web.Areas.UserArea.Models.ListingViewModels;
using Professional.Web.Helpers;
using Professional.Web.Infrastructure.Services.Contracts;
using Professional.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.UserArea.Controllers
{
    public class ListingController : UserController
    {
        int itemsPerPage = WebConstants.PostsPerPage;
        private IListingServices listingServices;
        public ListingController(IApplicationData data, IListingServices listingServices)
            : base(data)
        {
            this.listingServices = listingServices;
        }

        // GET: UserArea/Listing
        public ActionResult Users(string id, int? page)
        {
            int pageNumber = page.GetValueOrDefault(1);

            // id is the filter value
            var users = this.listingServices.GetUsers(id);
            var usersCount = users.Count();
            var usersPaged = users.OrderBy(f => f.UserName)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage);

            var firstLetters = users
                .Select(f => f.LastName.Substring(0, 1)).Distinct().OrderBy(l => l);

            var groupedByFirstLetter = usersPaged.GroupBy(s => s.LastName.Substring(0, 1))
                .Select(g => new ItemsByFieldViewModel
                {
                    Name = g.Key.ToString(),
                    Items = g.Select(i => new NavigationItem
                    {
                        Content = i.LastName,
                        Url = WebConstants.PublicProfilePageRoute + i.Id
                    }).ToList()
                });

            var pageCount = Math.Ceiling((double)usersCount / itemsPerPage);
            this.SetPaging(pageNumber, pageCount);
            ViewBag.Url = WebConstants.UsersPageRoute;

            var viewModel = new ListCollectionViewModel();
            viewModel.FieldsNames = firstLetters.ToList();
            viewModel.Title = "Professionals";
            viewModel.GetBy = "first letter";
            viewModel.Fields = groupedByFirstLetter.ToList();

            return View(viewModel);
        }

        public ActionResult Posts(string id, int? page, string user)
        {
            int pageNumber = page.GetValueOrDefault(1);

            // id is the filter value
            var posts = this.listingServices.GetPosts(id, user);
            var postsCount = posts.Count();

            var postsPaged = posts.OrderBy(f => f.Field.Name)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage);

            var fieldsNames = this.data.FieldsOfExpertise
                .All()
                .Select(f => f.Name);

            var groupedByField = postsPaged.GroupBy(p => p.Field.Name)
                .Select(p => new ItemsByFieldViewModel
                {
                    Name = p.FirstOrDefault().Field.Name,
                    Items = p.Select(i => new NavigationItem
                    {
                        Content = i.Title,
                        Url = WebConstants.PostPageRoute + i.ID
                    }).ToList()
                });

            var pageCount = Math.Ceiling((double)postsCount / itemsPerPage);
            this.SetPaging(pageNumber, pageCount);

            if (user == null)
            {
                ViewBag.Url = WebConstants.PostsPageRoute;                
            }
            else
            {
                ViewBag.Url = WebConstants.UserPostsPageRoute;
            }

            var viewModel = new ListCollectionViewModel();
            viewModel.FieldsNames = fieldsNames.ToList();
            viewModel.Title = "Posts";
            viewModel.GetBy = "field";
            viewModel.Fields = groupedByField.ToList();

            return View(viewModel);
        }

        public ActionResult UserEndorsements(string id, int? page)
        {
            int pageNumber = page.GetValueOrDefault(1);

            //id is the user's id
            var endorsements = this.listingServices.GetEndorsements(id);
            var endorsementsCount = endorsements.Count();

            var endorsementsPaged = endorsements
                .OrderBy(e => e.EndorsingUser.LastName)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(e => new EndorsementViewModel
                {
                    ID = e.ID,
                    Content = e.Comment,
                    AuthorFirstName = e.EndorsedUser.FirstName,
                    AuthorLastName = e.EndorsedUser.LastName,
                    AuthorID = e.EndorsingUserID
                });

            var firstLetters = endorsements
                .Select(f => f.EndorsingUser.LastName.Substring(0, 1)).Distinct().OrderBy(l => l);

            var groupedByFirstLetter = endorsementsPaged.GroupBy(s => s.AuthorLastName.Substring(0, 1))
                .Select(g => new ItemsByFieldViewModel
                {
                    Name = g.Key.ToString(),
                    Items = g.Select(i => new NavigationItem
                    {
                        Content = i.AuthorLastName + ", " + i.AuthorFirstName,
                        Url = WebConstants.PublicProfilePageRoute + i.AuthorID
                    }).ToList()
                });

            var pageCount = Math.Ceiling((double)endorsementsCount / itemsPerPage);
            this.SetPaging(pageNumber, pageCount);
            ViewBag.Url = WebConstants.UserEndorsementsPageRoute + id + "/";

            var viewModel = new ListCollectionViewModel();
            viewModel.FieldsNames = firstLetters.ToList();
            viewModel.Title = "Endorsements of user";
            viewModel.Fields = groupedByFirstLetter.ToList();

            return View(viewModel);
        }

        private void SetPaging(int pageNumber, double pageCount)
        {
            ViewBag.Pages = pageCount;

            ViewBag.CurrentPage = pageNumber;

            if (ViewBag.CurrentPage >= pageCount)
            {
                ViewBag.NextPage = 1;
            }
            else
            {
                ViewBag.NextPage = ViewBag.CurrentPage + 1;
            }

            if (ViewBag.CurrentPage <= 1)
            {
                ViewBag.PreviousPage = pageCount;
            }
            else
            {
                ViewBag.PreviousPage = ViewBag.CurrentPage - 1;
            }
        }

    //    [OutputCache(Duration = 60, VaryByParam = "none")]
    //    private IQueryable<User> GetUsers(string filter)
    //    {
    //        var users = this.data.Users.All();
    //        if (filter != null)
    //        {
    //            filter = filter.ToLower();
    //            users = users.Where(u => u.UserName.Substring(0, 1) == filter);
    //        }

    //        return users;
    //    }

    //    [OutputCache(Duration = 60, VaryByParam = "none")]
    //    private IQueryable<Post> GetPosts(string filter, string user)
    //    {
    //        var posts = this.data.Posts.All();
    //        if (filter != null)
    //        {
    //            filter = filter.ToLower();
    //            posts = posts.Where(p => p.Field.Name.ToLower() == filter);
    //        }

    //        if (user != null)
    //        {
    //            posts = posts.Where(p => p.CreatorID == user);
    //        }

    //        return posts;
    //    }

    //    [OutputCache(Duration = 60, VaryByParam = "none")]
    //    private IQueryable<EndorsementOfUser> GetEndorsements(string userID)
    //    {
    //        var endorsements = this.data.EndorsementsOfUsers.All();
    //        if (userID == null)
    //        {
    //            throw new ArgumentNullException("You must specify the user by ID");
    //        }

    //        endorsements = endorsements.Where(p => p.EndorsedUserID == userID);

    //        return endorsements;
    //    }
    }
}