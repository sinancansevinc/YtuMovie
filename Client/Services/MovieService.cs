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

        public async Task<HttpResponseMessage>  AddComment(MovieComment movieComment)
        {
           HttpResponseMessage response = await httpClient.PostAsJsonAsync(APIEndpoints.s_addComment, movieComment);

            return response;
        }

        public async Task AddGenres()
        {
            
            var result =await httpClient.GetFromJsonAsync<GenreRoot>("https://api.themoviedb.org/3/genre/movie/list?api_key=3b25acea89bf65c5da6ff5d06c6f0312&language=en-US");

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(APIEndpoints.s_addGenres,result);
        }

        public async Task<IList<CommentViewModel>> GetMovieComments(int id)
        {
            var url=APIEndpoints.s_getMovieComments+id.ToString();
            var result= await  httpClient.GetFromJsonAsync<IList<CommentViewModel>>(url);

            return result;
        }
    }
}
