using System.Reflection;
using Volvo.NVS.Utilities.Automapper;

namespace Volvo.POS.ProxyComponents.Warehouse.Listener.AutoMapper
{
    /// <summary>
    /// Configures the AutoMapper for the complete service.
    /// </summary>
    public static class AutomapperConfig
    {

        /// <summary>
        /// Configure the AutoMapper globally for the complete application.
        /// </summary>
        public static void Configure()
        {
            // Register all the Profiles which are present in the current (Listener) assembly.
           // AutoMapperConfiguration.RegisterProfiles(Assembly.GetExecutingAssembly());
            // Register other profiles from the Unity configuration. It is required for all
            // lower layers where such layer uses the mapping itself (ex. Proxy)
          //  AutoMapperConfiguration.RegisterProfiles();
        }

    }
}
