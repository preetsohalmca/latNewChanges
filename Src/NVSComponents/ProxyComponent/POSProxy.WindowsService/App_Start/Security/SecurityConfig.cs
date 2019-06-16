using System.Configuration;
using System.Security.Claims;
using Volvo.NVS.Core.Exceptions;
using Volvo.NVS.Core.Logging;
using Volvo.NVS.Core.Unity;
using Volvo.NVS.Persistence.NHibernate.SessionHandling;
using Volvo.NVS.Security.Handlers;
using Volvo.NVS.Security.Unity;
using Volvo.POS.UserDomain.ServiceLayer;

namespace Volvo.POS.Proxy.WindowsService.Security
{
    /// <summary>
    /// Configures the Security Library for the sample application.
    /// </summary>
    public static class SecurityConfig
    {
        /// <summary>
        /// Type of the authentication established for the windows service.
        /// </summary>
        private const string AuthenticationType = "CustomService";

        /// <summary>
        /// Configures the Security Library for the POS application.
        /// </summary>
        /// <remarks>
        /// The POS sample application uses various CheckAccess attributes in order to protect domains.
        /// See for example the OrderDomain Service and method decorated with usage of those attributes.
        /// </remarks>
        public static void Configure()
        {
            // Automate the CheckAccess, Security Library interception mechanism configuration.
            // Configure the interception for all the methods marked with the unity based CheckAccess
            // attribute (or one of its derived attributes) when the marked method is on the class level.
            // It means that when this configuration is done one can use the CheckAccess attributes on class
            // methods, resolve types via unity (mapped to those classes) and be sure that the authorization
            // checks are executed as defined on the applied CheckAccess attributes. This is just a shortcut
            // in place of the more complex manual configuration of the unity interception.
            Container.Register(VirtualMethodInterceptionRegistrations.Registrar);

            // Authenticate the current application.
            AuthenticateAndAuthorize();
        }

        /// <summary>
        /// Setup the claims so the claim base authentication and authorization is possible.
        /// Establish the application user which should be used by the listener service for all the calls.
        /// </summary>
        private static void AuthenticateAndAuthorize()
        {
            using (new NHibernateSessionContext())
            {
                var posLogger = CachedContainer<ILogger>.TryResolve(() => new EmptyLogger());
                var userService = Container.Resolve<IUserService>();

                // The user which is used to perform all the Listener operations must be configured in the app.config
                var userId = ConfigurationManager.AppSettings["ListenerUser"];
                posLogger.LogDebug($"userId = {userId}");

                if (string.IsNullOrEmpty(userId))
                {
                    throw new NVSException(
                        "The user used to run the Listener is empty or not configured in the application configuration.");
                }

                // The user must be the registered application user
                var domainUser = userService.GetUser(userId);
                posLogger.LogDebug($"domainUser = {domainUser}");

                if (domainUser == null)
                {
                    throw new NVSException(
                        $"User '{userId}' configured to run the Listener has not been found in the application database.");
                }

                // Create the identity to be used according to the configured user (claims-based representation of a single user)
                var claimsIdentity = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, domainUser.UserName, "http://www.w3.org/2001/XMLSchema#string", AuthenticationType)
                    },
                    AuthenticationType,
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimTypes.Role);

                // Run the Security Library authentication handler so the custom identity is set for the complete
                // application domain (which is the windows service application domain). The claims service is also
                // initialized and all the configured (via unity) claim providers are executed.
                AppDomainCustomAuthenticationHandler.Run(claimsIdentity);
            }
        }
    }
}
