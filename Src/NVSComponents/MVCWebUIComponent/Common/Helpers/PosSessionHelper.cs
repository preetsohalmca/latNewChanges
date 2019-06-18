using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Unity;
using Volvo.NVS.Security.Claims;
using Volvo.NVS.Utilities.Web.Session;
using Volvo.LAT.UserDomain.ServiceLayer;

using Volvo.LAT.POLineDomain.ServiceLayer;
using DomainUser = Volvo.LAT.UserDomain.DomainLayer.Entities;

using PODomainUser = Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.MVCWebUIComponent.Common.Helpers
{
    /// <summary>
    /// The POS application session helper.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "PosSession")]
    public class PosSessionHelper : SessionHelper, IPosSessionHelper
    {
        /// <summary>
        /// The web session key for the current user.
        /// </summary>
        public const string CurrentUserSessionKey = "CurrentUser";

        /// <summary>
        /// Gets or sets the claims provider cache service.
        /// </summary>
        protected IClaimsProviderCache ClaimsProviderCache { get; set; }

        /// <summary>
        /// Gets or sets the user service.
        /// </summary>
        protected IUserService UserService { get; set; }
        protected IDashboardService DashboardServiec { get; set; }

        /// <summary>
        /// Gets or sets the claims service.
        /// </summary>
        protected IClaimsService ClaimsService { get; set; }
        protected IPOLineService POLineService { get; set; }
        protected IApplicationService ApplicationService { get; set; }

        protected IContractTypeService ContractTypeService { get; set; }

        protected IPurchaseOrderService PurchaseOrderService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PosSessionHelper"/> class.
        /// </summary>
        /// <remarks>
        /// This is the injection constructor as all the dependencies should be resolved in a lazy way (when needed).
        /// </remarks>
        [InjectionConstructor]
        public PosSessionHelper()
            : this(
                  Container.Resolve<IClaimsProviderCache>(),
                  Container.Resolve<IUserService>(),
                  Container.Resolve<IClaimsService>(),
                  Container.Resolve<IPOLineService>(),
                  Container.Resolve<IApplicationService>(),
                  Container.Resolve<IPurchaseOrderService>(),
                  Container.Resolve<IContractTypeService>(),
                  Container.Resolve<IDashboardService>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PosSessionHelper"/> class.
        /// </summary>
        /// <param name="claimsProviderCache">The claims provider cache service to be used.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="claimsService">The claims service.</param>
        public PosSessionHelper(IClaimsProviderCache claimsProviderCache, IUserService userService, 
            IClaimsService claimsService, IPOLineService poLineService,
            IApplicationService applicationService, IPurchaseOrderService purchaseOrderService, IContractTypeService contractTypeService, IDashboardService dashBoardService)
        {
            ClaimsProviderCache = claimsProviderCache;
            UserService = userService;
            ClaimsService = claimsService;
            POLineService = poLineService;
            ApplicationService = applicationService;
            this.PurchaseOrderService = purchaseOrderService;
            ContractTypeService = contractTypeService;
            this.DashboardServiec = dashBoardService;
        }

        /// <summary>
        /// Initialize the Session with all the obligatory and basic objects which should be present during the complete
        /// session lifetime. The initialization should be executed once at the Session StartUp.
        /// </summary>
        /// <remarks>
        /// Check also the code from the Global.asax.
        /// </remarks>
        public void InitializeOnStart()
        {
            // When a session starts clear the cached list of user roles for the current user as this list could
            // be modified. Clear the cached values created on different, previous requests (different requests
            // are recognized by comparing the request timestamp)
            ClaimsProviderCache.RemoveCacheForCurrentUser(HttpContext.Current.Timestamp);

            // Check and clear all the expired cached lists of roles for all the users. When some users are not
            // working in the system anymore the memory will be released. At the same time we are forcing the
            // cache refresh operation for every user to be execute at least as defined on the session timeout.
            ClaimsProviderCache.RemoveCacheForAll(1000);

            // ClaimsProviderCache.RemoveCacheForAll(SessionSettingsProvider.Timeout);
        }

        /// <summary>
        /// Gets a value indicating whether session is a new one (just created).
        /// </summary>
        public bool IsNewSession => HttpContext.Current.Session.IsNewSession;
    }
}
