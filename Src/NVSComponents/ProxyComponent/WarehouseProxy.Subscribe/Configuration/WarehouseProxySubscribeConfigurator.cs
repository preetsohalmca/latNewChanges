using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Unity.Configuration;
using Volvo.POS.Proxy.Warehouse.Subscribe.Translator;
using AMapper = AutoMapper;

namespace Volvo.POS.Proxy.Warehouse.Subscribe.Configuration
{
    public class WarehouseProxySubscribeConfigurator : IContainerConfigurator
    {
        public void Configure(IUnityContainer container) => container
            .RegisterType<IPartsReservationService, PartsReservationService>()
            .RegisterType<IPartsAvailabilityService, PartsAvailabilityService>()
            .RegisterType<AMapper.Profile, WarehouseSubscribeProfile>("WarehouseSubscribeProfile");
    }
}
