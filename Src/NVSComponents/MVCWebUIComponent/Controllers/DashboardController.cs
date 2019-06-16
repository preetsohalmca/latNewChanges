namespace Volvo.LAT.MVCWebUIComponent.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.Practices.Unity;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using Volvo.LAT.MVCWebUIComponent.Common.ActionFilter;
    using Volvo.LAT.MVCWebUIComponent.Common.Helpers;
    using Volvo.LAT.MVCWebUIComponent.Models.ViewModel;
    using Volvo.LAT.PartDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.ServiceLayer;
    using Volvo.LAT.UserDomain.DomainLayer.Entities;
    using Volvo.LAT.UserDomain.ServiceLayer;
    using Volvo.NVS.Core.Unity;
    using Volvo.NVS.Persistence.NHibernate.Web.SessionHandling;
    using Volvo.NVS.Utilities.Web.Localization;

    public class DashboardController : BaseController
    {

        protected IPosSessionHelper SessionHelper { get; set; }
        protected IDashboardService DashboardServices { get; set; }

        protected IUserService UserService { get; set; }
        protected IPOLineService POLineServices { get; set; }


        [InjectionConstructor]
        public DashboardController()
            : this(
                  Container.Resolve<ILocalizationHelper>(),
                  Container.Resolve<IThemesHelper>(),
                  Container.Resolve<IBundlingHelper>(),
                  Container.Resolve<IPosSessionHelper>(),
                  Container.Resolve<IDashboardService>(),
                   
                  Container.Resolve<IUserService>(),
                  Container.Resolve<IPOLineService>())


        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="localizationHelper">The localization helper.</param>
        /// <param name="themesHelper">The themes helper.</param>
        /// <param name="bundlingHelper">The bundling helper.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="sessionHelper">The session helper.</param>
        public DashboardController(
            ILocalizationHelper localizationHelper,
            IThemesHelper themesHelper,
            IBundlingHelper bundlingHelper,
            IPosSessionHelper sessionHelper,
            IDashboardService dashboardServices,

            IUserService userService,
             IPOLineService polineService
            )
            : base(localizationHelper, themesHelper, bundlingHelper)
        {
            this.SessionHelper = sessionHelper;
            this.DashboardServices = dashboardServices;

            this.UserService = userService;
            this.POLineServices = polineService;

        }
        // GET: Dashboard
        [NHibernateMvcSessionContext]
        
        public ActionResult Index()
        {
            var currentUser = this.UserService.GetCurrent();
            if (currentUser != null && (currentUser.UserRole.Name == Role.Admin.ToString() || currentUser.UserRole.Name == Role.User.ToString()))
            {
                var searchModel = new SearchModel();
                searchModel.ActivityTypes = this.POLineServices.GetAllActivityTypes();
                searchModel.Owners = this.POLineServices.GetAllOwners();

                var assignmentCode = this.POLineServices.FindAllWBSORAssignmentCode();
                searchModel.AssignmentCodes = assignmentCode.Select(x => new DDLModel() { text = x, value = x }).ToList();

                var wbsElements = this.POLineServices.FindPOLineAsQueryableNew().Where(x => x.WbsElement != null).Select(x => x.WbsElement).ToList();
                var wbsElementsIds = wbsElements.Select(x => x.WbsElementID).Distinct().ToList();

                var itemWbs = this.POLineServices.GetAllWbs().Where(x=> wbsElementsIds.Contains(x.WbsElementID));//(x => new DDLModel() { text = x.Name, value = x.WbsElementID.ToString() }).ToList();

                searchModel.WBS = itemWbs.Distinct().Select(x => new DDLModel() { text = x.Name, value = x.WbsElementID.ToString() }).ToList().OrderBy(x => x.text) ;//this.POLineServices.GetAllWbs().Distinct().Select(x => new DDLModel() { text = x.Name, value = x.WbsElementID.ToString() }).ToList();//assignmentCode.Select(x => new DDLModel() { text = x, value = x }).ToList();
                searchModel.ContractTypes = this.POLineServices.GetAllContractTypes().OrderBy(x => x.Name); 
                searchModel.Applications = this.POLineServices.GetAllApplications().OrderBy(x=>x.Name);
                searchModel.Requestores = this.POLineServices.FindAllRequesterName().Where(x => !string.IsNullOrEmpty(x)).Distinct().Select(x => new DDLModel() { text = x, value = x });
                return View("Index", searchModel);
            }
            return View("AccessDenied");

        }

        [NHibernateMvcSessionContext]
        public ActionResult GetNewPurchaseOrders([DataSourceRequest]DataSourceRequest request)
        {
            var cUser = UserService.GetCurrent();
            int totalrecords;
            int pagesToSkip = request.Page>1? (request.Page - 1) * request.PageSize:0;
            var result = this.DashboardServices.FindNewPurchaseOrders(request.PageSize, pagesToSkip, cUser.Username, out totalrecords).ToDataSourceResult(new DataSourceRequest());
           
            result.Total = totalrecords;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [NHibernateMvcSessionContext]
        public ActionResult GetPolineRenewals([DataSourceRequest]DataSourceRequest request)
        {
            var cUser = UserService.GetCurrent();
            int pagesToSkip = request.Page > 1 ? (request.Page - 1) * request.PageSize : 0;
            int totalrecords;
            var result = this.DashboardServices.FindRenewals(request.PageSize, pagesToSkip, cUser.Username, out totalrecords).ToDataSourceResult(new DataSourceRequest());
            result.Total = totalrecords;
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
