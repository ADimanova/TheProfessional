using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Professional.Models;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Professional.Data.Contracts;
using Professional.Data;
using Professional.Web.Infrastructure.Caching;
using Professional.Web.Infrastructure.Services;

namespace Professional.Tests
{
    [TestClass]
    public class HomeServicesTests
    {
        // GetFields Tests
        [TestMethod]
        public void GetFieldsGets9FieldsOrderedByRank()
        {
            var fakeFields = new List<FieldOfExpertise> {
                new FieldOfExpertise { Name = "testObjA", Rank = 3 },
                new FieldOfExpertise { Name = "testObjB", Rank = 0 },
                new FieldOfExpertise { Name = "testObjC", Rank = 2 },
                new FieldOfExpertise { Name = "testObjD", Rank = 1 },
                new FieldOfExpertise { Name = "testObjE", Rank = 4 },
                new FieldOfExpertise { Name = "testObjF", Rank = 5 },
                new FieldOfExpertise { Name = "testObjG", Rank = 7 },
                new FieldOfExpertise { Name = "testObjH", Rank = 8 },
                new FieldOfExpertise { Name = "testObjI", Rank = 6 },
                new FieldOfExpertise { Name = "testObjJ", Rank = 9 },
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<FieldOfExpertise>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeFields);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.FieldsOfExpertise).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<FieldOfExpertise>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<FieldOfExpertise>>>()))
                .Returns(() => fakeFields
                .OrderByDescending(f => f.Rank)
                .Take(9)
                .AsQueryable<FieldOfExpertise>());

            var homeServices = new HomeServices(data.Object, cache.Object);

            var fieldsResult = homeServices.GetFields();

            CollectionAssert.AreEquivalent(
                new List<string> { 
                    "testObjJ", "testObjH", "testObjG",
                    "testObjI", "testObjF", "testObjE",
                    "testObjA", "testObjC", "testObjD"
                },
            fieldsResult.Select(i => i.Name).ToList<string>());
        }

        [TestMethod]
        public void GetFieldsGetsAllFieldsWhenLessThan9()
        {
            var fakeFields = new List<FieldOfExpertise> {
                new FieldOfExpertise { Name = "testObjA", Rank = 3 },
                new FieldOfExpertise { Name = "testObjB", Rank = 0 },
                new FieldOfExpertise { Name = "testObjC", Rank = 2 },
                new FieldOfExpertise { Name = "testObjD", Rank = 1 },
                new FieldOfExpertise { Name = "testObjE", Rank = 4 },
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<FieldOfExpertise>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeFields);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.FieldsOfExpertise).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<FieldOfExpertise>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<FieldOfExpertise>>>()))
                .Returns(() => fakeFields
                .OrderByDescending(f => f.Rank)
                .Take(9)
                .AsQueryable<FieldOfExpertise>());

            var homeServices = new HomeServices(data.Object, cache.Object);

            var fieldsResult = homeServices.GetFields();

            CollectionAssert.AreEquivalent(
                new List<string> { 
                    "testObjE","testObjA", "testObjC", "testObjD", "testObjB"
                },
            fieldsResult.Select(i => i.Name).ToList<string>());
        }

        [TestMethod]
        public void GetFieldsGets0FieldsWhenNoFields()
        {
            var fakeFields = new List<FieldOfExpertise> {
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<FieldOfExpertise>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeFields);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.FieldsOfExpertise).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<FieldOfExpertise>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<FieldOfExpertise>>>()))
                .Returns(() => fakeFields
                .OrderByDescending(f => f.Rank)
                .Take(9)
                .AsQueryable<FieldOfExpertise>());

            var homeServices = new HomeServices(data.Object, cache.Object);

            var fieldsResult = homeServices.GetFields();

            Assert.AreEqual(0, fieldsResult.Count());
        }

        // GetTopPosts Tests
        [TestMethod]
        public void GetTopPostsReturns3PostOrderedByDate()
        {
            var fakePosts = new List<Post>();
            for (int i = 0; i < 4; i++)
            {
                fakePosts.Add(new Post
                {
                    Title = "Test" + i,
                    Content = "TestContent",
                    CreatorID = "Selected",
                    FieldID = 1,
                    CreatedOn = DateTime.Now.AddDays(i)
                });
            }

            var fakePostsQueryable = fakePosts.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<Post>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakePostsQueryable);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Posts).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<Post>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<Post>>>()))
                .Returns(() => fakePostsQueryable
                    .Where(i => i.IsDeleted == false)
                    .OrderByDescending(i => i.CreatedOn)
                    .Take(3)
                    .AsQueryable());

            var homeServices = new HomeServices(data.Object, cache.Object);

            var postsResult = homeServices.GetTopPosts();

            CollectionAssert.AreEquivalent(
                new List<string> { 
                    "Test3", "Test2", "Test1",
                },
            postsResult.Select(i => i.Title).ToList<string>());
        }

        [TestMethod]
        public void GetTopGetsAllPostsWhenLessThan3()
        {
            var fakePosts = new List<Post>();
            for (int i = 0; i < 2; i++)
            {
                fakePosts.Add(new Post
                {
                    Title = "Test" + i,
                    Content = "TestContent",
                    CreatorID = "Selected",
                    FieldID = 1,
                    CreatedOn = DateTime.Now.AddDays(i)
                });
            }

            var fakePostsQueryable = fakePosts.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<Post>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakePostsQueryable);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Posts).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<Post>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<Post>>>()))
                .Returns(() => fakePostsQueryable
                    .Where(i => i.IsDeleted == false)
                    .OrderByDescending(i => i.CreatedOn)
                    .Take(3)
                    .AsQueryable());

            var homeServices = new HomeServices(data.Object, cache.Object);

            var postsResult = homeServices.GetTopPosts();

            CollectionAssert.AreEquivalent(
                new List<string> { 
                    "Test1", "Test0"
                },
            postsResult.Select(i => i.Title).ToList<string>());
        }

        [TestMethod]
        public void GetTopGets0PostsWhenNoPosts()
        {
            var fakePosts = new List<Post>().AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<Post>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakePosts);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Posts).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<Post>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<Post>>>()))
                .Returns(() => fakePosts
                    .Where(i => i.IsDeleted == false)
                    .OrderByDescending(i => i.CreatedOn)
                    .Take(3)
                    .AsQueryable());

            var homeServices = new HomeServices(data.Object, cache.Object);

            var postsResult = homeServices.GetTopPosts();

            Assert.AreEqual(0, postsResult.Count());
        }

        // GetFeatured Tests
        [TestMethod]
        public void GetFeaturedReturns3UsersOrderedByUsersEndorsementsCount()
        {
            var fakeField = new FieldOfExpertise { Name = "fake" };
            var fakeEndorsement = new List<EndorsementOfUser>();
            for (int i = 0; i < 4; i++)
            {
                fakeEndorsement.Add(new EndorsementOfUser { Value = i });
            }

            var fakeUsers = new List<User>();
            for (int i = 0; i < 4; i++)
            {
                fakeUsers.Add(new User { 
                    FirstName = "User" + i,
                    LastName = "SomeOne",
                    FieldsOfExpertise = new List<FieldOfExpertise> { fakeField }
                });
            }

            fakeUsers[3].UsersEndorsements = new List<EndorsementOfUser> 
            {
                fakeEndorsement[0], fakeEndorsement[1], 
                fakeEndorsement[2]
            };
            fakeUsers[2].UsersEndorsements = new List<EndorsementOfUser> 
            {
                fakeEndorsement[0], fakeEndorsement[1],
                fakeEndorsement[2], fakeEndorsement[3]
            };
            fakeUsers[1].UsersEndorsements = new List<EndorsementOfUser> 
            {
                fakeEndorsement[0], fakeEndorsement[1]
            };
            fakeUsers[0].UsersEndorsements = new List<EndorsementOfUser> 
            {
                fakeEndorsement[0]
            };

            var fakeUsersQueryable = fakeUsers.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<User>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeUsersQueryable);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Users).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<User>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<User>>>()))
                .Returns(() => fakeUsersQueryable
                .Where(u => u.IsDeleted == false)
                .Where(u => u.FieldsOfExpertise.Count > 0)
                .OrderByDescending(u => u.UsersEndorsements.Count)
                .Take(3)
                .AsQueryable());

            var homeServices = new HomeServices(data.Object, cache.Object);

            var usersResult = homeServices.GetFeatured();

            CollectionAssert.AreEquivalent(
                new List<string> { 
                    "User2", "User3", "User1",
                },
            usersResult.Select(i => i.FirstName).ToList<string>());
        }

        [TestMethod]
        public void GetFeaturedGetsAllUserssWhenLessThan3()
        {
            var fakeField = new FieldOfExpertise { Name = "fake" };
            var fakeEndorsement = new List<EndorsementOfUser>();
            for (int i = 0; i < 2; i++)
            {
                fakeEndorsement.Add(new EndorsementOfUser { Value = i });
            }

            var fakeUsers = new List<User> {
                new User
                {
                    FirstName = "UserA",
                    LastName = "SomeOne",
                    FieldsOfExpertise = new List<FieldOfExpertise> { fakeField },
                    UsersEndorsements = new List<EndorsementOfUser> 
                    {
                        fakeEndorsement[0], fakeEndorsement[1], 
                    }
                },
                new User
                {
                    FirstName = "UserB",
                    LastName = "SomeOne",
                    FieldsOfExpertise = new List<FieldOfExpertise> { fakeField },
                    UsersEndorsements = new List<EndorsementOfUser> 
                    {
                        fakeEndorsement[0], 
                    }
                },
            };

            var fakeUsersQueryable = fakeUsers.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<User>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeUsersQueryable);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Users).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<User>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<User>>>()))
                .Returns(() => fakeUsersQueryable
                .Where(u => u.IsDeleted == false)
                .Where(u => u.FieldsOfExpertise.Count > 0)
                .OrderByDescending(u => u.UsersEndorsements.Count)
                .Take(3)
                .AsQueryable());

            var homeServices = new HomeServices(data.Object, cache.Object);

            var usersResult = homeServices.GetFeatured();

            CollectionAssert.AreEquivalent(
                new List<string> { 
                    "UserA", "UserB"
                },
            usersResult.Select(i => i.FirstName).ToList<string>());
        }

        [TestMethod]
        public void GetFeaturedGets0UsersWhenNoUsers()
        {

            var fakeUsers = new List<User>();

            var fakeUsersQueryable = fakeUsers.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<User>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeUsersQueryable);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Users).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<User>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<User>>>()))
                .Returns(() => fakeUsersQueryable
                .Where(u => u.IsDeleted == false)
                .Where(u => u.FieldsOfExpertise.Count > 0)
                .OrderByDescending(u => u.UsersEndorsements.Count)
                .Take(3)
                .AsQueryable());

            var homeServices = new HomeServices(data.Object, cache.Object);

            var usersResult = homeServices.GetFeatured();

            Assert.AreEqual(0, usersResult.Count());
        }
    }
}
