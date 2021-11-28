namespace Server.Logging
{
    public class CustomLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {

            return null;

        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string fileName = DateTime.UtcNow.ToString()
              .Replace(" ", "")
              .Replace(":", "")
              .Replace(".", "") + "log";
            using (StreamWriter streamWriter = new StreamWriter($"{Environment.CurrentDirectory}/Logs/{fileName}.txt", true))
            {
                string logMsg = formatter(state, exception);

                await streamWriter.WriteLineAsync($"[{DateTime.UtcNow}] - Event Name : {eventId.Name} -  {logMsg}");
                streamWriter.Close();
                await streamWriter.DisposeAsync();
            }
        }
    }

}
