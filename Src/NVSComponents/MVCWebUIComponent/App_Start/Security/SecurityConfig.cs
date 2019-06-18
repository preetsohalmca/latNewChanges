using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Volvo.NVS.Core.Configuration;
using Volvo.NVS.Security.Cryptography;
using Volvo.NVS.Security.Handlers;
using Volvo.NVS.Security.SystemWeb.Ping;
using Volvo.NVS.Security.SystemWeb.Windows;
using Volvo.NVS.Security.Tokens;

namespace Volvo.LAT.MVCWebUIComponent.Security
{
    /// <summary>
    /// Configures the Security Library for the sample application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The POS sample application uses the Ping based authentication (Volvo adopted OAuth2 - a single access_token is guaranteed to be a JWT with identity information)
    /// or the Integrated Windows Authentication. Ping is used on QA and PROD environments only. Windows Authentication is used on all other environments.
    /// </para>
    /// <para>
    /// As the POS is a classic web application it is configured to use the PingAccess as well. It is up to the PingAccess to talk to the PingFederate and obtain tokens.
    /// Those tokens are later stored on cookies (not transferred on authorization headers as in the client centric solutions). POS is not doing any of OpenID Connect,
    /// OAuth2 flows. It is only parsing and validating received tokens.
    /// </para>
    /// <para>
    /// Some of the required web.config changes are done in the build server, when an automated deployment is executed. The parameters for web.config are replaced there
    /// according to the schema defined in the parameters.xml file.
    /// </para>
    /// </remarks>
    public static class SecurityConfig
    {
        /// <summary>
        /// Gets a value indicating whether the Ping based authentication (e.g. involving the OpenID Connect or Volvo adopted OAuth 2 protocol) is used.
        /// If false then the sample application will be configured to use Integrated Windows Authentication which must also be reflected in web.config.
        /// </summary>
        public static bool IsPing =>
            ConfigurationManager.AppSettings["UsePing"].Trim().ToLowerInvariant() == true.ToString().ToLowerInvariant();

        /// <summary>
        /// Gets the name of the current environment (as set in the configuration file during deployment).
        /// </summary>
        public static string Environment => ConfigurationManager.AppSettings["Environment"];

        /// <summary>
        /// A signing issuer of the JSON Web token when Ping is used.
        /// </summary>
        private const string Issuer = "PingAccess";

        /// <summary>
        /// An audience of the JSON Web token when Ping is used. It may depend on the environment name on which we run.
        /// </summary>
        private const string Audience = "pos{0}";

        /// <summary>
        /// A name of the Ping Access cookie on which a token issued by Ping is expected. It may depend on the environment name on which we run.
        /// </summary>
        private const string CookieName = "PA.pos{0}";

        /// <summary>
        /// An URI at which JSON Web Keys are exposed by the Ping Federate. Those keys are used to check JWT token signatures when Ping is used.
        /// </summary>
        private const string JwsKeyEndpoint = "https://securemobile-qa.volvo.com/pa/oidc/JWKS";

        /// <summary>
        /// Provides options to be used by the Ping authentication handler if Ping based solution should be configured.
        /// </summary>
        /// <returns>An object holding all of the Ping options to be used.</returns>
        private static PingAuthenticationHandlerOptions CreatePingOptions()
        {
            // A name of the cookie and a name of the allowed audience received on the JWT contains an environment name.
            // The only one exception is the production environment which uses a  'pure', 'clear' pos name in all options.
            string optionName = string.Compare(Environment, "prod", StringComparison.OrdinalIgnoreCase) == 0
                ? string.Empty
                : Environment.ToLowerInvariant();

            return new PingAuthenticationHandlerOptions
            {
                TokenMode = PingAuthenticationHandlerTokenMode.RootTokenOnly,
                KeyProvider = new JwsKeyProvider(new Uri(JwsKeyEndpoint)),
                AuthenticationCookieName = string.Format(CookieName, optionName),
                AllowedAudience = string.Format(Audience, optionName),
                Issuer = Issuer
            };
        }

        /// <summary>
        /// Configures the Security Library performing actions which must run before the web application is initiated.
        /// Sets Integrated Windows Authentication or Ping based authentication to be used.
        /// </summary>
        /// <remarks>
        /// Register the Ping or Windows module depending on the current settings.
        /// NOTE: The automated authentication module registration is disable in web.config under the "volvo.nvs.security" node.
        /// It must be disabled there otherwise the security library would configure all the referenced modules automatically
        /// and as we reference both SystemWeb.Ping and SystemWeb.Windows we would then use both at the same time.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "PreConfigure")]
        public static void PreConfigure() => HttpApplication.RegisterModule(IsPing ? typeof(PingJwtHttpModule) : typeof(WindowsHttpModule));

        /// <summary>
        /// Configures the Security Library for the POS application.
        /// </summary>
        /// <remarks>
        /// The POS sample application uses various CheckAccess attributes in order to protect domains.
        /// See for example the OrderDomain Service and method decorated with usage of those attributes.
        /// </remarks>
        public static void Configure() => Configure(LibraryConfigurator.Current);

        /// <summary>
        /// Configures the Security Library for the POS application.
        /// </summary>
        /// <param name="configurator">A library configurator to be used.</param>
        /// <remarks>
        /// The POS sample application uses various CheckAccess attributes in order to protect domains.
        /// See for example the OrderDomain Service and method decorated with usage of those attributes.
        /// </remarks>
        private static void Configure(ILibraryConfigurator configurator)
        {
            // In POS we are using Ping only on some of the environments (Prod, QA). The rest should use Windows Authentication.
            // See also the PreConfigure method were we manually decide which of the authentication modules should be involved.
            // The rest of the needed web.config related settings we tune on the build server when automated deployment is performed.
            if (IsPing)
            {
                configurator.ConfigureSecurity(config => config
                    .SetJwtValidator(new JwtESSignedValidator())
                    .SetJwtValidationProperties(CreatePingOptions()));
            }

            // Configure security settings which are the same no matter if Windows or Ping is used.
            configurator.ConfigureSecurity(config => config

                    // Automate the CheckAccess, Security Library interception mechanism configuration.
                    // Configure the interception for all the methods marked with the unity based CheckAccess
                    // attribute (or one of its derived attributes) when the marked method is on the class level.
                    // It means that when this configuration is done one can use the CheckAccess attributes on class
                    // methods, resolve types via unity (mapped to those classes) and be sure that the authorization
                    // checks are executed as defined on the applied CheckAccess attributes. This is just a shortcut
                    // in place of the more complex manual configuration of the unity interception.
                    .SetupUnityInterceptionForCheckAccess()

                    // Use the web claims service so the current identity is per http context.
                    .SetClaimsService(service => service.UseWebClaimsService()));
        }
    }
}