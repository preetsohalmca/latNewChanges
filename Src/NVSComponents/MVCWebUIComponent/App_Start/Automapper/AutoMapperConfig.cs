using Volvo.NVS.Core.Configuration;
using Volvo.NVS.Utilities.Automapper;

namespace Volvo.LAT.MVCWebUIComponent.AutoMapper
{
    /// <summary>
    /// Configures the AutoMapper for the complete web application.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Configure the AutoMapper globally for the complete application.
        /// </summary>
        public static void Configure() => LibraryConfigurator.Current.ConfigureAutoMapper(config => config.RegisterProfiles());
    }
}