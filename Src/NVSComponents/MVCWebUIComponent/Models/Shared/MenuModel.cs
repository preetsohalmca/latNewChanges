using System;
using System.Web;
using Volvo.NVS.Utilities.Web.Extensions;
using Volvo.NVS.Utilities.Web.Localization;
using Volvo.LAT.MVCWebUIComponent.Common.Extensions;
using Volvo.LAT.MVCWebUIComponent.Common.Helpers;
using Volvo.LAT.MVCWebUIComponent.Controllers;

namespace Volvo.LAT.MVCWebUIComponent.Models.Shared
{
    /// <summary>
    /// Represents a common menu model used to trace selected menu items.
    /// </summary>
    public class MenuModel : ISelectableMenu
    {
        /// <summary>
        /// A localization helper to be used by the model.
        /// </summary>
        private readonly ILocalizationHelper localizationHelper;

        /// <summary>
        /// A themes helper to be used by the model.
        /// </summary>
        private readonly IThemesHelper themesHelper;

        /// <summary>
        /// A bundling helper to be used by the model.
        /// </summary>
        private readonly IBundlingHelper bundlingHelper;

        /// <summary>
        /// Gets or sets the name of the current UI culture selected for the application.
        /// </summary>
        public string CultureName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current theme selected for the application.
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether bundling is enabled.
        /// </summary>
        public bool BundlingEnabled { get; set; }

        /// <summary>
        /// The controller name from the Request
        /// </summary>
        private string requestedController;

        /// <summary>
        /// The action name from the Request
        /// </summary>
        private string requestedAction;

        /// <summary>
        /// Gets the key used to store information about the current menu state in a View Data dictionary.
        /// </summary>
        public static string Key => typeof(MenuModel).Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuModel"/> class.
        /// </summary>
        /// <param name="localizationHelper">A localization helper to be used by the model.</param>
        /// <param name="themesHelper">A themes helper to be used by the model.</param>
        /// <param name="bundlingHelper">A bundling helper to be used by the model.</param>
        public MenuModel(ILocalizationHelper localizationHelper, IThemesHelper themesHelper, IBundlingHelper bundlingHelper)
        {
            if (localizationHelper == null)
            {
                throw new ArgumentNullException("localizationHelper");
            }

            if (themesHelper == null)
            {
                throw new ArgumentNullException("themesHelper");
            }

            if (bundlingHelper == null)
            {
                throw new ArgumentNullException("bundlingHelper");
            }

            this.themesHelper = themesHelper;
            this.localizationHelper = localizationHelper;
            this.bundlingHelper = bundlingHelper;
        }

        /// <summary>
        /// Loads and initializes the menu model.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        public void Load(HttpContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            CultureName = localizationHelper.GetCurrentCultureName(context);
            ThemeName = themesHelper.GetCurrentTheme(context);
            BundlingEnabled = bundlingHelper.LoadSettingsForBundlingAndMinification(context);

            // Store locally the controller and action from the Request
            requestedController = context.Request.GetController();
            requestedAction = context.Request.GetAction();
        }

        /// <summary>
        /// A name of the action which is applied to the Utility related menu items.
        /// </summary>
        private static readonly string UtilityMenuItemControllerName = typeof(UtilityController).ToControllerName();

        /// <summary>
        /// A name of the action which is applied to the Home related menu items.
        /// </summary>
        private static readonly string HomeMenuItemControllerName = typeof(HomeController).ToControllerName();

        /// <summary>
        /// A name of the action which is applied to the User related menu items.
        /// </summary>
        private static readonly string UserMenuItemControllerName = typeof(UserController).ToControllerName();

        /// <summary>
        /// Determines if a menu item for the specified controller action should be marked as selected or not.
        /// </summary>
        /// <param name="menuItemControllerName">A menu item controller name.</param>
        /// <param name="menuItemAction">A menu item action name from the controller.</param>
        /// <param name="menuItemArguments">A string representing all the arguments passed into the menu item action.</param>
        /// <returns>True if a menu item for the given controller, action and its arguments should be selected.</returns>
        public bool IsMenuItemSelected(string menuItemControllerName, string menuItemAction, string menuItemArguments)
        {
            // Selections for the Utility controller
            if (menuItemControllerName == UtilityMenuItemControllerName)
            {
                // If a menu item is selecting themes and it is selecting currently used theme then it should be marked.
                if (menuItemAction == "SetTheme" && menuItemArguments == ThemeName)
                {
                    return true;
                }

                // If a menu item is selecting languages and it is for the currently used language then it should be marked.
                if (menuItemAction == "SetLocalization" && menuItemArguments != null
                    && menuItemArguments.ToUpperInvariant() == CultureName.ToUpperInvariant())
                {
                    return true;
                }

                // If a menu item is for minification and it is representing the current choice then it should be marked.
                if (menuItemAction == "EnableBundlingAndMinification" && menuItemArguments != null
                    && menuItemArguments.ToUpperInvariant() == BundlingEnabled.ToString().ToUpperInvariant())
                {
                    return true;
                }
            }
            else
            {
                // Since the automatic selection was disabled and we are doing things manually, we must also enable the first level (root) items.
                return IsFirstLevelHighlightableMenuItem(menuItemControllerName, menuItemAction);
            }

            return false;
        }

        /// <summary>
        /// Determines if a menu item is a first level one and should be highlighted.
        /// </summary>
        /// <param name="menuItemControllerName">The menu item controller name.</param>
        /// <param name="menuItemAction">The menu item action name.</param>
        /// <returns>True if the menu item is a first level (root) one and if it should be highlighted. False otherwise.</returns>
        public bool IsFirstLevelHighlightableMenuItem(string menuItemControllerName, string menuItemAction) =>
            (menuItemControllerName == HomeMenuItemControllerName ||  
             menuItemControllerName == UserMenuItemControllerName) //// && menuItemAction == "Index"
            && menuItemControllerName == requestedController && menuItemAction == requestedAction;
    }
}