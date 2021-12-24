using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly MovieDBContext context;

        public GenreController(MovieDBContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [Route("GetGenres")]
        [AllowAnonymous]
        public async Task<IList<Genre>> Get()
        {
            var result = context.Genres.ToList();
            return result;

        }
    }
}
