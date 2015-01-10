namespace Professional.Web.Areas.UserArea.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Professional.Data;
    using Professional.Models;
    using Professional.Web.Areas.UserArea.Models;
    using Professional.Web.Areas.UserArea.Models.CreateItem;
    using Professional.Web.Areas.UserArea.Models.ListingViewModels;
    using Professional.Web.Areas.UserArea.Models.Profile.Private;
    using Professional.Web.Areas.UserArea.Models.Profile.Public;
    using Professional.Web.Helpers;
    using Professional.Web.Infrastructure.Services.Contracts;
    using Professional.Web.Models.Shared;
    using Professional.Web.Areas.UserArea.Models.Shared;
using Professional.Web.Areas.UserArea.Models.Profile;

    public class ProfileController : UserController
    {
        private const int ChatsToTake = 5;
        private const int MessagesToTake = 5;

        private static User currentUser;
        private IProfileServices profileServices;

        private static int chatsCount = 0;
        private static int messagesCount = 0;

        private static bool LoadChats;
        private static bool LoadMessages;

        public ProfileController(IApplicationData data, IProfileServices profileServices)
            : base(data)
        {
            this.profileServices = profileServices;
        }

        public ActionResult Public(string id)
        {
            currentUser = this.GetUser(id);
            if (currentUser == null)
            {
                return this.RedirectToAction("Index", "Home", new { Area = string.Empty });
            }

            string loggedUserId = this.GetLoggedUserId();
            if (currentUser.Id == loggedUserId)
            {
                return this.RedirectToAction("Private", "Profile", new { Area = "UserArea" });
            }

            var userInfo = this.SetUserInfo(currentUser, false);

            var userFields = this.profileServices.GetUserFields(id)
                .Select(f => f.Name).ToList();

            var topPostPanel = new ListPanelViewModel();
            topPostPanel.UniqueIdentificator = "Top";
            topPostPanel.Title = "Top Posts";
            topPostPanel.Items = this.profileServices.GetTopPosts(id)
                .Select(p => new NavigationItem
                {
                    Content = p.Title,
                    Url = "#"
                }).ToList<NavigationItem>();
            topPostPanel.Fields = userFields;

            var recentPostPanel = new ListPanelViewModel();
            recentPostPanel.UniqueIdentificator = "Recent";
            recentPostPanel.Title = "Recent Posts";
            recentPostPanel.Items = this.profileServices.GetRecentPosts(id)
                .Select(p => new NavigationItem
                {
                    Content = p.Title,
                    Url = "#"
                }).ToList<NavigationItem>();
            recentPostPanel.Fields = userFields;

            var btnNavigatePosts = new NavigationItem();
            btnNavigatePosts.Content = "See post's page";
            btnNavigatePosts.Url = WebConstants.UserPostsPageRoute + id;

            var btnNavigateEndorsements = new NavigationItem();
            btnNavigateEndorsements.Content = "See endorsements's page";
            btnNavigateEndorsements.Url = WebConstants.UserEndorsementsPageRoute + id;

            var isEndorsed = this.profileServices.IsEndorsed(id, loggedUserId);

            var endorsements = this.profileServices.GetUserEndorsements(id)
                .Project().To<UserEndorsementViewModel>();
            
            var connection = this.profileServices.GetConnection(id, loggedUserId);
            var contactModel = new ContactViewModel();
            contactModel.FromUserId = id;
            contactModel.FromUserName = currentUser.FullName;
            contactModel.IsConnected = connection != null;
            contactModel.IsAccepted = connection != null ? connection.IsAccepted : false;

            var publicProfileInfo = new PublicProfileViewModel();
            publicProfileInfo.UserInfo = userInfo;
            publicProfileInfo.ContactInfo = contactModel;
            if (!isEndorsed && loggedUserId != id)
            {
                var endorseInfo = new EndorsementInputModel();
                endorseInfo.EndorsedID = id;
                endorseInfo.EndorseAction = "EndorsementOfUser";
                publicProfileInfo.EndorseFunctionality = endorseInfo;
            }

            publicProfileInfo.BtnNavigatePosts = btnNavigatePosts;
            publicProfileInfo.BtnNavigateEndorsements = btnNavigateEndorsements;
            publicProfileInfo.TopPostsList = topPostPanel;
            publicProfileInfo.RecentPostsList = recentPostPanel;
            publicProfileInfo.UserInfo.Endorsements = endorsements.ToList();

            return this.View(publicProfileInfo);
        }

        public ActionResult Private()
        {
            string currentUserId = this.GetLoggedUserId();

            var currentUser = this.GetUser(currentUserId);
            var userInfo = this.SetUserInfo(currentUser, true);

            var occupationsListing = new ShortListingViewModel();
            occupationsListing.Title = "Occupations";
            occupationsListing.Type = "Occupation";
            occupationsListing.Items = userInfo.Occupations;

            var fieldsListing = new ShortListingViewModel();
            fieldsListing.Title = "Fields";
            fieldsListing.Type = "Field";
            fieldsListing.Items = userInfo.Fields;

            var proInfo = new PrivateProInfoViewModel();
            proInfo.OccupationsListing = occupationsListing;
            proInfo.FieldsListing = fieldsListing;

            chatsCount = ChatsToTake;
            LoadChats = true;
            var messagesReceived = this.GetQueriedChats(currentUser.Id, 0, ChatsToTake);

            LoadMessages = true;
            messagesCount = 0;

            var connectionRequests = this.profileServices.GetUserConnectionRequests(currentUserId)
                .Project().To<ConnectionViewModel>();

            var notifications = this.profileServices.GetUserNotifications(currentUserId)
                .Project().To<NotificationShortViewModel>();

            var updateModel = new UpdatesViewModel();
            if (messagesReceived.Any(m => !m.IsRead))
            {
                updateModel.HasNewMessages = true;
            }

            var messagesCounted = messagesReceived.Count();
            if (messagesCounted > 0)
            {
                var chats = new ChatsListingViewModel();
                chats.ChatsListing = messagesReceived.ToList();
                if (messagesCounted == ChatsToTake)
	            {
                    chats.LoadMore = true;
	            }
                updateModel.ActiveChats = chats;
            }

            if (connectionRequests.Count() > 0)
            {
                updateModel.HasNewConnection = true;
                updateModel.ConnectionRequests = connectionRequests.ToList();
            }

            if (notifications.Count() > 0)
            {
                updateModel.HasNewNotifications = true;
                updateModel.Notifications = notifications.ToList();
            }

            var privateProfileInfo = new PrivateProfileViewModel();

            IList<NavigationItem> navItems = this.GetNavItems(currentUser.Id);
            var navList = new HorizontalNavbarViewModel();
            navList.Title = "Navigation";
            navList.ListItems = navItems;
            privateProfileInfo.UserInfo = userInfo;
            privateProfileInfo.UpdatesInfo = updateModel;
            privateProfileInfo.NavigationList = navList;
            privateProfileInfo.ProInfo = proInfo;

            return this.View(privateProfileInfo);
        }

        public ActionResult DeleteOccupation(string query, string title)
        {
            string currentUserId = this.GetLoggedUserId();
            var currentUser = this.GetUser(currentUserId);

            var occupation = currentUser.Occupations.FirstOrDefault(f => f.Title == query);
            currentUser.Occupations.Remove(occupation);

            var editedResult = currentUser.Occupations.Select(o => o.Title);

            var listModel = new ShortListingViewModel();
            listModel.Title = title;
            listModel.Type = "Occupation";
            listModel.Items = editedResult.ToList();

            return this.PartialView("~/Areas/UserArea/Views/Shared/Partials/_ShortListPanel.cshtml", listModel);
        }

        public ActionResult DeleteField(string query, string title)
        {
            string currentUserId = this.GetLoggedUserId();
            var currentUser = this.GetUser(currentUserId);

            var field = currentUser.FieldsOfExpertise.FirstOrDefault(f => f.Name == query);
            currentUser.FieldsOfExpertise.Remove(field);

            this.data.SaveChanges();

            var editedResult = currentUser.FieldsOfExpertise.Select(o => o.Name);

            var listModel = new ShortListingViewModel();
            listModel.Title = title;
            listModel.Type = "Field";
            listModel.Items = editedResult.ToList();

            return this.PartialView("~/Areas/UserArea/Views/Shared/Partials/_ShortListPanel.cshtml", listModel);
        }

        public ActionResult LoadMoreChats()
        {
            if (!LoadChats)
            {
                return new EmptyResult();
            }

            var userId = this.GetLoggedUserId();
            var chat = this.GetQueriedChats(userId, chatsCount, ChatsToTake).ToList();
            if (chat.Count < ChatsToTake)
            {
                LoadChats = false;
            }
            chatsCount = chatsCount + ChatsToTake;

            var chats = new ChatsListingViewModel();
            chats.ChatsListing = chat.ToList();
            return this.PartialView("~/Areas/UserArea/Views/Shared/Partials/_ActiveChatsListing.cshtml", chats);
        }

        [HttpPost]
        public ActionResult LoadMoreMessages(LoadData data)
        {
            var secondUser = this.GetUser(data.Id);

            if (!LoadMessages || secondUser == null)
            {
                return new EmptyResult();
            }

            var loggedUserId = this.GetLoggedUserId();
            var messages = this.GetQueriedMessages(loggedUserId, data.Id, messagesCount, MessagesToTake, data.Time).ToList();
            if (data.Loaded)
            {
                if (messages.Count < MessagesToTake)
                {
                    LoadMessages = false;
                }
                messagesCount = messagesCount + MessagesToTake;
            }

            return this.Json(messages);
        }

        public ActionResult Filter(string query, string condition)
        {
            var resultPosts = this.profileServices.GetFilteredPosts(query, condition)
                .Take(WebConstants.ListPanelCount)
                .Select(i => new NavigationItem
                {
                    Content = i.Title,
                    Url = WebConstants.PostPageRoute + i.ID
                }).ToList();

            return this.PartialView("~/Areas/UserArea/Views/Shared/Partials/_ListItems.cshtml", resultPosts);
        }

        public ActionResult ResetMessageCount()
        {
            messagesCount = 0;
            LoadMessages = true;
            return new EmptyResult();
        }

        private UserViewModel SetUserInfo(User user, bool isPrivate)
        {
            var userInfo = Mapper.Map<UserViewModel>(user);
            userInfo.PersonalHistory = this.GetModifiedHistory(userInfo.PersonalHistory);
            userInfo.IsPrivate = isPrivate;
            userInfo.ProfileImageUrl = userInfo.ProfileImageId == null ?
                WebConstants.DefaultImage :
                WebConstants.GetImagePageRoute + userInfo.ProfileImageId;

            return userInfo;
        }

        private string GetModifiedHistory(string history)
        {
            if (history == null)
            {
                return WebConstants.DefaultHistory;
            }
            else
            {
                return history;
            }
        }

        private IQueryable<ChatListedViewModel> GetQueriedChats(string userId, int skipCount, int takeCount)
        {
            var messagesReceived = this.profileServices.GetUserMessages(userId)
                .GroupBy(m => m.FromUserId)
                .OrderByDescending(g => g.FirstOrDefault().CreatedOn)
                .Skip(skipCount)
                .Take(takeCount)
                .Select(g => new ChatListedViewModel
                {
                    FromUserId = g.Key,
                    FromUserName = g.FirstOrDefault().FromUser.FirstName + " " + g.FirstOrDefault().FromUser.LastName,
                    Preview = g.OrderByDescending(m => m.CreatedOn).FirstOrDefault().Content.Substring(0, 20) + "...",
                    IsRead = g.OrderByDescending(m => m.CreatedOn).FirstOrDefault().IsRead
                });

            return messagesReceived;
        }

        private IQueryable<MessageViewModel> GetQueriedMessages(
            string firstUserId, string secondUserId, int skipCount, int takeCount, string date)
        {
            DateTime parsedDate;
            var isValid = DateTime.TryParse(date, out parsedDate);

            var messagesReceived = this.profileServices.GetConversarionMessages(firstUserId, secondUserId);

            if (isValid)
            {
                messagesReceived = messagesReceived.Where(m => m.CreatedOn < parsedDate);
            }

            messagesReceived = messagesReceived
                .OrderByDescending(m => m.CreatedOn)
                .Skip(skipCount)
                .Take(takeCount);

            var messagesResult = messagesReceived.Select(m => new MessageViewModel
                {
                    Sender = m.FromUser.FirstName,
                    Content = m.Content,
                    FirstUserId = m.FromUser.Id
                });

            return messagesResult;
        }


        private IList<NavigationItem> GetNavItems(string currentUserId)
        {
            return new List<NavigationItem>
            {
                new NavigationItem 
                { 
                    Content = "Create Post",
                    Url = WebConstants.CreatePostPageRoute
                },
                new NavigationItem 
                { 
                    Content = "Posts",
                    Url = WebConstants.UserPostsPageRoute + currentUserId
                },
                new NavigationItem 
                { 
                    Content = "Endorsements",
                    Url = WebConstants.UserEndorsementsPageRoute + currentUserId
                },
                new NavigationItem 
                { 
                    Content = "Connections",
                    Url = WebConstants.UserConnectionsPageRoute + currentUserId
                },
            };
        }
    }
}