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

        [HttpPost]
        public void AddComment(MovieComment movieComment)
        {
            movieComment.CreateDate=DateTime.Now;
            context.Add(movieComment);
            context.SaveChanges();
        }

        [HttpGet("getcomments/{id}")]
        public IEnumerable<MovieComment> GetMovieComments(int id)
        {
            try
            {
                var comments=context.MovieComments.Where(comment => comment.Id == id).ToList();

                var ap = (from p in context.Users
                          join e in context.MovieComments on p.Id equals e.UserId
                          select new CommentViewModel
                          {
                              UserName = p.UserName,
                              CommentDate = e.CreateDate
                          }).ToList();



                return comments;
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
