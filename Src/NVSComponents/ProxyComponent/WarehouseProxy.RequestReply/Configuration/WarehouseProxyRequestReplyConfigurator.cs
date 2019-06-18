using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Unity.Configuration;
using Volvo.POS.Proxy.Warehouse.RequestReply.Translators;
using AMapper = AutoMapper;

namespace Volvo.POS.Proxy.Warehouse.RequestReply.Configuration
{
    public class WarehouseProxyRequestReplyConfigurator : IContainerConfigurator
    {
        public void Configure(IUnityContainer container) => container
            .RegisterType<IWarehouseRequestService, WarehouseRequestService>()
            .RegisterType<AMapper.Profile, WarehouseRequestProfile>("WarehouseFireProfile");
    }
}
