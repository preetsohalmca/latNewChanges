using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Unity.Configuration;
using Volvo.NVS.Integration.WindowsServices.Services;
using Volvo.NVS.Utilities.WindowsServices.Services;
using Volvo.POS.Proxy.WindowsService.Listeners;

namespace Volvo.POS.Proxy.WindowsService.Unity
{
    public class WinConfig : IContainerConfigurator
    {
        public void Configure(IUnityContainer container) => container
            .RegisterType<IWindowsServiceBehaviour, IntegrationListenerService>("WindowsServiceBase")

            // QueueHandlers
            .RegisterType<IIntegrationListenerHandler, PartReservationResponseIntegrationHandler>("PartReservationResponse")
            .RegisterType<IIntegrationListenerHandler, PartsAvailabilityIntegrationHandler>("PartsAvailability");
    }
}
