using Shared.Models;
using Shared.ViewModels;

namespace Client.Services
{
    public interface IMovieService
    {
        Task<IList<CommentViewModel>> GetMovieComments(int id);
        Task AddGenres();
        Task<HttpResponseMessage> AddComment(MovieComment movieComment);
    }
}
