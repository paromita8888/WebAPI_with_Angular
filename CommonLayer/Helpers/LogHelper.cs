using log4net;
using System;

namespace WebAPI_with_Angular.Common
{
    public class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger("WEBAPI_Plus_Angular_Logger");

        /// <summary>
        /// Logs error message onto log source (text file)
        /// </summary>
        /// <param name="error">Error Message</param>
        /// <param name="exception">Exception Object</param>
        public static void LogError(string error, Exception exception)
        {
            log.Error(error, exception);
        }

        /// <summary>
        /// Logs message into log source (text file)
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void LogMessage(string message)
        {
            log.Info(message);
        }
    }
}