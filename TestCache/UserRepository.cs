using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheAspect;
using CacheAspect.Attributes;

namespace TestCache
{
    public class UserRepository
    {
        public IUserDal Dal{get; set;}

        //Get All Users is cached in Key = "GetAllUsers"
        [Cache.Cacheable("GetAllUsers")] 
        public List<User> GetAllUsers()
        {
            return Dal.GetAllUsers();
        }

        //GetUserById is cached using "GetUserById" + ID parameter
        [Cache.Cacheable("GetUserById")]
        public User GetUserById(int Id)
        {
            return Dal.GetUserById(Id);
        }

        //Add user invalidates "GetAllUsers" cache key (User parameter is ignored)
        [Cache.TriggerInvalidation("GetAllUsers", CacheSettings.IgnoreParameters)]
        public void AddUser(User user)
        {
            Dal.AddUser(user);
        }

        //Delete user invalidates both GetAllUsers & GetUserById
        //The user parameters Id property is used to build Key for "GetUserById"+ Id  Key
        //this is done using a bit reflection
        [Cache.TriggerInvalidation("GetAllUsers", CacheSettings.IgnoreParameters)]
        [Cache.TriggerInvalidation("GetUserById", CacheSettings.UseId)]
         public void DeleteUser(User user)
        {
            Dal.DeleteUser(user);

            var f = new {Id = 4, Name = "NAme"};
        }

    }
}
