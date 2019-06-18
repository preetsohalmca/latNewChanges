using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Volvo.NVS.Core.Unity;
using Volvo.NVS.Security.Exceptions;
using Volvo.NVS.Utilities.Web.Localization;
using Volvo.NVS.Utilities.Web.Messaging;
using Volvo.LAT.MVCWebUIComponent.Common.Helpers;
using UtilityResource = Volvo.LAT.MVCWebUIComponent.App_LocalResources.UtilityResource;

namespace Volvo.LAT.MVCWebUIComponent.Controllers
{
    /// <summary>
    /// The utility controller providing application configuration and giving access to sample views.
    /// </summary>
    public class UtilityController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilityController"/> class.
        /// </summary>
        public UtilityController()
            : this(
                  Container.Resolve<ILocalizationHelper>(),
                  Container.Resolve<IThemesHelper>(),
                  Container.Resolve<IBundlingHelper>(),
                  null) // flash messenger
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UtilityController"/> class.
        /// </summary>
        /// <param name="localizationHelper">The localization helper.</param>
        /// <param name="themesHelper">The themes helper.</param>
        /// <param name="bundlingHelper">The bundling helper.</param>
        /// <param name="flashMessenger">The flash messanger.</param>
        public UtilityController(
            ILocalizationHelper localizationHelper,
            IThemesHelper themesHelper,
            IBundlingHelper bundlingHelper,
            IFlashMessenger flashMessenger)
            : base(localizationHelper, themesHelper, bundlingHelper)
        {
            FlashMessenger = flashMessenger ?? Messenger;
        }

        /// <summary>
        /// Gets or sets the current flash messenger.
        /// </summary>
        private IFlashMessenger FlashMessenger { get; set; }

        /// <summary>
        /// Goes into the non-existing page.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult GoToNonexistentPage() => Redirect($"~/{Guid.NewGuid()}");

        /// <summary>
        /// Creates a test exception.
        /// </summary>
        /// <returns>Action result.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public ActionResult RaiseException()
        {
            throw new Exception("Raised an Exception");
        }

        /// <summary>
        /// Creates a test authorization exception.
        /// </summary>
        /// <returns>Action result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public ActionResult RaiseNotAuthorizedException()
        {
            throw new NotAuthorizedException();
        }

        /// <summary>
        /// Changes the application theme.
        /// </summary>
        /// <param name="id">The theme id.</param>
        /// <returns>Action result.</returns>
        public ActionResult SetTheme(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ThemesHelper.SetCurrentTheme(ControllerContext.HttpContext, id);
                FlashMessenger.AppendMessage(string.Format(UtilityResource.Utilities_SetTheme_BrandHasBeenChanged, id));
            }

            return CreateResultFromUrlReferrer();
        }

        /// <summary>
        /// Change the application language.
        /// </summary>
        /// <param name="id">A culture identifier.</param>
        /// <returns>Action result.</returns>
        public ActionResult SetLocalization(string id)
        {
            LocalizationHelper.SetNewCulture(ControllerContext.HttpContext, id);
            FlashMessenger.AppendMessage(string.Format(UtilityResource.Utilities_SetLocalization_LocalizationHasBeenChanged, id));
            return CreateResultFromUrlReferrer();
        }

        /// <summary>
        /// Runs the css overview page.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult CssOverview() => View();

        /// <summary>
        /// Changes the bundling and minification option.
        /// </summary>
        /// <param name="enable">True in order to enable the option.</param>
        /// <returns>Action result.</returns>
        public ActionResult EnableBundlingAndMinification(bool enable)
        {
            BundlingHelper.EnableBundlingAndMinification(ControllerContext.HttpContext, enable);
            FlashMessenger.AppendMessage(enable ? UtilityResource.Utilities_EnableBundlingAndMinification_Enabled : UtilityResource.Utilities_EnableBundlingAndMinification_Disabled);
            return CreateResultFromUrlReferrer();
        }

        private ActionResult CreateResultFromUrlReferrer() =>
            ControllerContext.HttpContext.Request.UrlReferrer == null
                ? (ActionResult)RedirectToAction("Index", "Home")
                : Redirect(ControllerContext.HttpContext.Request.UrlReferrer.AbsoluteUri);
    }
}
