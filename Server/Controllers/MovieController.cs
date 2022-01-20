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

        public MovieController(MovieDBContext context, ILogger<MovieController> logger)
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
            Thread.Sleep(1000);
            return Ok(movieComment);
        }
        [Route("delete")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] CommentViewModel commentViewModel)
        {
            MovieComment movieComment = new MovieComment();
            movieComment = context.MovieComments.FirstOrDefault(p => p.Id == commentViewModel.CommentId);

            if (movieComment != null)
            {
                context.MovieComments.Remove(movieComment);
                await context.SaveChangesAsync();
                return Ok();

            }

            return BadRequest();
        }




        [HttpGet("getcomments/{id}")]
        [AllowAnonymous]
        public IList<CommentViewModel> GetMovieComments(int id)
        {
            List<CommentViewModel> commentViewModels = new List<CommentViewModel>();
            List<MovieComment> movieComments = new List<MovieComment>();
            try
            {

                //movieComments = context.MovieComments.Where(comment => comment.MovieId == id).ToList();

                //if (movieComments.Count > 0)
                //{
                    commentViewModels = (from p in context.Users
                              join e in context.MovieComments.Where(x=>x.MovieId==id) on p.Id equals e.UserId 
                              select new CommentViewModel
                              {
                                  UserName = p.UserName,
                                  CommentDate = e.CreateDate,
                                  Comment = e.Comment,
                                  CommentId = e.Id

                              }).ToList();

                //    return ap;
                //}
                //else
                //{
                return commentViewModels;
                //}
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
