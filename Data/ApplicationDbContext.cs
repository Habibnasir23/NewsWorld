using Microsoft.EntityFrameworkCore;

namespace NewsWorld.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NewsWorld.Models.News> News { get; set; }
    }
}
