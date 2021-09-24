using ChatAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI
{
    public class DbService : DbContext
    {
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatsList> ChatsList { get; set; }
        public DbService(DbContextOptions<DbService> options) : base(options)
        { }
    }
}
