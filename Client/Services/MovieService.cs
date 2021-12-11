using Client.Static;
using Shared.Models;
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
        public async Task<IEnumerable<MovieComment>> GetMovieComments(int id)
        {
            return await  httpClient.GetFromJsonAsync<IEnumerable<MovieComment>>(APIEndpoints.s_getMovieComments);
        }
    }
}
