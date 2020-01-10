using System;

namespace Logger.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(Log log)
        {
            var logString = $"{log.ProcessId} -> {log.OperationName} -> {log.Message} -> {log.StartedAt}";
            Console.WriteLine(logString);
        }
    }
}
