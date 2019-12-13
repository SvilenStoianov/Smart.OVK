using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Smart.Web.Infrastructure
{
    public class MainLogger
    {
        public static readonly MainLogger Instance = new MainLogger();

        private const string LoggerFilePath = "log4net-config.xml";

        private static readonly object SyncLock = new object();

        private static bool IsInitialized;

        private static ILog Log4NetLogger;

        private static MemoryAppender MemoryAppender;

        public static LoggingEvent[] GetLogs()
        {
            return MemoryAppender.GetEvents().ToArray();
        }

        public static void Initialize(LoggerConfigModel config)
        {
            if (IsInitialized)
            {
                return;
            }

            lock (SyncLock)
            {
                if (IsInitialized)
                {
                    return;
                }

                // Configure log4net.
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                
                var configFile = Path.Combine(config.LogRootDirectory, LoggerFilePath);

                using (var memoryStream = new MemoryStream())
                using (var fileStream = File.OpenRead(configFile))
                {
                    var xmlDoc = XDocument.Load(fileStream);

                    var xmlAppenders = xmlDoc.Root.DescendantsAndSelf("appender")
                          .Where(x => x.Attribute("type").Value == "log4net.Appender.RollingFileAppender").ToList();

                    foreach (var appender in xmlAppenders)
                    {
                        var fileElement = appender.DescendantsAndSelf("file").FirstOrDefault();

                        if (fileElement != null)
                        {
                            var valueAttribute = fileElement.Attribute("value");
                            valueAttribute.Value = Path.Combine(config.LogRootDirectory, valueAttribute.Value);
                        }
                    }

                    xmlDoc.Save(memoryStream);

                    memoryStream.Position = 0;

                    XmlConfigurator.Configure(logRepository, memoryStream);

                    var appenders = ((Hierarchy) logRepository).Root.Appenders;

                    foreach (var appender in appenders)
                    {
                        if (appender is MemoryAppender memoryAppender)
                        {
                            MemoryAppender = memoryAppender;
                        }
                    }
                }
                
                Log4NetLogger = LogManager.GetLogger(Assembly.GetEntryAssembly(), "Global logger");

                IsInitialized = true;
            }
        }

        public void LogDebug(string message)
        {
            Log4NetLogger.Debug(message);
        }

        public void LogError(string message)
        {
            Log4NetLogger.Error(message);
        }

        public void LogError(Exception exception)
        {
            var list = GetExceptionChain(exception);

            this.LogError($"Exception was handled. (ExceptionMessage: {exception.Message}, ExceptionType: {string.Join(", ", list.Select(x => x.GetType().Name))})");
        }

        private static List<Exception> GetExceptionChain(Exception exception)
        {
            var list = new List<Exception>();

            while (exception != null)
            {
                list.Add(exception);
                exception = exception.InnerException;
            }

            return list;
        }
    }

    public class LoggerConfigModel
    {
        public string LogRootDirectory { get; set; }
    }

    public class DetailedLogException : Exception
    {
        public DetailedLogException()
        {
            this.Details = new Dictionary<string, object>();
        }

        public DetailedLogException(string message)
            : base(message)
        {
            this.Details = new Dictionary<string, object>();
        }

        public DetailedLogException(string message, Exception inner)
            : base(message, inner)
        {
            this.Details = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Details { get; }
    }
}