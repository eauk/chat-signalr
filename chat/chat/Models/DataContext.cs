using System.Data.Entity;

namespace chat.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base(@"Data Source=.\SQLEXPRESS;Initial Catalog=SignalRChatDb; Integrated Security=SSPI; User ID = sa; Password = 1")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}