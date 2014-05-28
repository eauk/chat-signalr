using System.Collections.Generic;
using chat.Models;

namespace chat.Repository
{
    public interface IUserRepo
    {
        void Add(User user);
        IEnumerable<User> GetOnlineUsers();
        string SetUserOffline(string name);
        User FindUser(string name, string pass);
        User CheckUser(string name, string email);
        void Save();
    }
}