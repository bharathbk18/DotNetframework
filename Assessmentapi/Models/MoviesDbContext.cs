using Microsoft.EntityFrameworkCore;

namespace Assessmentapi.Models
{
    public class MoviesDbContext:DbContext
    {
        
public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {

        }
        public DbSet<Userdetails> Userdetail { get; set; }
        public DbSet<Movies> Movie { get; set; }
       
    }
}
