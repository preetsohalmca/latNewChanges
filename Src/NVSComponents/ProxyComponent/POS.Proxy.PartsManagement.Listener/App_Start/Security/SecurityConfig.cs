using Volvo.NVS.Core.Unity;
using Volvo.NVS.Security.Unity;
using Volvo.NVS.Security.Authorization;

namespace Volvo.POS.ProxyComponents.Warehouse.Listener.Security
{
    /// <summary>
    /// Configures the Security Library for the service application.
    /// </summary>
    public static class SecurityConfig
    {

        /// <summary>
        /// Configure the Security Library for the service application.
        /// </summary>
        public static void Configure()
        {
            // Automate the CheckAccess, Security Library interception mechanism configuration.
            // Configure the interception for all the methods marked with the unity based CheckAccess
            // attribute (or one of its derived attributes) when the marked method is on the class level
            Container.Register(VirtualMethodInterceptionRegistrations.Registrar);

            // Set the default (used by all interfaces having the CheckAccess method) Security Library binder
            // into the 'Multi' binder so it is possible to have more than one CheckAccess method on the single
            // authorization related interface type. Such interface types can then be used on the authorization
            // CheckAccess attributes. See for example the Authorization of the Order Domain.
            CheckAccessBinders.SetDefault(new MultiCheckAccessNameTypeBinder());
        }

    }
}
