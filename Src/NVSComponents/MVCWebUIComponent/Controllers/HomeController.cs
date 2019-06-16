namespace Volvo.LAT.MVCWebUIComponent.Controllers
{
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using Microsoft.Practices.Unity;
    using Volvo.LAT.MVCWebUIComponent.App_LocalResources;
    using Volvo.LAT.MVCWebUIComponent.Common.Helpers;
    using Volvo.LAT.POLineDomain.ServiceLayer;
    using Volvo.LAT.UserDomain.ServiceLayer;
    using Volvo.NVS.Core.Unity;
    using Volvo.NVS.Persistence.NHibernate.Web.SessionHandling;
    using Volvo.NVS.Utilities.Web.Localization;
    using Kendo.Mvc.Extensions;
    using System.Net.Http;
    using System.Web.Http;
    using System.Net;
    using Volvo.LAT.MVCWebUIComponent.Common.ActionFilter;
    using PartDomain.DomainLayer.Entities;
    using System.Linq;
    using UserDomain.DomainLayer.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// The main application controller.
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        [InjectionConstructor]
        public HomeController()
            : this(
                  Container.Resolve<ILocalizationHelper>(),
                  Container.Resolve<IThemesHelper>(),
                  Container.Resolve<IBundlingHelper>(),
                  Container.Resolve<IUserService>(),
                  Container.Resolve<IPOLineService>(),
                  Container.Resolve<IPosSessionHelper>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="localizationHelper">The localization helper.</param>
        /// <param name="themesHelper">The themes helper.</param>
        /// <param name="bundlingHelper">The bundling helper.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="sessionHelper">The session helper.</param>
        public HomeController(
            ILocalizationHelper localizationHelper,
            IThemesHelper themesHelper,
            IBundlingHelper bundlingHelper,
            IUserService userService,
            IPOLineService polineService,
            IPosSessionHelper sessionHelper)
            : base(localizationHelper, themesHelper, bundlingHelper)
        {
            UserService = userService;
            SessionHelper = sessionHelper;
            this.POLineServices = polineService;
        }

        /// <summary>
        /// Gets or sets the a session helper or provides the already resolved one.
        /// </summary>
        protected IPosSessionHelper SessionHelper { get; set; }

        protected IPOLineService POLineServices { get; set; }

        /// <summary>
        /// Gets or sets the user service to be used by the controller.
        /// </summary>
        protected IUserService UserService { get; set; }

        /// <summary>
        /// Navigates into the main application view.
        /// </summary>
        /// <returns>Action result.</returns>
        [NHibernateMvcSessionContext]
        public ActionResult Index()
        {
            var userIsRegistered = UserService.IsCurrentUserRegistered();

            if (this.SessionHelper.IsNewSession && !userIsRegistered)
            {
                Messenger.AppendMessage(CommonResource.Authorization_NotRegisteredUser);
                return this.View("AccessDenied");
            }
            var currentUser = this.UserService.GetCurrent();
            if (currentUser != null && (currentUser.UserRole.Name == Role.Admin.ToString() || currentUser.UserRole.Name == Role.User.ToString()))
            {
                var searchModel = new SearchModel();
 
                searchModel.ActivityTypes = this.POLineServices.GetAllActivityTypes();
                searchModel.Owners = this.POLineServices.GetAllOwners();
                var assignmentCode= this.POLineServices.FindAllWBSORAssignmentCode();
                searchModel.AssignmentCodes = assignmentCode.Select(x => new DDLModel() { text = x, value = x }).ToList();
                //searchModel.WBS = assignmentCode.Select(x => new DDLModel() { text = x, value = x }).ToList();
                var wbsElements = this.POLineServices.FindPOLineAsQueryableNew().Where(x => x.WbsElement != null).Select(x => x.WbsElement).ToList();
                var wbsElementsIds = wbsElements.Select(x => x.WbsElementID).Distinct().ToList();

                var itemWbs = this.POLineServices.GetAllWbs().Where(x => wbsElementsIds.Contains(x.WbsElementID));

                searchModel.WBS = itemWbs.Distinct().Select(x => new DDLModel() { text = x.Name, value = x.WbsElementID.ToString() }).ToList();
                searchModel.ContractTypes = this.POLineServices.GetAllContractTypes();
                searchModel.ActivityTypes = this.POLineServices.GetAllActivityTypes();
                searchModel.Applications= this.POLineServices.GetAllApplications();
                searchModel.Requestores= this.POLineServices.FindAllRequesterName().Where(x => !string.IsNullOrEmpty(x)).Distinct().Select(x => new DDLModel(){ text = x, value = x });
                return View("Index", searchModel);
            }
            return View("AccessDenied");
        }

        /// <summary>
        /// Navigates into the about application view.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult About() => View("About");

        //[HttpPost]
        //public JsonResult GetListOfPODatas(string textSearch = "")
        //{
        //    var userIsRegistered = UserService.GetListOfPODatas();
        //    // return Json(new object);
        //}
        [NHibernateMvcSessionContext]
        public ActionResult CurrentUserName()
        {
            var currenUser = UserService.GetCurrent();
            if (currenUser != null)
            {
                return Json(new { userName= currenUser.Name,userId= currenUser.UserID }, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
       

}
}