using Shared.Models;
using Shared.ViewModels;

namespace Client.Services
{
    public interface IMovieService
    {
        Task<IList<CommentViewModel>> GetMovieComments(int id);
        Task<IList<Genre>> GetGenres();
        Task<HttpResponseMessage> AddComment(MovieComment movieComment);
        Task<HttpResponseMessage> SignIn(User user);
        Task<HttpResponseMessage> Register(User user);
        Task<MovieRoot> GetMovieRoot(string apiUrl);
        Task<Movie> GetMovie(int movieId);
        Task<VideoRoot> GetVideoRoot(int movieId);

    }
}
