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

        public async Task<IList<CommentViewModel>> GetMovieComments(int id)
        {
            var url=APIEndpoints.s_getMovieComments+id.ToString();
            var result= await  httpClient.GetFromJsonAsync<IList<CommentViewModel>>(url);

            return result;
        }
    }
}
