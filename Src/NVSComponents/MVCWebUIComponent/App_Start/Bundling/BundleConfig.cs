using System.Web.Optimization;
using Volvo.NVS.Core.Configuration;
using Volvo.NVS.Core.Unity;
using Volvo.NVS.Utilities.Web.Bundling;

namespace Volvo.LAT.MVCWebUIComponent.Bundling
{
    /// <summary>
    /// Configures bundling for the web application.
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// Configure bundling for the web application.
        /// </summary>
        /// <remarks>
        /// We are using the Unity itself in order to resolve the bundle configuration object, service.
        /// This service acts here like the configuration helper and will do the configuration for us.
        /// The implementation of this service is provided by the Utility Library (BundleConfig class).
        /// The service searches for an implementation of the IBundleCollectionService (via Unity).
        /// The IBundleCollectionService will provide application specific configuration. Check the
        /// PosBundleCollectionService class for details about the configuration service for POS.
        /// It is the PosBundleCollectionService which is called by the implementation of IBundleConfig.
        /// </remarks>
        public static void Configure() => LibraryConfigurator.Current.ConfigureBundling(Container.Resolve<IBundleCollectionService>());
    }
}