using Microsoft.EntityFrameworkCore;
using SIgnalRChatApps.Models;

namespace SIgnalRChatApps.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Chat> Chats { get; set; } = default!;
    }
}
