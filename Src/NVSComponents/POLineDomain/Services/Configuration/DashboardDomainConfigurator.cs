namespace Volvo.LAT.POLineDomain.Configuration
{
    using InfrastructureLayer.ProjectionRepositories;
    using Microsoft.Practices.Unity;
    using Volvo.LAT.POLineDomain.DomainLayer;
    using Volvo.LAT.POLineDomain.DomainLayer.ProjectionRepositoryInterfaces;
    using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
    using Volvo.LAT.POLineDomain.InfrastructureLayer.Repositories;
    using Volvo.LAT.POLineDomain.ServiceLayer;
    using Volvo.NVS.Core.Unity.Configuration;
    using Volvo.LAT.PartDomain.InfrastructureLayer.Repositories;
    public class DashboardDomainConfigurator : IContainerConfigurator
    {
        public void Configure(IUnityContainer container) => container
            .RegisterType<IDashboardService, DashboardService>()
        .RegisterType<IPOLineService, POLineService>()
            .RegisterType<IPOLineRepository, POLineRepository>()
            .RegisterType<IPOLineSelectionRepository, POLineSelectionRepository>()
            .RegisterType<IApplicationService, ApplicationService>()
            .RegisterType<IApplicationRepositrory, ApplicationRepository>()
            .RegisterType<ICostListRepositrory, CostListRepository>()
            .RegisterType<IDashboardRepository, DashboardRepository>();
        //.RegisterType<IContractTypeRepository, ContractTypeRepository>();
    }
}
