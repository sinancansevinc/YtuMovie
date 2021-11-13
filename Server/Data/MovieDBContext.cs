using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class MovieDBContext:IdentityDbContext
    {
        public MovieDBContext(DbContextOptions<MovieDBContext>options):base(options)
        {

        }
    }
}
