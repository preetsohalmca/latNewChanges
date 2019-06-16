using System.Reflection;
using Volvo.NVS.Utilities.Automapper;

namespace Volvo.POS.Proxy.Warehouse.Subscribe.AutoMapper
{
    /// <summary>
    /// Configures the AutoMapper for the complete web application.
    /// </summary>
    public static class AutomapperConfig
    {

        /// <summary>
        /// Configure the AutoMapper globally for the complete application.
        /// </summary>
        public static void Configure()
        {
            // Register all the Profiles which are present in the current (Listener) assembly.
            AutoMapperConfiguration.RegisterProfiles(Assembly.GetExecutingAssembly());
        }

    }
}
