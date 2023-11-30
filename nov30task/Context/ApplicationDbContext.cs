using Microsoft.EntityFrameworkCore;
using nov30task.Models;

namespace nov30task.Context
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Slider> Sliders { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-OG8IRC4\SQLEXPRESS;Database=Pustok;Trusted_Connection=true");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
