using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Unity;
using Volvo.NVS.Utilities.Web.Localization;
using Volvo.LAT.MVCWebUIComponent.Common.Helpers;

namespace Volvo.LAT.MVCWebUIComponent.Controllers
{
    /// <summary>
    /// The error controller responsible for application error reporting.
    /// </summary>
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// </summary>
        [InjectionConstructor]
        public ErrorController()
            : this(
                  Container.Resolve<ILocalizationHelper>(),
                  Container.Resolve<IThemesHelper>(),
                  Container.Resolve<IBundlingHelper>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// </summary>
        /// <param name="localizationHelper">A localization helper to be used by the model.</param>
        /// <param name="themesHelper">A themes helper to be used by the model.</param>
        /// <param name="bundlingHelper">A bundling helper to be used by the model.</param>
        public ErrorController(
            ILocalizationHelper localizationHelper,
            IThemesHelper themesHelper,
            IBundlingHelper bundlingHelper)
            : base(localizationHelper, themesHelper, bundlingHelper)
        {
        }

        /// <summary>
        /// The generic error action, view.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult Error() => View();

        /// <summary>
        /// The not authorized action, view.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult NotAuthorized() => View();

        /// <summary>
        /// The error important resource not available action, view.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult ErrorImportantResource() => View();
    }
}