using Volvo.NVS.Utilities.WindowsServices.Services;

namespace Volvo.POS.Proxy.WindowsService
{
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            // Perform the configuration of the host.
            AppConfigurator.Configure();

            // Run the service according to the application configuration file.
            NVSWinService.Run();
        }
    }
}
