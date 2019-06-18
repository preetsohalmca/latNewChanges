using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Unity.Configuration;
using Volvo.NVS.Security.Claims;
using Volvo.LAT.UserDomain.DomainLayer;
using Volvo.LAT.UserDomain.DomainLayer.ProjectionRepositoryInterfaces;
using Volvo.LAT.UserDomain.DomainLayer.RepositoryInterfaces;
using Volvo.LAT.UserDomain.InfrastructureLayer.ProjectionRepositories;
using Volvo.LAT.UserDomain.InfrastructureLayer.Repositories;
using Volvo.LAT.UserDomain.ServiceLayer;

namespace Volvo.LAT.UserDomain.Configuration
{
    public class UserDomainConfigurator : IContainerConfigurator
    {
        public void Configure(IUnityContainer container) => container
            .RegisterType<IUserCheckAccess, UserAuthorizationService>()
            .RegisterType<IUserAuthorizationService, UserAuthorizationService>()
            .RegisterType<IUserService, UserService>()
            .RegisterType<IUserRepository, UserRepository>()
            .RegisterType<IUserManagementRepository, UserManagementRepository>()
            .RegisterType<IClaimsProviderCache, UserService>()
            .RegisterType<IClaimsProvider, UserService>();
    }
}
