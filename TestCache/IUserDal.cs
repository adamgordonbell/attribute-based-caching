using System;
namespace TestCache
{
    public interface IUserDal
    {
        void AddUser(User u);
        void DeleteUser(User u);
        System.Collections.Generic.List<User> GetAllUsers();
        User GetUserById(int id);
    }
}
