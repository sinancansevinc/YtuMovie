namespace Server.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {

        public ILogger CreateLogger(string categoryName) => new CustomLogger();
        public void Dispose() => throw new NotImplementedException();



    }
}
