using Shared.Models;
using Shared.ViewModels;

namespace Client.Services
{
    public interface IMovieService
    {
        Task<IList<CommentViewModel>> GetMovieComments(int id);
        Task<HttpResponseMessage>AddComment(MovieComment movieComment);
    }
}
