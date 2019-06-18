using System;
using Volvo.NVS.Core.Logging;
using Volvo.NVS.Logging;

namespace Volvo.POS.Proxy.WindowsService.Logging
{
    public class Logger : ILogger
    {
        public void LogDebug(string message)
        {
            LogConsole(message);
            Log.Debug(message);
        }

        public void LogInfo(string message)
        {
            LogConsole(message);
            Log.Info(message);
        }

        public void LogTrace(string message)
        {
            LogConsole(message);
            Log.Trace(message);
        }

        public void LogError(string message)
        {
            LogConsole(message);
            Log.Error(message);
        }

        public void LogError(string message, Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            LogConsole(ex.Message);
            Log.Error(message, ex);
        }

        public void LogError(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            LogConsole(ex.Message);
            Log.Error(ex);
        }

        public void LogNotify(string message)
        {
            LogConsole(message);
            Log.Notify(message);
        }

        public void LogNotify(string message, Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            LogConsole(message + ex.Message);
            Log.Notify(message, ex);
        }

        public void LogNotify(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            LogConsole(ex.Message);
            Log.Notify(ex);
        }

        public void LogBusinessTask(string message)
        {
            LogConsole(message);
            Log.BusinessTask(message);
        }

        private static void LogConsole(string message)
        {
            // running as a console
            if (Environment.UserInteractive)
            {
                Console.WriteLine(message);
            }
        }
    }
}
