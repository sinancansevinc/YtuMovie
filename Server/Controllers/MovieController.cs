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

        public MovieController(MovieDBContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public void AddComment(MovieComment movieComment)
        {
            context.Add(movieComment);
        }
    }
}
