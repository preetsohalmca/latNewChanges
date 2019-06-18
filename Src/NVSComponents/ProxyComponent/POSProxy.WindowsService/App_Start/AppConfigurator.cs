using Volvo.POS.Proxy.WindowsService.AutoMapper;
using Volvo.POS.Proxy.WindowsService.EntLib;
using Volvo.POS.Proxy.WindowsService.Logging;
using Volvo.POS.Proxy.WindowsService.Security;
using Volvo.POS.Proxy.WindowsService.Unity;

namespace Volvo.POS.Proxy.WindowsService
{
    /// <summary>
    /// Configures and bootstraps the application.
    /// </summary>
    public static class AppConfigurator
    {
        /// <summary>
        /// Configures the application.
        /// </summary>
        public static void Configure()
        {
            // Configure the unity container.
            ContainerConfig.Configure();

            // Configure the logging for the host.
            LoggingConfig.Configure();

            // Configure Enterprise Library blocks.
            EntLibConfig.Configure();

            // Configure the AutoMapper.
            AutoMapperConfig.Configure();

            // Configure the Security Library.
            SecurityConfig.Configure();
        }
    }
}
