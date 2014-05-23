using System;
using System.Linq;
using System.Threading.Tasks;
using chat.Models;
using chat.Repository;
using Microsoft.AspNet.SignalR;

namespace chat.Hubs
{
    public class ChatHub : Hub
    {
        private IUserRepo _userRepo;
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            if (!String.IsNullOrWhiteSpace(message))
                Clients.All.broadcastMessage(name, message.Trim());
        }

        public void SetOnline(string name, string pass)
        {
            _userRepo = new UserRepo(new DataContext());
            var user = _userRepo.FindUser(name, pass);
            if (user == null)
            {
                _userRepo.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Password = pass,
                    IsOnline = true,
                    Ava = "empty.png",
                    ConnectionId = Context.ConnectionId
                });
            }
            else
            {
                user.IsOnline = true;
                _userRepo.Save();
            }

            var onlineNames = string.Concat(_userRepo.GetOnlineUsers().Select(r => r.Name + "|").ToArray());

            Clients.All.broadcastMessage(name, name + " вошел в чат");
            Clients.All.setUserOnline(onlineNames);
        }


        private void SetOffline(string connectionId)
        {
            _userRepo = new UserRepo(new DataContext());
            var name = _userRepo.SetUserOffline(connectionId);

            var onlineNames = string.Concat(_userRepo.GetOnlineUsers().Select(r => r.Name + "|").ToArray());

            Clients.All.broadcastMessage(name, name + " покинул чат");
            Clients.All.setUserOnline(onlineNames);
        }

        public override Task OnDisconnected()
        {
            SetOffline(Context.ConnectionId);
            return base.OnDisconnected();
        }
    }
}