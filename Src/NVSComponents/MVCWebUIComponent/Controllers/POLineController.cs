namespace Volvo.LAT.MVCWebUIComponent.Controllers
{
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.Practices.Unity;
    using NHibernate;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Xml.Linq;
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

    /// <summary>
    /// The main application controller.
    /// </summary>
    public class POLineController : BaseController
    {
        protected IPosSessionHelper SessionHelper { get; set; }
        protected IPOLineService POLineServices { get; set; }
        protected IApplicationService ApplicationServices { get; set; }
        protected IContractTypeService ContractTypeService { get; set; }
        protected IPurchaseOrderService PurchaseOrderService { get; set; }
        protected IInvoiceReportService InvoiceReportServices { get; set; }
        protected IUserService UserService { get; set; }
        protected ICostListService CostListService { get; set; }

        [InjectionConstructor]
        public POLineController()
            : this(
                  Container.Resolve<ILocalizationHelper>(),
                  Container.Resolve<IThemesHelper>(),
                  Container.Resolve<IBundlingHelper>(),
                  Container.Resolve<IPosSessionHelper>(),
                  Container.Resolve<IPOLineService>(),
                  Container.Resolve<IApplicationService>(),
                  Container.Resolve<IContractTypeService>(),
                  Container.Resolve<IPurchaseOrderService>(),
                  Container.Resolve<IInvoiceReportService>(),
                  Container.Resolve<IUserService>(),
                  Container.Resolve<ICostListService>())
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
        public POLineController(
            ILocalizationHelper localizationHelper,
            IThemesHelper themesHelper,
            IBundlingHelper bundlingHelper,
            IPosSessionHelper sessionHelper,
            IPOLineService pOLineServices,
            IApplicationService applicationService,
            IContractTypeService contractTypeService,
            IPurchaseOrderService purchaseOrderService,
            IInvoiceReportService invoiceReportService,
            IUserService userService,
            ICostListService costListService)
            : base(localizationHelper, themesHelper, bundlingHelper)
        {
            this.SessionHelper = sessionHelper;
            this.POLineServices = pOLineServices;
            this.ApplicationServices = applicationService;
            this.ContractTypeService = contractTypeService;
            this.PurchaseOrderService = purchaseOrderService;
            this.InvoiceReportServices = invoiceReportService;
            this.UserService = userService;
            this.CostListService = costListService;
        }

        /// <summary>
        /// Navigates into the about application view.
        /// </summary>
        /// <returns>Action result.</returns>
        /// 
        [NHibernateMvcSessionContext]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View("searchPOs");
        }

        private IQueryable<POLine> SortGridQuerable(IQueryable<POLine> list, string member, string sortDirection)
        {
            switch (member)
            {
                case "EbdNumber":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.EbdNumber);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.EbdNumber);
                        }
                        break;
                    }
                case "PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.OwnerName);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.OwnerName);
                        }
                        break;
                    }
                case "PoLine":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.PoLine);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.PoLine);
                        }
                        break;
                    }
                case "AcOrWbs":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.AcOrWbs);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.AcOrWbs);
                        }
                        break;
                    }

                case "StartDate":
                    {
                        if (sortDirection == "Ascending")
                        {
                            //  list = list.OrderBy(x => x.StartDate);
                        }
                        else
                        {
                            //  list = list.OrderByDescending(x => x.StartDate);
                        }
                        break;
                    }
                case "EndDate":
                    {
                        if (sortDirection == "Ascending")
                        {
                            //  list = list.OrderBy(x => x.EndDate);
                        }
                        else
                        {
                            // list = list.OrderByDescending(x => x.EndDate);
                        }
                        break;
                    }
                case "CostCenter.FullName":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.CostCenter.FullName);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.CostCenter.FullName);
                        }
                        break;
                    }
                case "PurchaseOrderLineFromEbd.PurchaseOrder.VendorName":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.PurchaseOrderLineFromEbd.PurchaseOrder.VendorName);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.PurchaseOrderLineFromEbd.PurchaseOrder.VendorName);
                        }
                        break;
                    }
                case "ContractType.Name":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.ContractType.Name);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.ContractType.Name);
                        }
                        break;
                    }
                case "App.Name":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.App.Name);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.App.Name);
                        }
                        break;
                    }
                case "App.ApmId":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.App.ApmId);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.App.ApmId);
                        }
                        break;
                    }
                case "WbsElement.Name":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.WbsElement.Name);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.WbsElement.Name);
                        }
                        break;
                    }
                case "RequestorName":
                    {
                        if (sortDirection == "Ascending")
                        {
                            // list = list.OrderBy(x => x.RequestorName);
                        }
                        else
                        {
                            // list = list.OrderByDescending(x => x.RequestorName);
                        }
                        break;
                    }
            }
            return list;
        }

        private IEnumerable<POLine> SortGrid(IEnumerable<POLine> list, string member, string sortDirection)
        {
            switch (member)
            {
                case "EbdNumber":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.EbdNumber);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.EbdNumber);
                        }
                        break;
                    }
                case "PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.OwnerName);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.OwnerName);
                        }
                        break;
                    }
                case "PoLine":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.PoLine);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.PoLine);
                        }
                        break;
                    }
                case "AcOrWbs":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.AcOrWbs);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.AcOrWbs);
                        }
                        break;
                    }

                case "StartDate":
                    {
                        if (sortDirection == "Ascending")
                        {
                            //list = list.OrderBy(x => x.StartDate);
                        }
                        else
                        {
                            //  list = list.OrderByDescending(x => x.StartDate);
                        }
                        break;
                    }
                case "EndDate":
                    {
                        if (sortDirection == "Ascending")
                        {
                            // list = list.OrderBy(x => x.EndDate);
                        }
                        else
                        {
                            // list = list.OrderByDescending(x => x.EndDate);
                        }
                        break;
                    }
                case "CostCenter.FullName":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.CostCenter.FullName);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.CostCenter.FullName);
                        }
                        break;
                    }
                case "PurchaseOrderLineFromEbd.PurchaseOrder.VendorName":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.PurchaseOrderLineFromEbd.PurchaseOrder.VendorName);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.PurchaseOrderLineFromEbd.PurchaseOrder.VendorName);
                        }
                        break;
                    }
                case "ContractType.Name":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.ContractType.Name);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.ContractType.Name);
                        }
                        break;
                    }
                case "App.Name":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.App.Name);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.App.Name);
                        }
                        break;
                    }
                case "App.ApmId":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.App.ApmId);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.App.ApmId);
                        }
                        break;
                    }
                case "WbsElement.Name":
                    {
                        if (sortDirection == "Ascending")
                        {
                            list = list.OrderBy(x => x.WbsElement.Name);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.WbsElement.Name);
                        }
                        break;
                    }
                case "RequestorName":
                    {
                        if (sortDirection == "Ascending")
                        {
                            //  list = list.OrderBy(x => x.RequestorName);
                        }
                        else
                        {
                            // list = list.OrderByDescending(x => x.RequestorName);
                        }
                        break;
                    }
            }
            return list;
        }

        [NHibernateMvcSessionContext]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult POLineGridReadOld([DataSourceRequest]DataSourceRequest request, string searchText,
            bool isAdvancedSearch = false, DateTime? startDate = null, DateTime? endDate = null, string applicationId = null,
            string ownerName = null, string requesterName = null, string wbs = null, string assignmentCode = null, string contractTypeId = null,
            bool isRenewalYes = false, bool isRenewalNo = false, bool isRenewalAll = false)
        {
            var strtDate = (startDate.HasValue) ? startDate.Value : DateTime.MinValue;
            var endDte = (endDate.HasValue) ? endDate.Value : DateTime.MinValue;
            int totalRecords;
            DataSourceResult result = new DataSourceResult();
            if (request == null)
            {
                throw new System.ArgumentNullException("request");
            }

            if (string.IsNullOrEmpty(searchText) && !isAdvancedSearch)
            {
                var rrr = this.POLineServices.FindPOLineAsQueryable(request.Page, request.PageSize, out totalRecords);
                var response = new List<POLine>();
                if (request.Sorts.Count > 0)
                {
                    response = this.SortGrid(rrr, request.Sorts[0].Member, request.Sorts[0].SortDirection.ToString()).ToList();
                }
                else
                {
                    response = rrr.ToList();
                }
                result.Data = response;
                result.Total = totalRecords;
                // result.Data = rrr;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (isAdvancedSearch)
                {
                    var response = this.POLineServices.FindPOLineAsQueryable1(searchText, false, strtDate, endDte, applicationId, ownerName, requesterName, wbs, assignmentCode, contractTypeId, isRenewalYes, isRenewalNo, isRenewalAll, request.PageSize, request.Page, out totalRecords);
                    result.Total = totalRecords;
                    if (request.Sorts.Count > 0)
                    {
                        response = this.SortGrid(response, request.Sorts[0].Member, request.Sorts[0].SortDirection.ToString());
                    }
                    result.Data = response;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var response2 = this.POLineServices.FindPOLineAsQueryable1(searchText, false, strtDate, endDte, applicationId, ownerName, requesterName, wbs, assignmentCode, contractTypeId, isRenewalYes, isRenewalNo, isRenewalAll, request.PageSize, request.Page, out totalRecords).ToDataSourceResult(request);
                    response2.Total = totalRecords;
                    // result.Data = response2;
                    return Json(response2, JsonRequestBehavior.AllowGet);
                }

            }
        }

        [NHibernateMvcSessionContext]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult POLineGridRead([DataSourceRequest]DataSourceRequest request, string searchText,
            bool isAdvancedSearch = false, DateTime? startDate = null, DateTime? endDate = null, string applicationId = null,
            string ownerName = null, string requesterName = null, string wbs = null, string assignmentCode = null, string contractTypeId = null,
            bool isRenewalYes = false, bool isRenewalNo = false, bool isRenewalAll = false, bool isMyPurchaseOrder = false, bool IsMyRenewalYes = false)
        {
            var strtDate = (startDate.HasValue) ? startDate.Value : DateTime.MinValue;
            var endDte = (endDate.HasValue) ? endDate.Value : DateTime.MinValue;
            int totalRecords;

            var purchaseorder = Request.QueryString["purchaseorder"];
            var renewals = Request.QueryString["renewals"];

            DataSourceResult result = new DataSourceResult();
            if (request == null)
            {
                throw new System.ArgumentNullException("request");
            }
            if (isAdvancedSearch)
            {
                if ((searchText == "") && (startDate == null) && endDate == null
                      && (applicationId == "") && (ownerName == "") && (requesterName == "") && (wbs == "") && (assignmentCode == "")
                      && (contractTypeId == "") && (isRenewalYes == false) && isRenewalNo == false && isRenewalAll == false)

                    isAdvancedSearch = false;
            }
            if (string.IsNullOrEmpty(searchText) && !isAdvancedSearch)
            {
                var rrr = this.POLineServices.FindPOLineAsQueryableNew().AsQueryable();
                var response = new List<POLine>();
                if (request.Sorts.Count > 0)
                {
                    rrr = this.SortGridQuerable(rrr, request.Sorts[0].Member, request.Sorts[0].SortDirection.ToString());
                    totalRecords = rrr.Count();

                    var pageRecords = request.Page > 1 ? (request.Page - 1) * request.PageSize : 0;
                    response = rrr.Take(request.PageSize).Skip(pageRecords).ToList();//.Skip(request.Page).ToList();
                }
                else
                {
                    totalRecords = rrr.Count();
                    var pageRecords = request.Page > 1 ? (request.Page - 1) * request.PageSize : 0;
                    response = rrr.OrderBy(x => x.EbdNumber).OrderBy(x => x.PoLine).Take(request.PageSize).Skip(pageRecords).ToList();//.Skip(request.Page).ToList();
                }
                result.Data = response;
                result.Total = totalRecords;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (isAdvancedSearch)
                {
                    var response = this.POLineServices.FindPOLineAsQueryable1(searchText, false, strtDate, endDte, applicationId,
                        ownerName, requesterName, wbs, assignmentCode, contractTypeId, isRenewalYes, isRenewalNo, isRenewalAll,
                        request.PageSize, request.Page, out totalRecords);


                    result.Total = totalRecords;
                    if (request.Sorts.Count > 0)
                    {
                        response = this.SortGrid(response, request.Sorts[0].Member, request.Sorts[0].SortDirection.ToString());
                    }
                    else
                    {
                        if (isAdvancedSearch)
                        {

                            if (strtDate != DateTime.MinValue)
                            {
                                // response = response.OrderBy(x => x.StartDate).ToList();

                            }
                            if (endDte != DateTime.MinValue && strtDate == DateTime.MinValue)
                            {
                                //  response = response.OrderBy(x => x.EndDate).ToList();
                            }
                        }
                        else
                            response = response.OrderBy(x => x.EbdNumber);
                    }
                    var pageRecords = request.Page > 1 ? (request.Page - 1) * request.PageSize : 0;
                    result.Data = response.Skip(pageRecords).Take(request.PageSize).ToList();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var pageRecords = request.Page > 1 ? (request.Page - 1) * request.PageSize : 0;
                    var response2 = this.POLineServices.FindPOLineAsQueryable1(searchText, false, strtDate, endDte, applicationId, ownerName, requesterName, wbs, assignmentCode, contractTypeId, isRenewalYes, isRenewalNo, isRenewalAll, request.PageSize, request.Page, out totalRecords).Skip(pageRecords).Take(request.PageSize).ToDataSourceResult(request);
                    response2.Total = totalRecords;
                    return Json(response2, JsonRequestBehavior.AllowGet);
                }

            }
        }


        [NHibernateMvcSessionContext]
        public JsonResult GetOwnerName()
        {

            var result = this.POLineServices.FindAllOwner1();

            return Json(result.Select(x => new { text = x, value = x }), JsonRequestBehavior.AllowGet);
        }


        [NHibernateMvcSessionContext]
        public JsonResult GetApplicationName()
        {
            var result = this.ApplicationServices.GetApplicationName();
            return Json(result.Select(x => new { text = x.Name, value = x.AppId }), JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        public JsonResult GetWBSORAssignmentCodeFromPOLine()
        {
            var result = this.POLineServices.FindAllWBSORAssignmentCode();
            return Json(result.Select(x => new { text = x, value = x }), JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        public JsonResult GetAllWbsItems()
        {
            var result = this.POLineServices.GetAllWbs().Select(x => x.Name).Take(1000).Distinct();
            return Json(result.Select(x => new { text = x, value = x }), JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        public JsonResult FindAllRequesterName()
        {
            var result = this.POLineServices.FindAllRequesterName();
            return Json(result.Where(x => !string.IsNullOrEmpty(x)).Distinct().Select(x => new { text = x, value = x }), JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        public JsonResult GetAllContractType()
        {
            var result = this.ContractTypeService.GetContractTypes();
            return Json(result.Select(x => new { text = x.Name, value = x.ContractTypeId }), JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        [HttpGet]
        public ActionResult GetPurchaseOrderDetail(string purchaseOrderId)
        {
            var purchaseOrder = this.PurchaseOrderService.GetPurchaseOrderByEBDNumber(purchaseOrderId);
            purchaseOrder.PurchaseOrderLineFromEbd = this.PurchaseOrderService.GetPurchaseOrderLineEBD(purchaseOrder.PurchaseOrderId);



            var result = this.PurchaseOrderService.GetPurchaseOrderDetail(purchaseOrderId);
            var groupResult = result.GroupBy(x => x.PoLine);



            if (result != null && result.Any())
            {
                decimal totalOrderAmunt = 0;
                decimal totalSeekAmount = 0;
                decimal splitLineItemAmount = 0;
                var searchModel = new SearchModel();
                searchModel.ActivityTypes = this.POLineServices.GetAllActivityTypes();
                searchModel.Owners = this.POLineServices.GetAllOwners();
                var assignmentCode = this.POLineServices.FindAllWBSORAssignmentCode();
                searchModel.AssignmentCodes = assignmentCode.Select(x => new DDLModel() { text = x, value = x }).ToList();
                var wbsElements = this.POLineServices.FindPOLineAsQueryableNew().Where(x => x.WbsElement != null).Select(x => x.WbsElement).ToList();
                var wbsElementsIds = wbsElements.Select(x => x.WbsElementID).Distinct().ToList();

                var itemWbs = this.POLineServices.GetAllWbs().Where(x => wbsElementsIds.Contains(x.WbsElementID));

                searchModel.WBS = itemWbs.Distinct().Select(x => new DDLModel() { text = x.Name, value = x.WbsElementID.ToString() }).ToList();
                searchModel.ContractTypes = this.POLineServices.GetAllContractTypes();
                searchModel.Applications = this.POLineServices.GetAllApplications();
                searchModel.Requestores = this.POLineServices.FindAllRequesterName().Where(x => !string.IsNullOrEmpty(x)).Distinct().Select(x => new DDLModel() { text = x, value = x });
                result.ForEach(a =>
                {


                    var year = Convert.ToDateTime(a.StartDate);
                    var seekRate = this.POLineServices.GetPOlineSeekAmount(a.Currency, year.Year);

                    decimal exchangeRateCurrencyRate = 0;

                    if (a.ExchangeRateYear.HasValue)
                    {
                        exchangeRateCurrencyRate = this.POLineServices.GetPOlineSeekAmount(a.Currency, a.ExchangeRateYear.Value);
                    }
                    a.SekRate = exchangeRateCurrencyRate > 0 ? exchangeRateCurrencyRate : seekRate;
                    a.ExchangeRateCurrencyRate = exchangeRateCurrencyRate;
                    a.LastChangeByName = string.Empty;
                    totalOrderAmunt = totalOrderAmunt + Convert.ToDecimal(a.OrderAmount);
                    totalSeekAmount = totalSeekAmount + (Convert.ToDecimal(a.OrderAmount) * a.SekRate);
                    splitLineItemAmount = splitLineItemAmount + Convert.ToDecimal(a.SplitLineItemAmount);
                    a.Numberofchargableamount = NumberOfChargeMonths(a.StartDate, a.EndDate, a.EarlierPaymentDate, a.DelayedDate);
                    if (a.EndDate.HasValue)
                    {
                        var today = DateTime.Now;
                        var endDay = Convert.ToDateTime(a.EndDate);
                        var daysLeft = (today - endDay).TotalDays;
                        a.RenewalTotalDaysLeft = Convert.ToInt32(daysLeft);
                    }
                    a.Applications = this.POLineServices.GetAllApplications();
                    a.ActivityTypes = this.POLineServices.GetAllActivityTypes();
                    a.ContractTypes = this.POLineServices.GetAllContractTypes();
                    a.Owners = this.POLineServices.GetAllOwners();
                    a.StatusPo = this.POLineServices.GetAllStatus();
                    a.SearchModelDetail = searchModel;

                });
                var user = this.UserService.GetUserBtUserId(result.FirstOrDefault().LastChangeBy);
                result.ForEach(c =>
                {
                    c.OrderamountAmount = totalOrderAmunt;
                    c.OrderamountinSEK = Convert.ToString(totalSeekAmount);
                    c.LeftTotalPoAmount = splitLineItemAmount;
                    c.LastChangeByName = user != null ? user.Name : string.Empty;
                });
            }

            return this.View("PurchaseOrderDetail", result);
        }

        [NHibernateMvcSessionContext]
        public ActionResult UpdateAssignment(AssignmentCode assignment)
        {

            this.PurchaseOrderService.SaveAssignmentCodeDetails(assignment);
            return Json(true, JsonRequestBehavior.AllowGet);


        }

        [NHibernateMvcSessionContext]
        public ActionResult CheckIfPoNumberExistsInDB(string poNumber)
        {
            var result = this.POLineServices.FindPOLineAsQueryableNew().Where(x => x.EbdNumber == poNumber);
            if (result.ToList().Count() > 0)
            {
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        [HttpGet]
        public ActionResult GetRenewalDaysLeft(string endDate, string renewalOrderLine)
        {
            var today = DateTime.Now;
            if (string.IsNullOrEmpty(endDate) || endDate == "null")
            {
                return this.Json(0, JsonRequestBehavior.AllowGet);
            }
            var endDay = Convert.ToDateTime(endDate);

            var daysLeft = (today - endDay).TotalDays;
            if (renewalOrderLine != null && string.Compare(renewalOrderLine, "") != 0)
            {
                bool isExistigRenewalLine = this.POLineServices.CheckRenewalOrderLineExist(renewalOrderLine);
                return this.Json(new { DaysLeft = Convert.ToInt32(daysLeft), IsRenewalValid = isExistigRenewalLine }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(Convert.ToInt32(daysLeft), JsonRequestBehavior.AllowGet);
            }
        }

        [NHibernateMvcSessionContext]
        [HttpGet]
        public JsonResult GellAllOwners()
        {
            var result = this.POLineServices.GetAllOwners();
            return this.Json(result.OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        [HttpGet]
        public JsonResult GellAllStatus()
        {
            var result = this.POLineServices.GetAllStatus();
            return this.Json(result.OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        [HttpGet]
        public JsonResult GetAllContractTypes()
        {
            var result = this.POLineServices.GetAllContractTypes();
            return this.Json(result.OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        [HttpGet]
        public JsonResult GetAllApplications()
        {
            var result = this.POLineServices.GetAllApplications();
            return this.Json(result.OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        [NHibernateMvcSessionContext]
        [HttpGet]
        public JsonResult GetActivityTypes()
        {
            var result = this.POLineServices.GetAllActivityTypes();
            return this.Json(result.OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }


        [NHibernateMvcSessionContext]
        [HttpGet]
        public JsonResult GellAllProducts()
        {
            var result = this.POLineServices.GellAllProducts();
            return this.Json(result.OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        [NHibernateMvcSessionContext]
        [HttpGet]
        public JsonResult GellAllCostTypes()
        {
            var result = this.POLineServices.GellAllCostTypes();
            return this.Json(result.OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        public JsonResult SaveDetail(POLineSaveViewModel pOLineSave)
        {
            int renwalDaysLeft = 0;
            var currentUser = this.UserService.GetCurrent();
            var result = this.POLineServices.FindPolineByPurchaseOrderLineID(pOLineSave.PurchaseOrderLineId);
            var assignment = this.PurchaseOrderService.GetAssignmentCode(pOLineSave.PurchaseOrderLineId);
            if (result != null)
            {
                result.StatusPoID = pOLineSave.StatusPoId;
                result.Remark = pOLineSave.Remark;
                result.AcOrWbs = pOLineSave.AcOrWbs;
                result.ActivityTypeId = pOLineSave.ActivityTypeId;
                result.AppId = pOLineSave.AppId;
                assignment.ContactPerson = pOLineSave.ContactPerson;
                assignment.RequestorName = string.IsNullOrEmpty(pOLineSave.RequestorName) ? null : pOLineSave.RequestorName;
                if (pOLineSave.ContractEndDate != null)
                {
                    assignment.EndDate = Convert.ToDateTime(pOLineSave.ContractEndDate);
                }
                else
                    assignment.EndDate = null;

                if (pOLineSave.ContractStartDate != null)
                {
                    assignment.StartDate = Convert.ToDateTime(pOLineSave.ContractStartDate);
                }
                else
                    assignment.StartDate = null;
                result.ContractTypeId = pOLineSave.ContractTypeId;

                if (pOLineSave.EarlierPaymentDate != null)
                {
                    assignment.EarlierPaymentDate = Convert.ToDateTime(pOLineSave.EarlierPaymentDate);
                }
                else
                {
                    assignment.EarlierPaymentDate = null;
                }


                assignment.ExchangeRateYear = pOLineSave.ExchangeRateYear;
                //  assignment.MonthlyRate= pOLineSave.
                result.InvoiceNumber = pOLineSave.InvoiceNumber;
                result.PurchaseOrderLineFromEbd.PurchaseOrder.InvoiceNumber = pOLineSave.InvoiceNumberHeader;
                result.LastChangeDate = DateTime.Now;
                result.ProductId = pOLineSave.ProductId;
                result.ProductNumber = pOLineSave.ProductNumber;
                result.Renewal = pOLineSave.Renewal;
                result.OwnerId = pOLineSave.OwnerId;
                result.CostTypeId = pOLineSave.CostTypeId;
                assignment.SplitLineItemAmount = pOLineSave.RechargeAmount;
                if (pOLineSave.RenewalOrderPurchaseLine != null)
                {
                    var renewalorderlineSplit = pOLineSave.RenewalOrderPurchaseLine.Split('_');
                    if (renewalorderlineSplit[0] != result.EbdNumber)
                        result.RenewalOrderPurchaseLine = pOLineSave.RenewalOrderPurchaseLine;
                }
                result.PurchaseOrderLineFromEbd.PurchaseOrder.PurchaserName = string.IsNullOrEmpty(pOLineSave.PurchaserName) ? null : pOLineSave.PurchaserName;
                result.LastChangeBy = currentUser.UserID;
                // result.Comments = POLineSave.Comments;
                if (pOLineSave.DelayedPaymentDate != null)
                {
                    assignment.DelayedDate = Convert.ToDateTime(pOLineSave.DelayedPaymentDate);
                }
                else
                {
                    assignment.DelayedDate = null;
                }

                this.POLineServices.SaveUpdateDetail(result, pOLineSave.Comments);
                this.PurchaseOrderService.SaveAssignmentCodeDetails(assignment);

                if (assignment.EndDate.HasValue)
                {
                    var today = DateTime.Now;
                    var endDay = Convert.ToDateTime(assignment.EndDate);
                    var daysLeft = (today - endDay).TotalDays;
                    renwalDaysLeft = Convert.ToInt32(daysLeft);
                }

                var totalCurrntMonths = NumberOfChargeMonths(assignment.StartDate, assignment.EndDate, assignment.EarlierPaymentDate, assignment.DelayedDate);

                return this.Json(new { statusO = true, renewaDaysLeft = renwalDaysLeft, lastChangedBy = currentUser.Name, lastUpdatedDate = DateTime.Now.ToString("MM/dd/yyyy hh:ss tt"), TotalMonths = totalCurrntMonths }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { statusO = false, renewaDaysLeft = renwalDaysLeft, lastChangedBy = currentUser.Name, lastUpdatedDate = DateTime.Now.ToString("MM/dd/yyyy hh:ss tt") }, JsonRequestBehavior.AllowGet);
        }


        private int NumberOfChargeMonths(DateTime? start, DateTime? end, DateTime? early, DateTime? delay)
        {
            int totalMonths = 0;
            if (start == null || end == null)
            {
                return 0;
            }
            if (early == null && delay == null)
            {
                int m1 = end.Value.Month - start.Value.Month;//for years
                int m2 = (end.Value.Year - start.Value.Year) * 12; //for months
                if (m1 < 0)
                {
                    totalMonths = m2 + m1 + 1;

                }
                else
                    totalMonths = m1 + m2 + 1;

            }
            else if (delay == null && early != null)
            {
                int m1 = early.Value.Month - start.Value.Month;//for years
                int m2 = (early.Value.Year - start.Value.Year) * 12; //for months
                if (m1 < 0)
                {
                    totalMonths = m2 + m1 + 1;

                }
                else
                    totalMonths = m1 + m2 + 1;

            }
            else if (delay != null && early == null)
            {
                int m1 = end.Value.Month - delay.Value.Month;//for years
                int m2 = (end.Value.Year - delay.Value.Year) * 12; //for months
                if (m1 < 0)
                {
                    totalMonths = m2 + m1 + 1;

                }
                else
                    totalMonths = m1 + m2 + 1;


            }
            else if (delay != null && early != null)
            {
                int m1 = early.Value.Month - delay.Value.Month;//for years
                int m2 = (early.Value.Year - delay.Value.Year) * 12; //for months
                if (m1 < 0)
                {
                    totalMonths = m2 + m1 + 1;

                }
                else
                    totalMonths = m1 + m2 + 1;

            }
            return Math.Abs(totalMonths);
        }
        [NHibernateMvcSessionContext]
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task FillMonthlyCostListData(string poLineNumber)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM");

            List<Tuple<string, string, decimal>> tupleRstlList = new List<Tuple<string, string, decimal>>();

            var poLinesRecords = this.POLineServices.FindPOLineAsQueryable().Select(x => new { poNumber = x.EbdNumber, polineId = x.PurchaseOrderLineId }).Distinct().ToList();
            var distinctPoNumbers = poLinesRecords.Select(x => x.poNumber).Distinct().ToList();
            foreach (var poNumber in distinctPoNumbers)
            {
                decimal currentMonthCost = 0;
                string polineIdVar = string.Empty;
                var multiplePORecords = poLinesRecords.Where(x => x.poNumber == poNumber).ToList();
                foreach (var item in multiplePORecords)
                {
                    polineIdVar = Convert.ToString(item.polineId);
                    var records = "";// GetCostListData(Convert.ToString(item.polineId));
                    //if (records != null && records.Item1 != null && records.Item1.Count > 0)
                    //{
                    //    if (records.Item1.Any(x => x.DateAndYear == currentDate))
                    //    {
                    //        var currentRec = records.Item1.FirstOrDefault(x => x.DateAndYear == currentDate);
                    //        if (currentRec != null)
                    //        {
                    //            currentMonthCost = currentRec.Cost;
                    //        }
                    //        if (!(currentMonthCost > 0))
                    //        {
                    //            if (records.Item2 > 0)
                    //            {
                    //                currentMonthCost = records.Item2;
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (records.Item2 > 0)
                    //        {
                    //            currentMonthCost = records.Item2;
                    //        }
                    //    }
                    //}

                    tupleRstlList.Add(new Tuple<string, string, decimal>(poNumber, polineIdVar, currentMonthCost));
                }

            }
            this.POLineServices.SaveUpdateMonthlyRate(tupleRstlList);

        }

        [NHibernateMvcSessionContext]
        public Tuple<List<CostListViewModel>, decimal, List<CustomModelSecondGrid>> GetCostListData(string poLineNumber, bool ignoreSecondGridData = false)
        {
            var monthDuration = 0;
            var item = this.POLineServices.GetById(new Guid(poLineNumber));
            var assignment = this.PurchaseOrderService.GetAssignmentCode(item.PurchaseOrderLineId.ToString());
            var costlist = new List<CostListViewModel>();
            var currentMonthYear = DateTime.Now.ToString("yyyy-MM");
            var tuppleList = new List<Tuple<string, string, decimal>>();
            var tuppleListMonthly = new List<Tuple<string, string, decimal>>();

            decimal monthlyAmountForView = 0;
            List<CustomModelSecondGrid> secondGridData = new List<CustomModelSecondGrid>();
            if (item == null)
            {
                return new Tuple<List<CostListViewModel>, decimal, List<CustomModelSecondGrid>>(costlist, decimal.Round(monthlyAmountForView, 2, MidpointRounding.AwayFromZero), secondGridData);
            }
            if (assignment.StartDate.HasValue && assignment.EndDate.HasValue)
            {
                var startDateToEndDateMonths = Enumerable.Range(0, int.MaxValue)
                                 .Select(e => assignment.StartDate.Value.AddMonths(e))
                                 .TakeWhile(e => e <= assignment.EndDate)
                                 .Select(e => e.ToString("yyyy-MM"));

                var totalMonthsCountStartDateToEndDate = startDateToEndDateMonths.Count();
                var perMonthCalculatedAmount = assignment.SplitLineItemAmount / totalMonthsCountStartDateToEndDate;
                monthlyAmountForView = perMonthCalculatedAmount.HasValue ? perMonthCalculatedAmount.Value : 0;

                if (assignment.EarlierPaymentDate == null && assignment.DelayedDate == null)
                {
                    var case1Result = startDateToEndDateMonths.Select(x => new CostListViewModel
                    {
                        DateAndYear = x,
                        Cost = perMonthCalculatedAmount.HasValue ? decimal.Round(perMonthCalculatedAmount.Value, 2, MidpointRounding.AwayFromZero) : 0,
                        PoLineId = item.PurchaseOrderLineId.ToString(),
                        PoNumber = item.EbdNumber
                    }).ToList();
                    costlist = case1Result;
                }
                else if (assignment.EarlierPaymentDate == null && assignment.DelayedDate != null)
                {
                    var startDateToDelayedDateMonths = Enumerable.Range(0, int.MaxValue)
                             .Select(e => assignment.StartDate.Value.AddMonths(e))
                             .TakeWhile(e => e <= assignment.DelayedDate)
                             .Select(e => e.ToString("yyyy-MM"));
                    var startDateToDelayedDateMonthCount = startDateToDelayedDateMonths.Count();
                    foreach (var element in startDateToEndDateMonths)
                    {
                        var costlistVm = new CostListViewModel();
                        var tempDate = Convert.ToDateTime(element);
                        if ((tempDate.Year < assignment.DelayedDate.Value.Year)
                            || (tempDate.Year == assignment.DelayedDate.Value.Year
                                    && tempDate.Month < assignment.DelayedDate.Value.Month))
                        {
                            costlistVm.Cost = 0;
                            costlistVm.DateAndYear = tempDate.ToString("yyyy-MM");
                            //New
                            costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                            costlistVm.PoNumber = item.EbdNumber;
                        }
                        else if (tempDate.Year == assignment.DelayedDate.Value.Year
                                    && tempDate.Month == assignment.DelayedDate.Value.Month)
                        {
                            costlistVm.Cost = (perMonthCalculatedAmount.HasValue
                                                        && startDateToDelayedDateMonthCount > 0
                                                            && perMonthCalculatedAmount.Value > 0)
                                                                    ? startDateToDelayedDateMonthCount * perMonthCalculatedAmount.Value
                                                                    : 0;
                            costlistVm.Cost = decimal.Round(costlistVm.Cost, 2, MidpointRounding.AwayFromZero);
                            costlistVm.DateAndYear = tempDate.ToString("yyyy-MM");
                            //New
                            costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                            costlistVm.PoNumber = item.EbdNumber;
                        }
                        else
                        {
                            costlistVm.Cost = perMonthCalculatedAmount.HasValue ? perMonthCalculatedAmount.Value : 0;
                            costlistVm.DateAndYear = element;
                            //New
                            costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                            costlistVm.PoNumber = item.EbdNumber;
                        }

                        costlist.Add(costlistVm);
                    }
                }
                else if (assignment.EarlierPaymentDate != null && assignment.DelayedDate == null)
                {

                    var startDateToEarlierPaymentDateMonths = Enumerable.Range(0, int.MaxValue)
                             .Select(e => assignment.StartDate.Value.AddMonths(e))
                             .TakeWhile(e => e <= assignment.EarlierPaymentDate)
                             .Select(e => e.ToString("yyyy-MM"));


                    var startDateToEarlierDateMonthCount = startDateToEarlierPaymentDateMonths.Count();
                    decimal? perMonthAmountAsPerEarlierPaymentDate = 0;
                    if (startDateToEarlierDateMonthCount > 0 && assignment.SplitLineItemAmount > 0)
                    {
                        perMonthAmountAsPerEarlierPaymentDate = assignment.SplitLineItemAmount / startDateToEarlierDateMonthCount;
                    }
                    monthlyAmountForView = perMonthAmountAsPerEarlierPaymentDate.Value;

                    foreach (var element in startDateToEndDateMonths)
                    {
                        var costlistVm = new CostListViewModel();
                        var tempDate = Convert.ToDateTime(element);
                        if ((tempDate.Year < assignment.EarlierPaymentDate.Value.Year)
                            || (tempDate.Year == assignment.EarlierPaymentDate.Value.Year
                                    && tempDate.Month <= assignment.EarlierPaymentDate.Value.Month))
                        {
                            costlistVm.Cost = perMonthAmountAsPerEarlierPaymentDate.HasValue ? perMonthAmountAsPerEarlierPaymentDate.Value : 0;
                            costlistVm.Cost = decimal.Round(costlistVm.Cost, 2, MidpointRounding.AwayFromZero);
                            costlistVm.DateAndYear = tempDate.ToString("yyyy-MM");
                            //New
                            costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                            costlistVm.PoNumber = item.EbdNumber;
                        }
                        else
                        {
                            costlistVm.Cost = 0;
                            costlistVm.DateAndYear = element;
                            //New
                            costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                            costlistVm.PoNumber = item.EbdNumber;
                        }

                        costlist.Add(costlistVm);
                    }
                }
                else if (assignment.EarlierPaymentDate != null && assignment.DelayedDate != null)
                {
                    if (assignment.EarlierPaymentDate.Value.Year == assignment.DelayedDate.Value.Year
                                && assignment.EarlierPaymentDate.Value.Month == assignment.DelayedDate.Value.Month)
                    {
                        foreach (var element in startDateToEndDateMonths)
                        {
                            var costlistVm = new CostListViewModel();
                            var tempDate = Convert.ToDateTime(element);
                            if (tempDate.Year == assignment.EarlierPaymentDate.Value.Year && tempDate.Month == assignment.EarlierPaymentDate.Value.Month)
                            {
                                costlistVm.Cost = assignment.SplitLineItemAmount.HasValue ? assignment.SplitLineItemAmount.Value : 0;
                                costlistVm.Cost = decimal.Round(costlistVm.Cost, 2, MidpointRounding.AwayFromZero);
                                costlistVm.DateAndYear = tempDate.ToString("yyyy-MM");
                                //New
                                costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                                costlistVm.PoNumber = item.EbdNumber;

                                var totalMonthsfromStartToEarlierMonth = Enumerable.Range(0, int.MaxValue)
                                 .Select(e => assignment.StartDate.Value.AddMonths(e))
                                 .TakeWhile(e => e <= assignment.EarlierPaymentDate)
                                 .Select(e => e.ToString("yyyy-MM"));
                                if (totalMonthsfromStartToEarlierMonth.Count() > 0)
                                {
                                    monthDuration = totalMonthsfromStartToEarlierMonth.Count();
                                }
                            }
                            else
                            {
                                costlistVm.Cost = 0;
                                costlistVm.DateAndYear = element;
                                //New
                                costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                                costlistVm.PoNumber = item.EbdNumber;
                            }

                            costlist.Add(costlistVm);
                        }

                    }
                    else
                    {
                        var startDateToDelayedDateMonths = Enumerable.Range(0, int.MaxValue)
                             .Select(e => assignment.StartDate.Value.AddMonths(e))
                             .TakeWhile(e => e <= assignment.DelayedDate)
                             .Select(e => e.ToString("yyyy-MM"));
                        Console.WriteLine("----------------------------------------------------------------");

                        // Retrive Start date to Delay date months count. THis is required because we need to calculated delayed month payment by 
                        // multiplying count with monthly amount.
                        var startDateToDelayedDateMonthCount = startDateToDelayedDateMonths.Count();

                        // Retrieve start date to earlier date months
                        var startDateToEarlierDateMonths = Enumerable.Range(0, int.MaxValue)
                            .Select(e => assignment.StartDate.Value.AddMonths(e))
                            .TakeWhile(e => e <= assignment.EarlierPaymentDate)
                            .Select(e => e.ToString("yyyy-MM"));
                        Console.WriteLine("----------------------------------------------------------------"); ;

                        // Retrieve start date to earlier date months count, this is required because we need to caluclated per month amount i.e. from start date
                        // to earlier payment date.
                        var startDateToEarlierDateMonthCount = startDateToEarlierDateMonths.Count();
                        // calculate permonth amount by divideing total splitlineamount with startDateToEarlierDateMonthCount.
                        var startToEarlierDateMonthlyAmount = assignment.SplitLineItemAmount / startDateToEarlierDateMonthCount;

                        monthlyAmountForView = startToEarlierDateMonthlyAmount.Value;

                        foreach (var element in startDateToEndDateMonths)
                        {
                            var costlistVm = new CostListViewModel();
                            var tempDate = Convert.ToDateTime(element);
                            if (((tempDate.Year < assignment.DelayedDate.Value.Year)
                                || (tempDate.Year == assignment.DelayedDate.Value.Year
                                && tempDate.Month < assignment.DelayedDate.Value.Month)) || (tempDate.Year > assignment.EarlierPaymentDate.Value.Year)
                                || (tempDate.Year == assignment.EarlierPaymentDate.Value.Year
                                && tempDate.Month > assignment.EarlierPaymentDate.Value.Month))
                            {
                                costlistVm.Cost = 0;
                                costlistVm.DateAndYear = element;
                                //New
                                costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                                costlistVm.PoNumber = item.EbdNumber;
                            }
                            else if (tempDate.Year == assignment.DelayedDate.Value.Year && tempDate.Month == assignment.DelayedDate.Value.Month)
                            {
                                costlistVm.Cost = startToEarlierDateMonthlyAmount.HasValue
                                                            && startToEarlierDateMonthlyAmount > 0
                                                            && startDateToDelayedDateMonthCount > 0
                                                                        ? startDateToDelayedDateMonthCount * startToEarlierDateMonthlyAmount.Value
                                                                        : 0;
                                costlistVm.Cost = decimal.Round(costlistVm.Cost, 2, MidpointRounding.AwayFromZero);
                                costlistVm.DateAndYear = element;
                                //New
                                costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                                costlistVm.PoNumber = item.EbdNumber;
                            }
                            else if ((tempDate.Year == assignment.EarlierPaymentDate.Value.Year && tempDate.Month <= assignment.EarlierPaymentDate.Value.Month)
                                        || (tempDate.Year == assignment.DelayedDate.Value.Year && tempDate.Month > assignment.DelayedDate.Value.Month))
                            {
                                costlistVm.Cost = decimal.Round(startToEarlierDateMonthlyAmount.Value, 2, MidpointRounding.AwayFromZero);
                                costlistVm.DateAndYear = element;
                                costlistVm.PoLineId = item.PurchaseOrderLineId.ToString();
                                costlistVm.PoNumber = item.EbdNumber;
                            }

                            costlist.Add(costlistVm);
                        }
                    }
                }

            }

//            if (!ignoreSecondGridData)
//            {
//                try
//                {
//                    this.ConvertAndSaveCostListData(costlist, item.EbdNumber, item.PurchaseOrderLineId.ToString());
//                }
//#pragma warning disable CS0168 // The variable 'ex' is declared but never used
//                catch (Exception ex) { }
//#pragma warning restore CS0168 // The variable 'ex' is declared but never used

//                secondGridData = this.GetSecondGridData(item.PurchaseOrderLineId.ToString());

//                if (secondGridData.Count() > 0)
//                {
//                    foreach (var rec in secondGridData)
//                    {
//                        if (monthlyAmountForView > 0)
//                        {
//                            rec.MonthlyRate = Convert.ToString(decimal.Round(monthlyAmountForView, 2, MidpointRounding.AwayFromZero));
//                        }
//                    }
//                }
//            }

            if (costlist.Any(x => x.DateAndYear == currentMonthYear))
            {
                var currentRec = costlist.FirstOrDefault(x => x.DateAndYear == currentMonthYear);
                if (currentRec != null)
                {
                    if (monthDuration > 0)
                    {
                        var perMonthCalculatedAmount = assignment.SplitLineItemAmount / monthDuration;
                        var monthlyAmount = perMonthCalculatedAmount.HasValue ? perMonthCalculatedAmount.Value : 0;
                        monthlyAmountForView = monthlyAmount;
                        tuppleList.Add(new Tuple<string, string, decimal>(item.EbdNumber, poLineNumber, decimal.Round(monthlyAmount, 2, MidpointRounding.AwayFromZero)));
                    }
                    else
                    {
                        tuppleList.Add(new Tuple<string, string, decimal>(item.EbdNumber, poLineNumber, decimal.Round(currentRec.Cost, 2, MidpointRounding.AwayFromZero)));
                    }
                }
                else
                {
                    if (monthDuration > 0)
                    {
                        var perMonthCalculatedAmount = assignment.SplitLineItemAmount / monthDuration;
                        var monthlyAmount = perMonthCalculatedAmount.HasValue ? perMonthCalculatedAmount.Value : 0;
                        monthlyAmountForView = monthlyAmount;
                        tuppleList.Add(new Tuple<string, string, decimal>(item.EbdNumber, poLineNumber, decimal.Round(monthlyAmount, 2, MidpointRounding.AwayFromZero)));
                    }
                    else
                    {
                        tuppleList.Add(new Tuple<string, string, decimal>(item.EbdNumber, poLineNumber, decimal.Round(monthlyAmountForView, 2, MidpointRounding.AwayFromZero)));
                    }
                }
            }
            else
            {
                if (monthDuration > 0)
                {
                    var perMonthCalculatedAmount = assignment.SplitLineItemAmount / monthDuration;
                    var monthlyAmount = perMonthCalculatedAmount.HasValue ? perMonthCalculatedAmount.Value : 0;
                    monthlyAmountForView = monthlyAmount;
                    tuppleList.Add(new Tuple<string, string, decimal>(item.EbdNumber, poLineNumber, decimal.Round(monthlyAmount, 2, MidpointRounding.AwayFromZero)));
                }
                else
                {
                    tuppleList.Add(new Tuple<string, string, decimal>(item.EbdNumber, poLineNumber, decimal.Round(monthlyAmountForView, 2, MidpointRounding.AwayFromZero)));
                }
            }

            if (tuppleList != null && tuppleList.Count > 0)
            {
                if (monthlyAmountForView > 0)
                {
                    tuppleListMonthly.Add(new Tuple<string, string, decimal>(item.EbdNumber, poLineNumber, decimal.Round(monthlyAmountForView, 2, MidpointRounding.AwayFromZero)));

                    this.POLineServices.SaveUpdateMonthlyRate(tuppleListMonthly);
                }
                else
                    this.POLineServices.SaveUpdateMonthlyRate(tuppleList);
            }
            secondGridData.ForEach(a =>
            {

                if (!string.IsNullOrEmpty(a.RechargeAmount))
                {
                    a.RechargeAmount = Math.Round(Convert.ToDecimal(a.RechargeAmount), 2).ToString();
                }

            });
            if (costlist.Count > 0)
            {
                this.ConvertAndSaveCostListData(costlist, costlist.FirstOrDefault().PoNumber, poLineNumber);
            }
            return new Tuple<List<CostListViewModel>, decimal, List<CustomModelSecondGrid>>(costlist, decimal.Round(monthlyAmountForView, 2, MidpointRounding.AwayFromZero), secondGridData);
        }

        private void ConvertAndSaveCostListData(List<CostListViewModel> vmList, string poNumber, string poLineId)
        {
            try
            {
                List<CostList> costList = new List<CostList>();
                foreach (var item in vmList)
                {
                    DateTime date;
                    DateTime.TryParse(item.DateAndYear, out date);
                    CostList costListObj = new CostList()
                    {
                        Cost = item.Cost,
                        Date = date,
                        PoLineId = item.PoLineId,
                        PoNumber = item.PoNumber,
                        CostListId = Guid.NewGuid(),
                    };
                    costList.Add(costListObj);
                }

                if (costList.Count > 0)
                {
                    this.CostListService.DeleteAndUpdateCostList(costList, poNumber, poLineId);
                }
            }
            catch (Exception ex)
            {

            }
        }

        [NHibernateMvcSessionContext]
        public JsonResult FillCostList(string poLineNumber)
        {
            var tupleResult = this.GetCostListData(poLineNumber);
            return this.Json(new { Costlist = tupleResult.Item1, MonthlyDividedAmount = tupleResult.Item2, SecondGridData = tupleResult.Item3 }, JsonRequestBehavior.AllowGet);

        }

        public List<CustomModelSecondGrid> GetSecondGridData(string poLineId)
        {
            var items = this.POLineServices.GetCustomModelSecondGridData(poLineId);
            return items;
        }

        [NHibernateMvcSessionContext]
        public ActionResult Reports(int id = 0)
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

                var itemWbs = this.POLineServices.GetAllWbs().Where(x => wbsElementsIds.Contains(x.WbsElementID));

                searchModel.WBS = itemWbs.Distinct().Select(x => new DDLModel() { text = x.Name, value = x.WbsElementID.ToString() }).ToList();
                searchModel.ContractTypes = this.POLineServices.GetAllContractTypes();
                searchModel.Applications = this.POLineServices.GetAllApplications();
                searchModel.Requestores = this.POLineServices.FindAllRequesterName().Where(x => !string.IsNullOrEmpty(x)).Distinct().Select(x => new DDLModel() { text = x, value = x });

                ViewBag.TabIndex = id;

                return View("reports", searchModel);
            }
            return View("AccessDenied");
        }

        [NHibernateMvcSessionContext]
        public JsonResult GenerateInvoiceReport(DateTime? selectedDate = null)
        {
            try
            {
                // XElement finalXML = null;
                XDocument finalXML = null;
                var currentUser = this.UserService.GetCurrent();
                var purchaseOrderRecords = this.POLineServices.GetAllPurchaseOrders().ToList();
                List<InvoiceReportRecordDTO> listInvoiceReportDTO = new List<InvoiceReportRecordDTO>();
                var records = this.POLineServices.FindPOLineAsQueryable();
                //.Where(x => (x.StartDate.HasValue && x.StartDate.Value.Year <= selectedDate.Value.Year
                //                && x.StartDate.Value.Month <= selectedDate.Value.Month)
                //                    && (x.EndDate.HasValue && x.EndDate.Value.Year >= selectedDate.Value.Year
                //                    && x.EndDate.Value.Month >= selectedDate.Value.Month));
                //.Where(x => x.StartDate <= selectedDate || x.EndDate >= selectedDate);

                if (records.ToList().Count() > 0)
                {
                    foreach (var item in records)
                    {
                        var costListData = this.CostListService.GetAllCostListsByPoLineId(item.PurchaseOrderLineId.ToString())
                            .FirstOrDefault(x => x.Date.Month == selectedDate.Value.Month && x.Date.Year == selectedDate.Value.Year);

                        //var purchaseOrder = purchaseOrderRecords.FirstOrDefault(x => x.PurchaseOrderId == item.PurchaseOrderLineId);
                        InvoiceReportRecordDTO obj = new InvoiceReportRecordDTO
                        {
                            AcOrWbs = item.AcOrWbs,
                            ActivityType = item.ActivityType.Name,

                            CostCenter = ((item.CostCenter != null) ? (item.CostCenter.CountryCode + "" + item.CostCenter.FullName) : null),

                            EbdNumber = item.EbdNumber,
                            InvoiceMasterId = Convert.ToString(item.PurchaseOrderLineId),
                            PoName = item.PurchaseOrderLineFromEbd.PurchaseOrder.PurchaseOrderName,
                            // Volume = (costListData != null && costListData.Cost > 0) ? Convert.ToString(costListData.Cost) : (item.MonthlyRate > 0) ? Convert.ToString(item.MonthlyRate) : string.Empty,
                        };
                        if (!string.IsNullOrEmpty(obj.Volume))
                            listInvoiceReportDTO.Add(obj);
                    }
                }

                //get the same month existing reports
                var sameMonthReports = this.InvoiceReportServices.GetInvoicingReports()
                                        .OrderByDescending(x => x.TimeStamp)
                                        .Where(x => x.Month == selectedDate.Value.Month && x.Year == selectedDate.Value.Year)
                                        .Select(x => new { InvoiceReportID = x.InvoicingReportID, XML = x.Xml, timeStamp = x.TimeStamp })
                                        .ToList();

                List<string> newerToOldReportInvoiceMasterIds = new List<string>();
                if (sameMonthReports.Count > 0)
                {
                    List<InvoiceReportRecordDTO> comparedAndCalculatedList = new List<InvoiceReportRecordDTO>();
                    foreach (var sameMnthRecord in sameMonthReports)
                    {
                        //parse the same month report's xml
                        XDocument xmlDoc;
                        xmlDoc = XDocument.Parse(sameMnthRecord.XML);
                        var xmlRecords = xmlDoc.Descendants("ArrayOfInvoicingReportRecord").Elements("InvoicingReportRecord").Select(x => new InvoicingReportRecordVM ///.Element("ArrayOfInvoicingReportRecord").Elements("InvoicingReportRecord").Select(x => new InvoicingReportRecordVM
                        {
                            AcOrWbs = x.Element("AcOrWbs")?.Value,
                            CostCenter = x.Element("CostCenter")?.Value,
                            ActivityType = x.Element("ActivityType")?.Value,
                            Volume = x.Element("Volume")?.Value,
                            PoName = x.Element("PoName")?.Value,
                            EbdNumber = x.Element("EbdNumber")?.Value,
                            InvoiceMasterId = x.Element("InvoiceMasterId")?.Value,
                        }).ToList();

                        //loop over the new records
                        foreach (var newRecord in listInvoiceReportDTO)
                        {
                            decimal newRecordVolume = 0;
                            decimal.TryParse(newRecord.Volume, out newRecordVolume);
                            InvoiceReportRecordDTO comparedAndCalculatedObj = new InvoiceReportRecordDTO();
                            //Check the new record list item in old record's xml item.                            
                            var existingRecords = xmlRecords.Where(x => x.InvoiceMasterId == newRecord.InvoiceMasterId).ToList();
                            //create object from new record
                            var comparedAndCalculatedObjNewRec = new InvoiceReportRecordDTO
                            {
                                Volume = newRecord.Volume,//"-" + newRecord.Volume,
                                AcOrWbs = newRecord.AcOrWbs,
                                ActivityType = newRecord.ActivityType,
                                CostCenter = newRecord.CostCenter,
                                EbdNumber = newRecord.EbdNumber,
                                InvoiceMasterId = newRecord.InvoiceMasterId,
                                PoName = newRecord.PoName,
                            };
                            foreach (var matchedItem in existingRecords)
                            {
                                //newerToOldReportInvoiceMasterIds.Add(matchedItem.InvoiceMasterId);
                                decimal oldRecordVolume = 0;
                                decimal.TryParse(matchedItem.Volume, out oldRecordVolume);
                                if (oldRecordVolume < 0)
                                { }
                                else
                                {
                                    if (!comparedAndCalculatedList.Any(x => x.InvoiceMasterId == newRecord.InvoiceMasterId && !string.IsNullOrEmpty(x.Volume)))
                                    {
                                        if (oldRecordVolume != newRecordVolume && oldRecordVolume > 0)
                                        {
                                            comparedAndCalculatedObj = new InvoiceReportRecordDTO
                                            {
                                                Volume = "-" + matchedItem.Volume,
                                                AcOrWbs = matchedItem.AcOrWbs,
                                                ActivityType = matchedItem.ActivityType,
                                                CostCenter = matchedItem.CostCenter,
                                                EbdNumber = matchedItem.EbdNumber,
                                                InvoiceMasterId = matchedItem.InvoiceMasterId,
                                                PoName = matchedItem.PoName,
                                            };
                                            if (!newerToOldReportInvoiceMasterIds.Any(x => x == newRecord.InvoiceMasterId))
                                            {
                                                comparedAndCalculatedList.Add(comparedAndCalculatedObj);
                                                comparedAndCalculatedList.Add(comparedAndCalculatedObjNewRec);
                                            }
                                        }
                                        else if (oldRecordVolume == newRecordVolume)
                                        {
                                            newerToOldReportInvoiceMasterIds.Add(matchedItem.InvoiceMasterId);
                                        }
                                    }
                                }
                            }
                        }

                        //invoice report count is greate than old report records i.e. Suppose user added a new po number which does not exists earlier
                        if (listInvoiceReportDTO.Count > xmlRecords.Count)
                        {
                            var ebdInvoiceReport = listInvoiceReportDTO.Select(x => x.EbdNumber).ToList();
                            var ebdSamemonthReport = xmlRecords.Select(x => x.EbdNumber).ToList();

                            //get the missing records from new report po number.
                            var missingNewRecord = ebdInvoiceReport.Except(ebdSamemonthReport).ToList();
                            //loop over the missing records
                            foreach (var item in missingNewRecord)
                            {
                                //if missing record is not exists in comparedAndCalculatedList then add into the list.
                                if (!comparedAndCalculatedList.Any(x => x.EbdNumber == item))
                                {
                                    var newRecordDetail = listInvoiceReportDTO.FirstOrDefault(x => x.EbdNumber == item);
                                    comparedAndCalculatedList.Add(new InvoiceReportRecordDTO
                                    {
                                        Volume = newRecordDetail.Volume,
                                        AcOrWbs = newRecordDetail.AcOrWbs,
                                        ActivityType = newRecordDetail.ActivityType,
                                        CostCenter = newRecordDetail.CostCenter,
                                        EbdNumber = newRecordDetail.EbdNumber,
                                        InvoiceMasterId = newRecordDetail.InvoiceMasterId,
                                        PoName = newRecordDetail.PoName,
                                    });
                                }
                            }
                        }

                    }

                    if (comparedAndCalculatedList.Count > 0)
                    {

                        finalXML = new XDocument(
                                new XDeclaration("1.0", "UTF-16", ""),
                                new XElement("ArrayOfInvoicingReportRecord",
                                from rec in comparedAndCalculatedList
                                select new XElement("InvoicingReportRecord",
                                             new XElement("AcOrWbs", rec.AcOrWbs), //(!string.IsNullOrEmpty(rec.AcOrWbs) ? rec.AcOrWbs : string.Empty)),
                                               new XElement("CostCenter", (!string.IsNullOrEmpty(rec.CostCenter) ? rec.CostCenter : string.Empty)),
                                               new XElement("ActivityType", (!string.IsNullOrEmpty(rec.ActivityType) ? rec.ActivityType : string.Empty)),
                                               new XElement("Volume", (!string.IsNullOrEmpty(rec.Volume) ? rec.Volume : string.Empty)),
                                               new XElement("PoName", rec.PoName),
                                               new XElement("EbdNumber", rec.EbdNumber),
                                               new XElement("InvoiceMasterId", rec.InvoiceMasterId)
                                           ))
                                   );

                    }
                }
                else
                {
                    //First time report
                    finalXML = new XDocument(
                               new XDeclaration("1.0", "UTF-16", ""),
                               new XElement("ArrayOfInvoicingReportRecord",
                               from rec in listInvoiceReportDTO
                               select new XElement("InvoicingReportRecord",
                                        new XElement("AcOrWbs", rec.AcOrWbs),
                                          new XElement("CostCenter", rec.CostCenter),
                                          new XElement("ActivityType", rec.ActivityType),
                                          new XElement("Volume", rec.Volume),
                                          new XElement("PoName", rec.PoName),
                                          new XElement("EbdNumber", rec.EbdNumber),
                                          new XElement("InvoiceMasterId", rec.InvoiceMasterId)
                                      ))
                               );
                }

                if (finalXML != null)
                {
                    var entityObj = new InvoicingReport
                    {
                        InvoicingReportID = Guid.NewGuid(),
                        TimeStamp = DateTime.Now,//selectedDate.HasValue ? selectedDate.Value : DateTime.Now,
                        Month = selectedDate.HasValue ? Convert.ToDateTime(selectedDate).Month : DateTime.Now.Month,
                        Year = selectedDate.HasValue ? Convert.ToDateTime(selectedDate).Year : DateTime.Now.Year,
                        Xml = finalXML.ToString(),
                        CreatedUser = currentUser
                    };

                    this.InvoiceReportServices.SaveInvoicingReport(entityObj);

                    return this.Json(new { status = true, message = "Report saved successfully." }, JsonRequestBehavior.AllowGet);
                }
                return this.Json(new { status = false, message = "No results found for report generation." }, JsonRequestBehavior.AllowGet);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return this.Json(new { status = false, message = "Something went wrong. Please try after some time." }, JsonRequestBehavior.AllowGet);
            }
        }

        [NHibernateMvcSessionContext]
        public JsonResult FillInvoiceReport()
        {
            var fillInvoiceReportObjList = this.InvoiceReportServices.GetInvoicingReports()
                                    .OrderByDescending(x => x.TimeStamp)
                                    .Select(x => new { InvoiceReportID = x.InvoicingReportID, Year = x.Year, TimeStamp = x.TimeStamp.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture), Month = x.Month, InvoiceReportId = x.InvoicingReportID, CreatedBy = x.CreatedUser?.Name })
                                    .ToList();
            return this.Json(new { FillInvoiceReportObjList = fillInvoiceReportObjList }, JsonRequestBehavior.AllowGet);
        }

        [NHibernateMvcSessionContext]
        public ActionResult GetPurchaseOrderLineRecipents(string ebdNumber)
        {
            var result = this.POLineServices.GetPoLinebyEbdNumber(ebdNumber);

            var requestorEmail = result.RequestorEmail.Select(x => x.RequestorEmailId).Distinct().ToList();
            var resq = new List<string>();
            foreach (var item in requestorEmail)
            {
                var emails = item.Split(',');
                foreach (var item2 in emails)
                {
                    resq.Add(item2);
                }
            }
            requestorEmail = resq;
            var ContactPerson = result.ContactPersonEmail.Select(x => x.ContactPersonEmailId).Distinct().ToList();
            List<Requestor> req = new List<Requestor>();
            List<ContactPerson> contPrson = new List<ContactPerson>();
            var poLineEmailRecipent = new PoLineEmailRecipent();
            foreach (var requEmail in requestorEmail)
            {
                var requsterInner = new Requestor();
                requsterInner.PoLineId = new List<int>();

                foreach (var item in result.RequestorEmail)
                {
                    var spt = item.RequestorEmailId.Split(',');
                    foreach (var spEmail in spt)
                    {
                        if (spEmail == requEmail)
                        {
                            requsterInner.RequestorEmail = spEmail;
                            requsterInner.PoLineId.Add(item.PoLineId);
                        }
                    }
                }
                req.Add(requsterInner);
            }


            foreach (var contactEmail in ContactPerson)
            {
                var contactEmailInner = new ContactPerson();
                contactEmailInner.PoLineId = new List<int>();
                foreach (var item in result.ContactPersonEmail)
                {
                    if (item.ContactPersonEmailId != null)
                    {
                        var test = req.Where(x => x.RequestorEmail == item.ContactPersonEmailId.ToString());
                        if (test.Count() > 0)
                        {
                            continue;
                        }
                        else
                    if (item.ContactPersonEmailId == contactEmail)
                        {

                            {
                                contactEmailInner.ContactPersonEmail = item.ContactPersonEmailId;
                                contactEmailInner.PoLineId.Add(item.PoLineId);
                            }

                        }
                    }

                }
                contPrson.Add(contactEmailInner);
            }

            poLineEmailRecipent.EbdNumber = ebdNumber;
            poLineEmailRecipent.RequestorEmails = new List<Requestor>();
            poLineEmailRecipent.RequestorEmails.AddRange(req);
            poLineEmailRecipent.ContactPersonEmails = new List<ContactPerson>();
            poLineEmailRecipent.ContactPersonEmails.AddRange(contPrson);

            string results = this.RenderPartialToString("~/views/poline/_EmailRecipents.cshtml", poLineEmailRecipent);
            return this.Content(results);

        }


        private string CalculatePayment(string ebdNumber, string poline)
        {
            var result = this.PurchaseOrderService.GetPurchaseOrderDetail(ebdNumber);

            var polineId = poline.Split(',');
            var ids = new List<int>();
            foreach (var item in polineId)
            {
                ids.Add(int.Parse(item));
            }
            result = result.Where(c => ids.Contains(c.PoLine)).ToList();

            StringBuilder sb = new StringBuilder();
            if (result.Count() > 0)
            {
                decimal totalOrderAmunt = 0;
                decimal totalSeekAmount = 0;
                decimal splitLineItemAmount = 0;
                string currency = string.Empty;
                result.ForEach(a =>
                {
                    //var year = Convert.ToDateTime(a.StartDate);
                    //var seekRate = this.POLineServices.GetPOlineSeekAmount(a.Currency, year.Year);

                    //decimal exchangeRateCurrencyRate = 0;

                    //if (a.ExchangeRateYear.HasValue)
                    //{
                    //    exchangeRateCurrencyRate = this.POLineServices.GetPOlineSeekAmount(a.Currency, a.ExchangeRateYear.Value);
                    //}
                    //a.SekRate = seekRate;
                    //a.ExchangeRateCurrencyRate = exchangeRateCurrencyRate;
                    //a.LastChangeByName = string.Empty;
                    //totalOrderAmunt = totalOrderAmunt + Convert.ToDecimal(a.OrderAmount);
                    //totalSeekAmount = totalSeekAmount + (Convert.ToDecimal(a.OrderAmount) * seekRate);
                    //splitLineItemAmount = splitLineItemAmount + Convert.ToDecimal(a.SplitLineItemAmount);
                    //currency = a.Currency;

                });
                var assignmentCodeResult = result.GroupBy(x => x.AcOrWbs).ToList();
                foreach (var assignmentCode in assignmentCodeResult)
                {

                    //    sb.Append("Assignment Code : " + assignmentCode.FirstOrDefault().AcOrWbs + "<br/>");
                    //    sb.Append("WBS : " + assignmentCode.FirstOrDefault().WBS + "<br/>");
                    //    sb.Append("Total amount recharged for this WBS is (" + assignmentCode.FirstOrDefault().Currency + ")" + string.Format("{0:n}", result.Sum(x => x.SplitLineItemAmount)) + "(+2% mark-up and overhead costs)<br/>");
                    //    var costList = new List<CostListViewModel>();
                    //    var polineCostList = new List<PolineCostList>();
                    //    foreach (var item in assignmentCode)
                    //    {
                    //        var polineobj = new PolineCostList();
                    //        //costList.AddRange(this.GetCostListData(item.PurchaseOrderLineId.ToString()).Item1);
                    //        // polineobj.CostLists.AddRange(this.GetCostListData(item.PurchaseOrderLineId.ToString()).Item1);
                    //        polineCostList.Add(polineobj);
                    //    }
                    //    var earliestStartDate = Convert.ToDateTime(costList.Min(x => x.DateAndYear));
                    //    var earliestEndDate = Convert.ToDateTime(costList.Max(x => x.DateAndYear));



                    //    var totalSUm = costList.GroupBy(c => c.DateAndYear)
                    //        .Select(g => new Cost()
                    //        {
                    //            Total = g.Sum(s => s.Cost),
                    //            Date = g.Key
                    //        }).ToList();

                    //    var listFinal = new List<Cost>();
                    //    foreach (var cost in totalSUm)
                    //    {
                    //        listFinal.Add(cost);
                    //    }
                    //    var lst = listFinal.GroupBy(g => g.Total);

                    //    foreach (var amount in lst)
                    //    {
                    //        var earliestDate = amount.Min(x => x.Date);
                    //        var enddate = amount.Max(x => x.Date);
                    //        var total = amount.FirstOrDefault().Total;
                    //        string toDates = earliestDate;
                    //        if (earliestDate != enddate)
                    //        {
                    //            toDates += " to " + enddate;
                    //        }
                    //        else
                    //        {
                    //            toDates += ":";
                    //        }
                    //        sb.Append("Monthly charge for " + toDates + " " + string.Format("{0:n}", total) + " " + currency + " (+2% mark-up and overhead costs) <br/>");
                    //    }
                    //    sb.Append("<br/>");
                }
                sb.Append("<br/>");
                return sb.ToString();
            }
            return string.Empty;
        }

        public class Cost
        {
            public decimal Total { get; set; }
            public string Date { get; set; }
        }

        [NHibernateMvcSessionContext]
        public ActionResult SendEmail(List<RecipentEmail> recipents, string ebdNumber)
        {
            try
            {
                var currentUser = this.UserService.GetCurrent();
                foreach (var item in recipents)
                {
                    var polineTempalte = this.CalculatePayment(ebdNumber, item.PolineIds);
                    var polineId = item.PolineIds.Split(',');
                    var ids = new List<int>();
                    foreach (var item2 in polineId)
                    {
                        ids.Add(int.Parse(item2));
                    }
                    var polines = new List<CustomModel>();
                    var emailTemplate = new EmailTemplate();
                    var details = this.PurchaseOrderService.GetPurchaseOrderDetail(ebdNumber);
                    details = details.Where(c => ids.Contains(c.PoLine)).ToList();
                    //  emailTemplate.AllOrderAmount = Math.Round(Convert.ToDecimal(details.Sum(x => Convert.ToDecimal(x.OrderAmount))), 2);
                    emailTemplate.EmailTemplateString = polineTempalte;
                    // emailTemplate.CustomViewModel = details.FirstOrDefault();
                    emailTemplate.Name = currentUser.Name;
                    emailTemplate.userName = currentUser.Username;
                    var templateForReq = this.RenderPartialToString("~/views/poline/_EmailTemplate.cshtml", emailTemplate);
                    SendEmailToMany(item.EmailRecipent, templateForReq);

                }

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return View("AccessDenied");

            }
            return Json("");
        }

        private void SendEmailToMany(string address, string templateBoday)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient(ConfigurationManager.AppSettings["SmtpClient"]);

            mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]);
            mail.To.Add(address);
            mail.Subject = ConfigurationManager.AppSettings["Subject"];
            mail.Body = templateBoday;
            mail.IsBodyHtml = true;
            smtpServer.Send(mail);

        }


        public string RenderPartialToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
        [NHibernateMvcSessionContext]
        public JsonResult DeleteInvoiceReport(List<InvoicingReportDeleteViewModel> invoceReports)
        {
            if (invoceReports != null && invoceReports.Any())
            {
                var isDeleted = false;
                foreach (var invoiceReportId in invoceReports)
                {
                    if (invoiceReportId.IsDeleted)
                    {
                        isDeleted = this.InvoiceReportServices.DeleteInvoicingReportById(invoiceReportId.InvoicingReportID);
                    }
                }
                return this.Json(new { IsDeleted = isDeleted }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { IsDeleted = false }, JsonRequestBehavior.AllowGet);
        }
        public class InvoicingReportDeleteViewModel
        {
            public string InvoicingReportID { get; set; }
            public bool IsDeleted { get; set; }
        }

        [NHibernateMvcSessionContext]
        public List<InvoicingReportRecordVM> ParseXMLAndDownload(string invoiceReportId)
        {
            try
            {
                if (!string.IsNullOrEmpty(invoiceReportId))
                {
                    var invoiceReportObjXML = this.InvoiceReportServices.GetInvoicingReports().FirstOrDefault(x => x.InvoicingReportID.ToString() == invoiceReportId)?.Xml;
                    XDocument xmlDoc;
                    xmlDoc = XDocument.Parse(invoiceReportObjXML);
                    var xmlRecords = xmlDoc.Element("ArrayOfInvoicingReportRecord").Elements("InvoicingReportRecord").Select(x => new InvoicingReportRecordVM
                    {
                        AcOrWbs = x.Element("AcOrWbs")?.Value,
                        CostCenter = x.Element("CostCenter")?.Value,
                        ActivityType = x.Element("ActivityType")?.Value,
                        Volume = x.Element("Volume")?.Value,
                        PoName = x.Element("PoName")?.Value,
                        EbdNumber = x.Element("EbdNumber")?.Value,
                        InvoiceMasterId = x.Element("InvoiceMasterId")?.Value,
                    }).ToList();

                    return xmlRecords;
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return new List<InvoicingReportRecordVM>();
            }

            return new List<InvoicingReportRecordVM>();
        }
        [NHibernateMvcSessionContext]
        public FileContentResult DownloadExcel(string invoiceReportId)
        {
            try
            {
                var xmlDatalist = this.ParseXMLAndDownload(invoiceReportId);

                byte[] filecontent = ExportToCSV(xmlDatalist); // this.ExportToExcel(xmlDatalist);
                return File(filecontent, "application/CSV", "InvoiceReport-" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".csv");
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return File(new byte[] { }, "application/CSV", "No File-" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".csv");
            }
        }

        private byte[] ExportToExcel(List<InvoicingReportRecordVM> list)
        {
            try
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                    workSheet.TabColor = System.Drawing.Color.Black;
                    workSheet.DefaultRowHeight = 12;

                    workSheet.Row(1).Height = 20;
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(1).Style.Font.Bold = true;
                    workSheet.Cells[1, 1].Value = "AcOrWbs";
                    workSheet.Cells[1, 2].Value = "CostCenter";
                    workSheet.Cells[1, 3].Value = "ActivityType";
                    workSheet.Cells[1, 4].Value = "Volume";
                    workSheet.Cells[1, 5].Value = "PoName";
                    workSheet.Cells[1, 6].Value = "EbdNumber";
                    workSheet.Cells[1, 7].Value = "InvoiceMasterId";

                    int recordIndex = 2;
                    foreach (var rec in list)
                    {
                        workSheet.Cells[recordIndex, 1].Value = rec.AcOrWbs;
                        workSheet.Cells[recordIndex, 2].Value = rec.CostCenter;
                        workSheet.Cells[recordIndex, 3].Value = rec.ActivityType;
                        workSheet.Cells[recordIndex, 4].Value = rec.Volume;
                        workSheet.Cells[recordIndex, 5].Value = rec.PoName;
                        workSheet.Cells[recordIndex, 6].Value = rec.EbdNumber;
                        workSheet.Cells[recordIndex, 7].Value = rec.InvoiceMasterId;
                        recordIndex++;
                    }
                    workSheet.Column(1).AutoFit();
                    workSheet.Column(2).AutoFit();
                    workSheet.Column(3).AutoFit();
                    workSheet.Column(4).AutoFit();
                    workSheet.Column(5).AutoFit();
                    workSheet.Column(6).AutoFit();
                    workSheet.Column(7).AutoFit();

                    return excel.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                return new byte[] { };
            }
        }

        private byte[] ExportToCSV(List<InvoicingReportRecordVM> list)
        {
            var employeeCSV = new StringBuilder();
            list.ForEach(line =>
            {
                employeeCSV.AppendLine(";" + line.AcOrWbs + ";" + line.CostCenter + ";" + line.ActivityType + ";" + line.Volume + ";" + "\"" + line.PoName.Replace(",", "").Trim() + "\"" + ";" + line.EbdNumber);
            });

            byte[] buffer = Encoding.ASCII.GetBytes($"{employeeCSV.ToString()}");
            return buffer;
        }

        public class InvoicingReportRecordVM
        {
            public string AcOrWbs { get; set; }
            public string CostCenter { get; set; }
            public string ActivityType { get; set; }
            public string Volume { get; set; }
            public string PoName { get; set; }
            public string EbdNumber { get; set; }
            public string InvoiceMasterId { get; set; }
        }

        [NHibernateMvcSessionContext]
        public void CostListOneTimeJob()
        {
            List<List<CostList>> listOfListCostList = new List<List<CostList>>();
            //var allPoLineIds = this.POLineServices.FindPOLineAsQueryable().Distinct().Select(x => new { x.EbdNumber, x.PurchaseOrderLineId });
            ////var allPoLineIds = this.POLineServices.GetAllPurchaseOrders().Distinct().Select(x => new { x.PoNumber });
            var allPoLineIds = this.POLineServices.FindPOLineAsQueryable(false);
            var allpo = from t in allPoLineIds
                            // where t.MonthlyRate == 0
                            // && t.StartDate != null
                            // && t.EndDate != null
                        select new { t.EbdNumber, t.PurchaseOrderLineId };
            foreach (var poLine in allpo)
            {
                var assignment = this.PurchaseOrderService.GetAssignmentCode(poLine.PurchaseOrderLineId.ToString());

                if (assignment.MonthlyRate == 0 && assignment.StartDate != null)
                {
                    var tupleResult = GetCostListData(poLine.PurchaseOrderLineId.ToString(), true);
                }
            }
        }
    
        //[NHibernateMvcSessionContext]
        //public ActionResult ImportData(FormCollection formCollection)
        //{
        //    var polinerec = new List<POLine>();
        //    var searchModel = new SearchModel();
        //    var currentUser = this.UserService.GetCurrent();
        //    if (this.Request != null)
        //    {
        //        var assignmentCode = this.POLineServices.FindAllWBSORAssignmentCode();
        //        searchModel.AssignmentCodes = assignmentCode.Select(x => new DDLModel() { text = x, value = x }).ToList();
        //        searchModel.WBS = assignmentCode.Select(x => new DDLModel() { text = x, value = x }).ToList();
        //        searchModel.ContractTypes = this.POLineServices.GetAllContractTypes();
        //        searchModel.Applications = this.POLineServices.GetAllApplications();
        //        searchModel.Requestores = this.POLineServices.FindAllRequesterName().Where(x => !string.IsNullOrEmpty(x)).Distinct().Select(x => new DDLModel() { text = x, value = x });

        //        HttpPostedFileBase file = this.Request.Files["UploadedFile"];
        //        string fileExtention = Path.GetExtension(file.FileName);
        //        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName) && (fileExtention == ".xlsx" || fileExtention == "xls"))
        //        {
        //            string fileName = file.FileName;
        //            string fileContentType = file.ContentType;
        //            byte[] fileBytes = new byte[file.ContentLength];
        //            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
        //            using (var package = new ExcelPackage(file.InputStream))
        //            {
        //                var currentSheet = package.Workbook.Worksheets;
        //                var workSheet = currentSheet.First();
        //                var noOfCol = workSheet.Dimension.End.Column;
        //                var noOfRow = workSheet.Dimension.End.Row;
        //                var purchaseOrderLineED = new PurchaseOrderLineFromEbd();
        //                var purchaseOrder = new PurchaseOrder();
        //                /// var purchaseOrderLine = new POLine();

        //                var statusPo = new List<StatusPo>();
        //                var costCenter = new List<CostCenter>();
        //                var owner = this.POLineServices.GetAllOwners();
        //                var curenncy = new List<Currency>();
        //                statusPo.AddRange(this.POLineServices.GetAllStatus());
        //                costCenter.AddRange(this.POLineServices.GetAllCostCenter());
        //                curenncy.AddRange(this.POLineServices.GetAllCurrency());

        //                List<POLine> poLines = new List<POLine>();

        //                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
        //                {
        //                    try
        //                    {



        //                        var purchaseOrderFromDb = this.PurchaseOrderService.GetPurchaseOrderByEBDNumber(Convert.ToString(workSheet.Cells[rowIterator, 2].Value.ToString()));
        //                        var purchaseOrderLineEBD = new PurchaseOrderLineFromEbd();
        //                        if (purchaseOrderFromDb.PurchaseOrderId != Guid.Empty)
        //                        {
        //                            purchaseOrderLineEBD = this.PurchaseOrderService.GetPurchaseOrderLineEBD(purchaseOrderFromDb.PurchaseOrderId);
        //                        }

        //                        int i = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value.ToString());
        //                        var purchaseOrderLine = this.POLineServices.GetPolineByEbdNumberPoline(Convert.ToString(workSheet.Cells[rowIterator, 2].Value.ToString()), i);
        //                        if (purchaseOrderLine == null && purchaseOrderFromDb.PurchaseOrderId == Guid.Empty)
        //                        {
        //                            purchaseOrderLine = new POLine();
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd = new PurchaseOrderLineFromEbd();
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder = new PurchaseOrder();
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrderLineFromEbdId = Guid.NewGuid();
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.PurchaseOrderId = Guid.NewGuid();
        //                            purchaseOrderLine.PurchaseOrderLineId = Guid.NewGuid();
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd_ID = purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrderLineFromEbdId;
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.Owner = new Owner();
        //                            var ownerlist = owner.FirstOrDefault(c => c.Name == currentUser.Name);
        //                            if (ownerlist != null)
        //                            {
        //                                purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name = ownerlist.Name;
        //                                purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.OwnerId = ownerlist.OwnerId;

        //                                purchaseOrderLine.OwnerId = ownerlist.OwnerId.ToString();
        //                            }
        //                        }

        //                        if (purchaseOrderLine == null && purchaseOrderFromDb.PurchaseOrderId != Guid.Empty)
        //                        {
        //                            purchaseOrderLine = new POLine();
        //                            purchaseOrderLine.StartDate = null;
        //                            purchaseOrderLine.EndDate = null;
        //                            purchaseOrderLine.ApprovedBy = null;
        //                            purchaseOrderLine.ApprovedDate = null;
        //                            purchaseOrderLine.ContactPerson = null;
        //                            purchaseOrderLine.DelayedDate = null;
        //                            purchaseOrderLine.EarlierPaymentDate = null;
        //                            purchaseOrderLine.EbdNumber = null;
        //                            purchaseOrderLine.PoLine = 0;
        //                            purchaseOrderLine.ProductNumber = null;
        //                            purchaseOrderLine.Remark = null;
        //                            purchaseOrderLine.RenewalOrderPurchaseLine = null;
        //                            purchaseOrderLine.RequestorName = null;
        //                            purchaseOrderLine.Software = null;
        //                            purchaseOrderLine.SplitLineItemAmount = null;
        //                            purchaseOrderLine.PurchaseOrderLineId = Guid.NewGuid();
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd = purchaseOrderLineEBD;

        //                        }

        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.PoNumber = Convert.ToString(workSheet.Cells[rowIterator, 2].Value.ToString());
        //                        purchaseOrderLine.EbdNumber = Convert.ToString(workSheet.Cells[rowIterator, 2].Value.ToString());
        //                        purchaseOrderLineED.PoLine = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);
        //                        purchaseOrderLine.PoLine = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);

        //                        purchaseOrderLine.StatusPo = new StatusPo();
        //                        var status = statusPo.FirstOrDefault(x => x.Name == Convert.ToString(workSheet.Cells[rowIterator, 4].Value));
        //                        if (status != null)
        //                        {
        //                           purchaseOrderLine.StatusPo.Name = status.Name;
        //                           purchaseOrderLine.StatusPo.StatusPoId = status.StatusPoId;
        //                            purchaseOrderLine.StatusPoID= status.StatusPoId.ToString();
        //                        }
        //                        else
        //                        {
        //                            purchaseOrderLine.StatusPo = new StatusPo();
        //                            purchaseOrderLine.StatusPo.StatusPoId = Guid.NewGuid();
        //                            purchaseOrderLine.StatusPo.Name = Convert.ToString(workSheet.Cells[rowIterator, 4].Value);
        //                            purchaseOrderLine.StatusPo.TimeStamp = DateTime.Now;

        //                          var statusr=  this.POLineServices.InsertStatusPo(purchaseOrderLine.StatusPo);
        //                            purchaseOrderLine.StatusPo = new StatusPo();
        //                            purchaseOrderLine.StatusPo = statusr;
        //                            purchaseOrderLine.StatusPoID = purchaseOrderLine.StatusPo.StatusPoId.ToString();
        //                        }

        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.LineItemDescription = new string(Convert.ToString(workSheet.Cells[rowIterator, 5].Value).Take(199).ToArray());                                 
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.PurchaseOrderName = new string(Convert.ToString(workSheet.Cells[rowIterator, 6].Value).Take(199).ToArray());
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.OrderAmount = Convert.ToDecimal(workSheet.Cells[rowIterator, 7].Value);

        //                        purchaseOrderLine.Currency = new Currency();
        //                        var curey = curenncy.FirstOrDefault(x => x.Name == Convert.ToString(workSheet.Cells[rowIterator, 8].Value));
        //                        if (curey != null)
        //                        {
        //                            purchaseOrderLine.Currency.Name = curey.Name;
        //                            purchaseOrderLine.Currency.CurrencyID = curey.CurrencyID;
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.Currency = curey.Name;

        //                        }
        //                        var blanketOrder = Convert.ToString(workSheet.Cells[rowIterator, 9].Value);

        //                        try
        //                        {
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd.CreationDate = Convert.ToDateTime(DateTime.FromOADate(double.Parse(workSheet.Cells[rowIterator, 10].Value.ToString()))); //creationDate;

        //                        }
        //                        catch (Exception ee)
        //                        {
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd.CreationDate = DateTime.Now;
        //                        }

        //                        try
        //                        {

        //                            purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.OrderDate = Convert.ToDateTime(DateTime.FromOADate(double.Parse(workSheet.Cells[rowIterator, 11].Value.ToString()))); //creationDate;
        //                        }
        //                        catch (Exception ee)
        //                        {
        //                            purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.OrderDate = DateTime.Now;
        //                        }

        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.SpendType = Convert.ToString(workSheet.Cells[rowIterator, 12].Value.ToString());

        //                        purchaseOrderLine.CostCenter = new CostCenter();
        //                        var cost = costCenter.FirstOrDefault(c => c.FullName == Convert.ToString(workSheet.Cells[rowIterator, 13].Value.ToString()));
        //                        if (cost != null)
        //                        {
        //                            purchaseOrderLine.CostCenter.Name = cost.Name;
        //                            purchaseOrderLine.CostCenter.CostCenterId = cost.CostCenterId;
        //                            purchaseOrderLine.CostCenterId = cost.CostCenterId.ToString();
        //                        }

        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.CompanyId = Convert.ToString(workSheet.Cells[rowIterator, 15].Value);
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.ShortDescription = Convert.ToString(workSheet.Cells[rowIterator, 16].Value);
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.GeographicalSite = Convert.ToString(workSheet.Cells[rowIterator, 17].Value);
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.LowestBorg = new string(Convert.ToString(workSheet.Cells[rowIterator, 18].Value).Take(199).ToArray());
        //                        purchaseOrderLine.RequestorName = Convert.ToString(workSheet.Cells[rowIterator, 19].Value.ToString());
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.RequesterEmail = new string(Convert.ToString(workSheet.Cells[rowIterator, 20].Value).Take(199).ToArray()); 
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaserName = new string(Convert.ToString(workSheet.Cells[rowIterator, 21].Value).Take(199).ToArray()); 
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.FunctionalApproverName = new string(Convert.ToString(workSheet.Cells[rowIterator, 22].Value).Take(199).ToArray());
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.VendorName = new string(Convert.ToString(workSheet.Cells[rowIterator, 23].Value).Take(199).ToArray());
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.ParmaNbr = Convert.ToString(workSheet.Cells[rowIterator, 24].Value);
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.OrderedQuantity = Convert.ToDecimal(workSheet.Cells[rowIterator, 25].Value);
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.ReceivedQuantity = Convert.ToDecimal(workSheet.Cells[rowIterator, 26].Value);

        //                        try
        //                        {
        //                            if (Convert.ToDateTime(DateTime.FromOADate(double.Parse(workSheet.Cells[rowIterator, 27].Value.ToString()))) == DateTime.MinValue)
        //                            {
        //                                purchaseOrderLine.PurchaseOrderLineFromEbd.ContractStartDate = DateTime.Now;
        //                            }
        //                            else
        //                            {
        //                                purchaseOrderLine.PurchaseOrderLineFromEbd.ContractStartDate = Convert.ToDateTime(DateTime.FromOADate(double.Parse(workSheet.Cells[rowIterator, 27].Value.ToString()))); //creationDate;
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {


        //                        }
        //                        try
        //                        {
        //                            if (Convert.ToDateTime(DateTime.FromOADate(double.Parse(workSheet.Cells[rowIterator, 28].Value.ToString()))) == DateTime.MinValue)
        //                            {
        //                                purchaseOrderLine.PurchaseOrderLineFromEbd.ContractEndDate = DateTime.Now;
        //                            }
        //                            else
        //                            {
        //                                purchaseOrderLine.PurchaseOrderLineFromEbd.ContractEndDate = Convert.ToDateTime(DateTime.FromOADate(double.Parse(workSheet.Cells[rowIterator, 28].Value.ToString()))); //creationDate;
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {


        //                        } 
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.LicenseType = new string(Convert.ToString(workSheet.Cells[rowIterator, 29].Value).Take(199).ToArray());
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.SoftwareName = new string(Convert.ToString(workSheet.Cells[rowIterator, 30].Value).Take(199).ToArray());
        //                        purchaseOrderLine.StartDate = purchaseOrderLine.PurchaseOrderLineFromEbd.ContractStartDate;
        //                        purchaseOrderLine.EndDate = purchaseOrderLine.PurchaseOrderLineFromEbd.ContractEndDate;
        //                        if(purchaseOrderLine.SplitLineItemAmount == null)
        //                        purchaseOrderLine.SplitLineItemAmount = purchaseOrderLine.PurchaseOrderLineFromEbd.OrderAmount;
        //                        purchaseOrderLine.PoLine = i;
        //                        purchaseOrderLine.PurchaserName = purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.PurchaseOrderName;
        //                        purchaseOrderLine.EbdNumber = purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.PoNumber;
        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.PurchaseOrder.TimeStamp = DateTime.Now;
        //                        purchaseOrderLine.Software = purchaseOrderLine.PurchaseOrderLineFromEbd.SoftwareName;

        //                        purchaseOrderLine.PurchaseOrderLineFromEbd.TimeStamp = DateTime.Now;

        //                        purchaseOrderLine.TimeStamp = DateTime.Now;
        //                        purchaseOrderLine.LastChangeDate = DateTime.Now;

        //                        purchaseOrderLine.ContractType = new ContractType();
        //                        var contracttype = this.POLineServices.GetAllContractTypes().FirstOrDefault();
        //                        if (contracttype != null)
        //                        {
        //                            purchaseOrderLine.ContractType.Name = contracttype.Name;
        //                            purchaseOrderLine.ContractType.ContractTypeId = contracttype.ContractTypeId;
        //                            purchaseOrderLine.ContractTypeId= contracttype.ContractTypeId.ToString();
        //                        }

        //                        purchaseOrderLine.App = new App();
        //                        var app = this.POLineServices.GetAllApplications().FirstOrDefault();
        //                        if (app != null)
        //                        {
        //                            purchaseOrderLine.App.Name = app.Name;
        //                            purchaseOrderLine.App.AppId = app.AppId;
        //                            purchaseOrderLine.AppId = app.AppId.ToString();
        //                        }

        //                        purchaseOrderLine.ActivityType = new ActivityType();
        //                        var acc = this.POLineServices.GetAllActivityTypes().FirstOrDefault();
        //                        if (acc != null)
        //                        {
        //                            purchaseOrderLine.ActivityType.Name = acc.Name;
        //                            purchaseOrderLine.ActivityType.ActivityTypeId = acc.ActivityTypeId;
        //                            purchaseOrderLine.ActivityTypeId = acc.ActivityTypeId.ToString();
        //                        }

        //                        purchaseOrderLine.CostType = new CostType();
        //                        var cos = this.POLineServices.GellAllCostTypes().FirstOrDefault();
        //                        if (cos != null)
        //                        {
        //                            purchaseOrderLine.CostType.Name = cos.Name;
        //                            purchaseOrderLine.CostType.CostTypeId = cos.CostTypeId;
        //                            purchaseOrderLine.CostTypeId = cos.CostTypeId.ToString();
        //                        }

        //                        purchaseOrderLine.Product = new Product();
        //                        var pr = this.POLineServices.GellAllProducts().FirstOrDefault();
        //                        if (pr != null)
        //                        {
        //                            purchaseOrderLine.Product.Name = pr.Name;
        //                            purchaseOrderLine.Product.ProductId = pr.ProductId;
        //                        }
        //                        purchaseOrderLine.LastChangeBy = currentUser.UserID;
        //                        poLines.Add(purchaseOrderLine);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        var st = new StackTrace(ex, true);
        //                        // Get the top stack frame
        //                        var frame = st.GetFrame(0);
        //                        // Get the line number from the stack frame
        //                        var line = frame.GetFileLineNumber();


        //                    }

        //                }

        //                this.POLineServices.InserBulk(poLines);

        //            }

        //        }

        //    }
        //    this.ViewBag.ExcelImported = true;
        //    return this.View("reports", searchModel);
        //}
    }
}