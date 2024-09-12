using Microsoft.EntityFrameworkCore;
using proj_int.Models;

namespace proj_int.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User>  Users { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Event> Events { get; set; }

    }
}
