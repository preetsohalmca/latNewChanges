using Volvo.NVS.Core.Configuration;
using Volvo.NVS.Core.Unity;
using Volvo.LAT.MVCWebUIComponent.App_Start.Unity;
using Volvo.LAT.UserDomain.Configuration;
using Volvo.LAT.POLineDomain.Configuration;
using Volvo.LAT.PartDomain.Configuration;

namespace Volvo.LAT.MVCWebUIComponent.Unity
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
        public static void Configure()
        {
            // Configure the container using the default application configuration file.
            // In our case the unity node will be searched in the Web.config for the web application.
            LibraryConfigurator.Current
                .ConfigureContainer(configure => configure.FromConfigurator(new MvcConfig())) 
                .ConfigureContainer(configure => configure.FromConfigurator(new UserDomainConfigurator()))
                .ConfigureContainer(configure => configure.FromConfigurator(new POLineDomainConfigurator()))
                .ConfigureContainer(configure=>configure.FromConfigurator(new ApplicationConfiguration()))
                .ConfigureContainer(configure => configure.FromConfigurator(new ContractTypeConfiguration()))
                .ConfigureContainer(configure => configure.FromConfigurator(new PurchaseOrderConfiguration()))
                .ConfigureContainer(configure => configure.FromConfigurator(new InvoiceReportConfiguration()))
                .ConfigureContainer(configure => configure.FromConfigurator(new CostListConfiguration()))
                .ConfigureContainer(configure => configure.FromConfigurator(new DashboardDomainConfigurator()))
                // NVS Integration for WebSpehere MQ(IInputChannel, IOutputChannel, IReplyChannel)
                .ConfigureIntegrationMQChannels(builder => builder.RegisterChannels());
        }
    }
}