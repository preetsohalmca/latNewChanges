using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Volvo.POS.Proxy.WindowsService.EntLib
{
    /// <summary>
    /// Configures the Enterprise Library bootstrapping its blocks.
    /// </summary>
    public static class EntLibConfig
    {
        /// <summary>
        /// Configures the Enterprise Library bootstrapping its blocks.
        /// </summary>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static void Configure()
        {
            // Base the configuration of the Enterprise Library on the current application configuration file.
            var source = ConfigurationSourceFactory.Create();

            // Setup logger which will be used by other blocks
            var logwriterFactory = new LogWriterFactory(source);
            var logWriter = logwriterFactory.Create();
            Logger.SetLogWriter(logWriter);
        }
    }
}
