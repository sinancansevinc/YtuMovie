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

    }
}