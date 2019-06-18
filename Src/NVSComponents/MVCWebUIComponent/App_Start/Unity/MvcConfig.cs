using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Unity.Configuration;
using Volvo.NVS.Utilities.Web.Bundling;
using Volvo.NVS.Utilities.Web.Localization;
using Volvo.NVS.Utilities.Web.Localization.Culture;
using Volvo.NVS.Utilities.Web.Session;
using Volvo.LAT.MVCWebUIComponent.Bundling;
using Volvo.LAT.MVCWebUIComponent.Common.Culture;
using Volvo.LAT.MVCWebUIComponent.Common.Helpers;
using Volvo.LAT.MVCWebUIComponent.Models.Translators;
using AMapper = AutoMapper;

namespace Volvo.LAT.MVCWebUIComponent.App_Start.Unity
{
    public class MvcConfig : IContainerConfigurator
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public void Configure(IUnityContainer container) => container
            .RegisterType<IPosSessionHelper, PosSessionHelper>()
            .RegisterType<IBundlingHelper, BundlingHelper>()
            .RegisterType<IThemesHelper, ThemesHelper>()

            // AutoMapper Mappings/Profiles
             .RegisterType<AMapper.Profile, UserDomainMvcProfile>("UserDomainMvcProfile")
             .RegisterType<AMapper.Profile, POLineDomainMvcProfile>("POLinerDomainMvcProfile")

            // Utilities
            .RegisterType<IBundleCollectionService, PosBundleCollectionService>()
            .RegisterType<ICultureResolver, PosCultureResolver>()
            .RegisterType<ISessionHelper, SessionHelper>()
            .RegisterType<ISessionSettingsProvider, SessionSettingsProvider>()
            .RegisterType<ILocalizationHelper, LocalizationHelper>(new ContainerControlledLifetimeManager());
    }
}