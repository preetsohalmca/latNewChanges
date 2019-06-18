using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Unity.Configuration;

namespace Volvo.POS.Gateway.Fire.Configuration
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
#pragma warning disable S101 // Types should be named in camel case
    public class POSGatewayFireConfigurator : IContainerConfigurator
#pragma warning restore S101 // Types should be named in camel case
    {
        public void Configure(IUnityContainer container) => container.RegisterType<IGatewayFireService, GatewayFireService>();
    }
}
