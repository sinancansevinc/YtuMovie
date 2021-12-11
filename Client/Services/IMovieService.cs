using Shared.Models;

namespace Client.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieComment>> GetMovieComments(int id);
    }
}
