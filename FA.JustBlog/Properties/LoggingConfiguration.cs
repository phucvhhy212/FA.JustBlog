using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using log4net;

namespace FA.JustBlog.Properties
{
    internal static class LogConfiguration
    {
        internal static void SetupLog4net()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();

            var rollingAppender = GetRollingAppender();
            var consoleAppender = GetConsoleAppender();

            hierarchy.Root.AddAppender(consoleAppender);
            hierarchy.Root.AddAppender(rollingAppender);

            hierarchy.Root.Level = Level.Debug;
            hierarchy.Configured = true;

            var logger = LogManager.GetLogger(typeof(LogConfiguration));
            logger.Info($"Init Log4net successfully.");
        }

        private static ConsoleAppender GetConsoleAppender()
        {
            var patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "[%-40.60logger{3}] - %message%newline";
            patternLayout.ActivateOptions();

            var consoler = new ConsoleAppender();
            consoler.Layout = patternLayout;
            consoler.Threshold = Level.Info;
            return consoler;
        }

        private static RollingFileAppender GetRollingAppender()
        {
            var patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "[%date{yyyy-MM-dd HH:mm:ss.fff}] [%-5level] [%-60.60logger{3}] - %message%newline";
            patternLayout.ActivateOptions();

            var roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = @"Logs\Log4net.log";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "2MB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            return roller;
        }
    }
}
