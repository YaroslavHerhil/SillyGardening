using Microsoft.EntityFrameworkCore;
using SillyGardening.Core.Models;

namespace SillyGardening.DAL
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
    }
}
