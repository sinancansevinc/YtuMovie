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

        //[HttpPost]
        //[Route("add")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Add(GenreRoot root)
        //{
        //    foreach (var item in root.genres)
        //    {
        //        var genreItem = new Genre();
        //        genreItem.Id = item.Id;
        //        genreItem.Name = item.Name;
        //        await context.AddAsync(genreItem);
        //        await context.SaveChangesAsync();
        //    }

        //    return Ok();
        //}

        [HttpGet]
        [Route("add")]
        [AllowAnonymous]
        public async Task<List<Genre>> Get()
        {

            return context.Genres.ToList();

        }
    }
}
