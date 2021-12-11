namespace Server.Logging
{
    public class CustomLogger : ILogger
    {

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message =$"[{DateTime.UtcNow}] - {logLevel.ToString()} - {formatter(state, exception)}";
            WriteMessageToFile(message);
        }
        private static void WriteMessageToFile(string message)
        {
            const string filePath = $"Logs/log.txt";
            using (var streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

    }

}
