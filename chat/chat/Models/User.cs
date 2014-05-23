using System;

namespace chat.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Ava { get; set; }
        public bool IsOnline { get; set; }
        public string ConnectionId { get; set; } 
    }
}