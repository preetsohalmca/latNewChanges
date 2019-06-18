using System;
using System.Web.Mvc;
using Volvo.NVS.Utilities.Web.Controllers;
using Volvo.NVS.Utilities.Web.Localization;
using Volvo.LAT.MVCWebUIComponent.Common.Helpers;
using Volvo.LAT.MVCWebUIComponent.Models.Shared;
using Volvo.LAT.MVCWebUIComponent.Common.Extensions;

namespace Volvo.LAT.MVCWebUIComponent.Controllers
{
    /// <summary>
    /// Acts like a base controller for all application MVC controllers. Provides common and shared functionality.
    /// </summary>
    public abstract class BaseController : NVSController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="localizationHelper">A localization helper to be used by the model.</param>
        /// <param name="themesHelper">A themes helper to be used by the model.</param>
        /// <param name="bundlingHelper">A bundling helper to be used by the model.</param>
        protected BaseController(
            ILocalizationHelper localizationHelper,
            IThemesHelper themesHelper,
            IBundlingHelper bundlingHelper)
        {
            LocalizationHelper = localizationHelper;
            ThemesHelper = themesHelper;
            BundlingHelper = bundlingHelper;
        }

        /// <summary>
        /// Gets or sets the localization helper to be used by the model.
        /// </summary>
        protected ILocalizationHelper LocalizationHelper { get; set; }

        /// <summary>
        /// Gets or sets the themes helper to be used by the model.
        /// </summary>
        protected IThemesHelper ThemesHelper { get; set; }

        /// <summary>
        /// Gets or sets the bundling helper to be used by the model.
        /// </summary>
        protected IBundlingHelper BundlingHelper { get; set; }

        /// <summary>
        /// Called before the action method is invoked
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
           
            // The menu model must always be available for all the views (as menu is always presented)
            // so its data is prepared for every single MVC action being executed.
            InitializeMenuModel();

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Reads information about the current state of the menu and makes it available for a view.
        /// </summary>
        protected virtual void InitializeMenuModel()
        {
            var model = new MenuModel(LocalizationHelper, ThemesHelper, BundlingHelper);
            model.Load(HttpContext);
            ViewData[MenuModel.Key] = model;
        }
    }
}
