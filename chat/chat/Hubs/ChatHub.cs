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
        private IUserRepo userRepo;
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            if (!String.IsNullOrWhiteSpace(message))
                Clients.All.broadcastMessage(name, message.Trim());
        }

        public void SetOnline(string name)
        {
            userRepo = new UserRepo(new DataContext());
            userRepo.Add(new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                IsOnline = true,
                ConnectionId = Context.ConnectionId
            });

            var onlineNames = string.Concat(userRepo.GetOnlineUsers().Select(r => r.Name + "|").ToArray());

            Clients.All.broadcastMessage(name, name + " вошел в чат");
            Clients.All.setUserOnline(onlineNames);
        }


        public void SetOffline(string connectionId)
        {
            userRepo = new UserRepo(new DataContext());
            var name = userRepo.SetUserOffline(connectionId);

            var onlineNames = string.Concat(userRepo.GetOnlineUsers().Select(r => r.Name + "|").ToArray());

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