namespace Client.Static
{
    public static class APIEndpoints
    {
#if DEBUG
        internal const string ServerBaseUrl = "https://localhost:7021";

#else
        internal const string ServerBaseUrl = "https://appservice.azurewebsites.net";
#endif
        internal readonly static string s_register = $"{ServerBaseUrl}/api/user/register";
        internal readonly static string s_signin = $"{ServerBaseUrl}/api/user/signin";
        internal readonly static string s_weatherForecast = $"{ServerBaseUrl}/weatherforecast";
        internal readonly static string s_topRated ="https://api.themoviedb.org/3/movie/top_rated?api_key=3b25acea89bf65c5da6ff5d06c6f0312&language=en-US&page=";
        internal readonly static string s_movieDetail = "https://api.themoviedb.org/3/movie/movieId?api_key=3b25acea89bf65c5da6ff5d06c6f0312&language=en-US";
        internal readonly static string s_startLink = "https://www.youtube.com/embed/";
        internal readonly static string s_videoUrl = "https://api.themoviedb.org/3/movie/movieId/videos?api_key=3b25acea89bf65c5da6ff5d06c6f0312&language=en-US";
        internal readonly static string s_startImg = "https://image.tmdb.org/t/p/w342";

    }
}