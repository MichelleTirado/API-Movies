using API_Movies.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Movies.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        //List all models
        public DbSet<Category> Category { get; set; }
        public DbSet<Movie> Movie { get; set; }
    }
}
