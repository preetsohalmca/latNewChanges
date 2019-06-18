using Volvo.NVS.Core.Configuration;
using Volvo.NVS.Core.Unity;
using Volvo.POS.Gateway.Fire.Configuration;
using Volvo.POS.OrderDomain.Configuration;
using Volvo.POS.PartDomain.Configuration;
using Volvo.POS.Proxy.Warehouse.RequestReply.Configuration;
using Volvo.POS.Proxy.Warehouse.Subscribe.Configuration;
using Volvo.POS.UserDomain.Configuration;

namespace Volvo.POS.Proxy.WindowsService.Unity
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
        /// Configures the unity container for the application.
        /// </summary>
        public static void Configure()
        {
            LibraryConfigurator.Current.ConfigureContainer(configure => configure.FromConfigurator(new WinConfig()));
            LibraryConfigurator.Current.ConfigureContainer(configure => configure.FromConfigurator(new OrderDomainConfigurator()));
            LibraryConfigurator.Current.ConfigureContainer(configure => configure.FromConfigurator(new PartDomainConfigurator()));
            LibraryConfigurator.Current.ConfigureContainer(configure => configure.FromConfigurator(new UserDomainConfigurator()));
            LibraryConfigurator.Current.ConfigureContainer(configure => configure.FromConfigurator(new POSGatewayFireConfigurator()));
            LibraryConfigurator.Current.ConfigureContainer(configure => configure.FromConfigurator(new WarehouseProxyRequestReplyConfigurator()));
            LibraryConfigurator.Current.ConfigureContainer(configure => configure.FromConfigurator(new WarehouseProxySubscribeConfigurator()));

            // NVS Integration for WebSpehere MQ(IInputChannel, IOutputChannel, IReplyChannel)
            LibraryConfigurator.Current.ConfigureIntegrationMQChannels(builder => builder.RegisterChannels());

            // Security RegisterType<IClaimsService, ThreadClaimsService>()
            LibraryConfigurator.Current.ConfigureSecurity(security => security.SetClaimsService(service => service.UseThreadClaimsService()));
        }
    }
}
