using System;
using System.Diagnostics;
using System.Threading;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {

            var logEnabled = 0;
            while (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                for (int i = 1; i <= 10; i++)
                {
                    var proc = Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"C:\Development\POC\RestaurantIssues.POC\Logger\bin\Debug\netcoreapp3.1\3-Logger.exe",
                        Arguments = $"{logEnabled}",
                        CreateNoWindow = false,
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Normal,
                    });
                    Thread.Sleep(TimeSpan.FromSeconds(65));
                }
            }
        }
    }
}
