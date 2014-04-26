using System.Collections.Generic;
using chat.Models;

namespace chat.Repository
{
    public interface IUserRepo
    {
        void Add(User user);
        IEnumerable<User> GetOnlineUsers();
        string SetUserOffline(string name); 
    }
}