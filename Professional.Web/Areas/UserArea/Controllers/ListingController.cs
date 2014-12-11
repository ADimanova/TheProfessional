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

            var firstLetters = this.listingServices.GetLetters();

            var groupedByFirstLetter = usersPaged.GroupBy(s => s.LastName.Substring(0, 1).ToUpper())
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

            var fieldsNames = this.listingServices.GetFeilds();

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

            var firstLetters = this.listingServices.GetLetters();

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
    }
}