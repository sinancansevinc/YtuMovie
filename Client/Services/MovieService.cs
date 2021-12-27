using Client.Static;
using Shared.Models;
using Shared.ViewModels;
using System.Net.Http.Json;

namespace Client.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient httpClient;
        public MovieService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IList<Genre>> GetGenres()
        {
            var result = await httpClient.GetFromJsonAsync<IList<Genre>>(APIEndpoints.s_getGenres);

            return result;
        }

        public async Task<IList<CommentViewModel>> GetMovieComments(int id)
        {
            var url = APIEndpoints.s_getMovieComments + id.ToString();
            var result = await httpClient.GetFromJsonAsync<IList<CommentViewModel>>(url);

            return result;
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            var movie = await httpClient.GetFromJsonAsync<Movie>(APIEndpoints.s_movieDetail.Replace("movieId", movieId.ToString()));
            return movie;
        }

        public async Task<MovieRoot> GetMovieRoot(string apiUrl)
        {
            var result = await httpClient.GetFromJsonAsync<MovieRoot>(apiUrl);

            return result;
        }

        public async Task<VideoRoot> GetVideoRoot(int movieId)
        {
            var result = await httpClient.GetFromJsonAsync<VideoRoot>(APIEndpoints.s_videoUrl.Replace("movieId", movieId.ToString()));
            return result;
        }

        public async Task<HttpResponseMessage> SignIn(User user)
        {
            var result = await httpClient.PostAsJsonAsync(APIEndpoints.s_signin, user);
            return result;

        }
        public async Task<HttpResponseMessage> AddComment(MovieComment movieComment)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(APIEndpoints.s_addComment, movieComment);

            return response;
        }

        public async Task<HttpResponseMessage> Register(User user)
        {
            var result = await httpClient.PostAsJsonAsync(APIEndpoints.s_register, user);
            return result;
        }
    }
}
