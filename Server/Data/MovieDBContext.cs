using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class MovieDBContext:IdentityDbContext
    {
        public MovieDBContext(DbContextOptions<MovieDBContext>options):base(options)
        {
        }

        public DbSet<MovieComment> MovieComments { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}
