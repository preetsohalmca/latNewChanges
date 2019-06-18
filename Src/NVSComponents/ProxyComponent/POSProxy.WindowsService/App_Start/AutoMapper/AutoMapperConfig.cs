using Volvo.NVS.Core.Configuration;
using Volvo.POS.Proxy.Warehouse.Subscribe;

namespace Volvo.POS.Proxy.WindowsService.AutoMapper
{
    /// <summary>
    /// Configures the AutoMapper for the application.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Configure the AutoMapper globally for the complete application.
        /// </summary>
        public static void Configure() => LibraryConfigurator.Current.ConfigureAutoMapper(
            mapper => mapper.RegisterProfiles(typeof(IPartsAvailabilityService).Assembly));
    }
}
