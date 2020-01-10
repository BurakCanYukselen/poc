using Logger.Logger;
using System;
using System.Diagnostics;
using System.Linq;

namespace Logger
{

    class Program
    {
        static void Main(string[] args)
        {
            var tmr = new Stopwatch();
            tmr.Start();

            var dbLogEnabled = args.Contains("1");
            Console.WriteLine($"dbLogEnabled: {dbLogEnabled}");
            Startup.Init();

            var consoleLogger = Startup.GetRequiredService<ConsoleLogger>();
            var databaseLogger = Startup.GetRequiredService<DatabaseLogger>();

            var log = new Log()
            {
                ProcessId = Guid.NewGuid(),
                OperationName = "Test",
            };

            for (int i = 1; i <= 1000000; i++)
            {
                log.Message = $"{log.OperationName} - {i}";
                log.StartedAt = DateTimeOffset.Now;

                consoleLogger.Log(log);
                if (dbLogEnabled && i % 50000 == 0)
                    databaseLogger.Log(log);

            }

            tmr.Stop();

            Console.WriteLine($"Time Ellapsed - {tmr.Elapsed.TotalSeconds}");
            while (Console.ReadKey().Key != ConsoleKey.E) { };
        }
    }




}
