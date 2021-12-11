using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDBContext context;
        private readonly ILogger<MovieController> logger;

        public MovieController(MovieDBContext context,ILogger<MovieController>logger)
        {
            this.context = context;
            this.logger = logger;
        }

        //[HttpPost]
        //public void AddComment(MovieComment movieComment)
        //{
        //    context.Add(movieComment);
        //}

        [HttpGet("getcomments/{id}")]
        public IEnumerable<MovieComment> GetMovieComments(int id)
        {
            try
            {
                return context.MovieComments.Where(comment => comment.Id == id).ToList();
                //return movieService.GetMovieComments(id);
            }
            catch (Exception)
            {
                logger.LogInformation("There is an error at MovieController>GetComments");
                throw;
            }
        }
    }
}
