using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOS.LearningStyle.FinalYear.Core.Loggings
{
    public class FileLogger
    {
        private static readonly ILogger _diagnosticLogger;

        static FileLogger()
        {
            _diagnosticLogger = new LoggerConfiguration().ReadFrom.AppSettings()
                                                    .Enrich.FromLogContext()
                                                    .CreateLogger();
        }

        public static void Information(string message)
        {
            Log.Information(message);
        }

        public static void Warning(string message)
        {
            Log.Warning(message);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }
    }
}
