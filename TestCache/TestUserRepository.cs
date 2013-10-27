using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CacheAspect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace TestCache
{
    [TestClass]
    public class TestUserRepository
    {

        UserRepository target;
        IUserDal dal;
        ICache cache;

        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void SetUp()
        {
            dal = MockRepository.GenerateStrictMock<IUserDal>();
            cache = new InProcessMemoryCache();

            CacheService.Cache = cache;
            target = new UserRepository();
            target.Dal = dal;
        }

        [TestMethod]
        public void GetAllUsers_TryRetrievingDataTwice_DalShouldBeHitOnce()
        {
            //Arrange
            dal.Expect(d => d.GetAllUsers()).Return(GetUsers());

            //Act
            target.GetAllUsers();
            target.GetAllUsers();

            //Assert
            dal.VerifyAllExpectations();
            
        }

        [TestMethod]
        public void GetUserById_TryRetrievingDataTwice_DalShouldBeHitOnce()
        {
            //Arrange
            int id = 1;
            dal.Expect(d => d.GetUserById(id)).Return(GetUsers().First());

            //Act
            target.GetUserById(id);
            target.GetUserById(id);

            //Assert
            dal.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetUserById_TryRetrievingUsingDifferentIds_DalShouldBeHitTwice()
        {
            //Arrange
            int id1 = 1;
            int id2 = 2;
            dal.Expect(d => d.GetUserById(id1)).Return(GetUsers().First());
            dal.Expect(d => d.GetUserById(id2)).Return(GetUsers().Last());

            //Act
            target.GetUserById(id1);
            target.GetUserById(id2);

            //Assert
            dal.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAllUsers_AddUserAfterCaching_CacheShouldBeInvalidated()
        {
            //Arrange
            dal.Expect(d => d.GetAllUsers())
                .Return(GetUsers())
                .Repeat.Twice();                    //Second call expected after cache is invalidated
            dal.Expect(d => d.AddUser(null)).IgnoreArguments();
            
            //Act
            target.GetAllUsers();
            target.AddUser(new User{ Id = 1234});   //Should trigger invalidation
            target.GetAllUsers();

            //Assert
            dal.VerifyAllExpectations();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private List<User> GetUsers()
        {
            return new List<User>{ 
            new User{ Id = 1},
            new User{ Id = 2}
        };
        }
    }
}
