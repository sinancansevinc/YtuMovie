using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Shared.Models;
using Shared.ViewModels;

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

        [Route("add")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MovieComment movieComment)
        {
            movieComment.CreateDate = DateTime.Now;
            await context.AddAsync(movieComment);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("getcomments/{id}")]
        public IList<CommentViewModel> GetMovieComments(int id)
        {
            try
            {
                var comments=context.MovieComments.Where(comment => comment.Id == id).ToList();

                var ap = (from p in context.Users
                          join e in context.MovieComments on p.Id equals e.UserId
                          select new CommentViewModel
                          {
                              UserName = p.UserName,
                              CommentDate = e.CreateDate,
                              Comment=e.Comment
                              
                          }).ToList();




                return ap;
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
