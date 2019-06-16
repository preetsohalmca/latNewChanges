using Volvo.NVS.Core.Logging;
using Volvo.NVS.Core.Unity;

namespace Volvo.POS.Proxy.WindowsService.Logging
{
    /// <summary>
    /// Configures logging for the application.
    /// </summary>
    public static class LoggingConfig
    {
        /// <summary>
        /// Configures logging for the application.
        /// </summary>
        public static void Configure()
        {
            // Add our default, console logger if no logger is provided from the Unity configuration file.
            if (!Container.IsRegistered<ILogger>())
            {
                Container.RegisterType<ILogger, Logger>();
            }
        }
    }
}
