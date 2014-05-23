using System.Collections.Generic;
using System.Linq;
using chat.Models;

namespace chat.Repository
{
    public class UserRepo : IUserRepo
    {
        private DataContext context;

        public UserRepo(DataContext context)
        {
            this.context = context;
        }

        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public IEnumerable<User> GetOnlineUsers()
        {
            return context.Users.Where(r => r.IsOnline).ToList();
        }

        public User FindUser(string name, string pass)
        {
            return context.Users.FirstOrDefault(r => r.Name == name && r.Password == pass);
        }

        public string SetUserOffline(string connectionId)
        {
            var user = context.Users.FirstOrDefault(r => r.ConnectionId == connectionId);
            if (user != null)
            {
                user.IsOnline = false;
                context.SaveChanges();
                return user.Name;
            }
            return string.Empty;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}