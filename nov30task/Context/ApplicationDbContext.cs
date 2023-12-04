using Microsoft.EntityFrameworkCore;
using nov30task.Models;

namespace nov30task.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions opt) : base(opt) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
