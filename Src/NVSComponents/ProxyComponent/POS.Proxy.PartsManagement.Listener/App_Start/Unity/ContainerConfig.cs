using Volvo.NVS.Core.Unity;
using System.ServiceProcess;
using Microsoft.Practices.ServiceLocation;

namespace Volvo.POS.ProxyComponents.Warehouse.Listener.Unity
{
    /// <summary>
    /// Configures the Unity for the complete application.
    /// </summary>
    /// <remarks>
    /// Check also the <see cref="Container"/> documentation.
    /// </remarks>
    public static class ContainerConfig
    {

        /// <summary>
        /// Configures the unity container for the complete application.
        /// </summary>
        /// <param name="service">The currently running service object.</param>
        public static void Configure(ServiceBase service)
        {
            // Configure the container using the default application configuration file.
            // In our case the unity node will be searched in the Web.config for the web application.
            Container.Configure();
            
            // TODO: Remove this line after fixing NVS Integration
            ServiceLocator.SetLocatorProvider(() => null);

            // Registering the Service instance to be accessible
            Container.RegisterInstance(service);
        }

    }
}
