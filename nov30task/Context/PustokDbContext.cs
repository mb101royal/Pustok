using Microsoft.EntityFrameworkCore;
using nov30task.Models;

namespace nov30task.Context
{
    public class PustokDbContext : DbContext
    {
        public PustokDbContext(DbContextOptions opt) : base(opt) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }    
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
