using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Professional.Data;
using Professional.Web.Infrastructure.Caching;
using Professional.Web.Infrastructure.Services;
using System.Linq;
using System.Collections.Generic;
using Professional.Models;
using Professional.Data.Contracts;
using FakeDbSet;

namespace Professional.Tests
{
    [TestClass]
    public class ListingServicesTests
    {
        // GetLetters Tests
        [TestMethod]
        public void GetLettersReturnsEnglishLettersTests()
        {
            var fakeFields = new InMemoryDbSet<FieldOfExpertise> {
                new FieldOfExpertise { Name = "A" },
                new FieldOfExpertise { Name = "B" },
                new FieldOfExpertise { Name = "C" },
            };

            var dbContext = new Mock<IApplicationDbContext>();
            dbContext.Setup(f => f.Set<FieldOfExpertise>()).Returns(fakeFields);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Context).Returns(dbContext.Object);

            var cache = new Mock<ICacheService>();
            var listingServices = new ListingServices(data.Object, cache.Object);

            var letters = listingServices.GetLetters<FieldOfExpertise>("Name");
            var testLetters = new List<string>
            { "A", "B", "C", };

            CollectionAssert.AreEquivalent(testLetters, letters.ToList());
        }

        // GetFields Tests
        [TestMethod]
        public void GetFeildsReturnsFieldsCorrectlyTest()
        {
            var fakeFields = new List<FieldOfExpertise> {
                new FieldOfExpertise { Name = "testObjA" },
                new FieldOfExpertise { Name = "testObjB" },
                new FieldOfExpertise { Name = "testObjC" },
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<FieldOfExpertise>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeFields);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.FieldsOfExpertise).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<string>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<string>>>()))
                .Returns(() => fakeFields.Select(i => i.Name).AsQueryable<string>());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetFeilds();

            CollectionAssert.AreEquivalent(fakeFields.Select(i => i.Name).ToList(),
                fieldsResult.ToList());
        }

        [TestMethod]
        public void GetFeildsReturnsNoElementsWhenTheyAreDeletedTest()
        {
            var fakeFields = new List<FieldOfExpertise> {
                new FieldOfExpertise { Name = "testObjA", IsDeleted = true },
                new FieldOfExpertise { Name = "testObjB", IsDeleted = true },
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<FieldOfExpertise>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeFields);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.FieldsOfExpertise).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<string>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<string>>>()))
                .Returns(() => fakeFields.Where(f => !f.IsDeleted).Select(i => i.Name).AsQueryable<string>());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetFeilds();

            Assert.AreEqual(0, fieldsResult.Count());
        }

        // GetUsers Tests
        [TestMethod]
        public void GetUsersReturnsCorrectResultNoFilterTest()
        {
            var fakeUsers = new List<User> {
                new User { 
                    FirstName = "A",
                    LastName = "A"
                },
                new User { 
                    FirstName = "B",
                    LastName = "B"
                }
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<User>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeUsers);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Users).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<User>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<User>>>()))
                .Returns(() => fakeUsers.Where(i => i.IsDeleted == false).AsQueryable());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetUsers(null, null);

            Assert.AreEqual(2, fieldsResult.Count());
        }

        [TestMethod]
        public void GetUsersReturnsCorrectResultWithFilterTest()
        {
            var fakeUsers = new List<User> {
                new User { 
                    FirstName = "A",
                    LastName = "A"
                },
                new User { 
                    FirstName = "B",
                    LastName = "B"
                }
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<User>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeUsers);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Users).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<User>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<User>>>()))
                .Returns(() => fakeUsers.Where(i => i.IsDeleted == false).AsQueryable());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetUsers("A", null);

            Assert.AreEqual(1, fieldsResult.Count());
        }

        [TestMethod]
        public void GetUsersReturnsCorrectResultWithUser()
        {

            var userConnections = new List<Connection> {
                new Connection {
                    FirstUserId = "abc",
                    SecondUserId = "def",
                    IsAccepted = true
                }
            };

            var fakeUsers = new List<User> {
                new User { 
                    FirstName = "A",
                    LastName = "A",
                    Connections = userConnections
                },
                new User { 
                    FirstName = "B",
                    LastName = "B"
                }
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<User>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeUsers);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Users).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<User>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<User>>>()))
                .Returns(() => fakeUsers.AsQueryable());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetUsers(null, "abc");

            Assert.AreEqual(1, fieldsResult.Count());
        }

        [TestMethod]
        public void GetUsersReturnsCorrectResultWithUserAndFilter()
        {

            var userConnections = new List<Connection> {
                new Connection {
                    FirstUserId = "abc",
                    SecondUserId = "def",
                    IsAccepted = true
                }
            };

            var fakeUsers = new List<User> {
                new User { 
                    FirstName = "A",
                    LastName = "A",
                    Connections = userConnections
                },
                new User { 
                    FirstName = "B",
                    LastName = "B",
                    Connections = userConnections
                }
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<User>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeUsers);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Users).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<User>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<User>>>()))
                .Returns(() => fakeUsers.AsQueryable());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetUsers("A", "abc");

            Assert.AreEqual(1, fieldsResult.Count());
        }

        // GetPosts Tests
        [TestMethod]
        public void GetPostsReturnsCorrectResultNoFilterNoUserTest()
        {
            var fakePosts = new List<Post> {
                new Post { 
                    Title = "A",
                    Content = "A",
                    CreatorID = "Any",
                    FieldID = 1,
                    CreatedOn = DateTime.Now
                },
                new Post { 
                    Title = "B",
                    Content = "B",
                    CreatorID = "Any",
                    FieldID = 2,
                    CreatedOn = DateTime.Now
                },
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<Post>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakePosts);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Posts).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<Post>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<Post>>>()))
                .Returns(() => fakePosts.AsQueryable());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetPosts(null, null);

            Assert.AreEqual(2, fieldsResult.Count());
        }

        [TestMethod]
        public void GetPostsReturnsCorrectResultNoFilterWithUserTest()
        {
            var fakePosts = new List<Post> {
                new Post { 
                    Title = "A",
                    Content = "A",
                    CreatorID = "Selected",
                    FieldID = 1,
                    CreatedOn = DateTime.Now
                },
                new Post { 
                    Title = "B",
                    Content = "B",
                    CreatorID = "Any",
                    FieldID = 2,
                    CreatedOn = DateTime.Now
                },
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<Post>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakePosts);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Posts).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<Post>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<Post>>>()))
                .Returns(() => fakePosts.Where(i => i.IsDeleted == false).AsQueryable());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetPosts(null, "Selected");

            Assert.AreEqual(1, fieldsResult.Count());
            Assert.AreEqual("A", fieldsResult.First().Title);
        }

        [TestMethod]
        public void GetPostsReturnsCorrectResultWithFilterNoUserTest()
        {
            var fieldA = new FieldOfExpertise { ID = 1, Name = "FieldA" };
            var fieldB = new FieldOfExpertise { ID = 2, Name = "FieldB" };
            var fakePosts = new List<Post> {
                new Post { 
                    Title = "A",
                    Content = "A",
                    CreatorID = "Any",
                    Field = fieldA,
                    CreatedOn = DateTime.Now
                },
                new Post { 
                    Title = "B",
                    Content = "B",
                    CreatorID = "Any",
                    Field = fieldB,
                    CreatedOn = DateTime.Now
                },
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<Post>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakePosts);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.Posts).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<Post>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<Post>>>()))
                .Returns(() => fakePosts.AsQueryable());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetPosts("FieldA", null);

            Assert.AreEqual(1, fieldsResult.Count());
            Assert.AreEqual("A", fieldsResult.First().Title);
        }

        // GetEndorsements Tests
        [TestMethod]
        public void GetEndorsementsReturnsCorrectResultTest()
        {
            var fakeEndorsements = new List<EndorsementOfUser> {
                new EndorsementOfUser { 
                    Value = 1,
                    EndorsedUserID = ""
                },
                new EndorsementOfUser { 
                    Value = 5,
                    EndorsedUserID = "someID"
                },
                new EndorsementOfUser { 
                    Value = 3,
                    EndorsedUserID = "someID"
                },
            }.AsQueryable();

            var fieldsRepo = new Mock<IDeletableEntityRepository<EndorsementOfUser>>();
            fieldsRepo.Setup(f => f.All()).Returns(fakeEndorsements);

            var data = new Mock<IApplicationData>();
            data.Setup(f => f.EndorsementsOfUsers).Returns(fieldsRepo.Object);

            var cache = new Mock<ICacheService>();
            cache.Setup(c => c.Get<IQueryable<EndorsementOfUser>>(It.IsAny<string>(), It.IsAny<Func<IQueryable<EndorsementOfUser>>>()))
                .Returns(() => fakeEndorsements.AsQueryable());

            var listingServices = new ListingServices(data.Object, cache.Object);

            var fieldsResult = listingServices.GetEndorsements("someID");

            Assert.AreEqual(2, fieldsResult.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetEndorsementsThrowsExceptionIfUserIdIsNullTest()
        {
            var data = new Mock<IApplicationData>();
            var cache = new Mock<ICacheService>();
            var listingServices = new ListingServices(data.Object, cache.Object);
            var fieldsResult = listingServices.GetEndorsements(null);
        }
    }
}
