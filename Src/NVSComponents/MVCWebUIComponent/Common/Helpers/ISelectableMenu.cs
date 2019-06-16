namespace Volvo.LAT.MVCWebUIComponent.Common.Helpers
{
    /// <summary>
    /// Defines a contract allowing to recognize wherever particular menu items are selected or not.
    /// </summary>
    public interface ISelectableMenu
    {
        /// <summary>
        /// Determines if a menu item for the specified controller action is recognized as the selected one or not.
        /// </summary>
        /// <param name="menuItemControllerName">A menu item controller name.</param>
        /// <param name="menuItemAction">A menu item action name from the controller.</param>
        /// <param name="menuItemArguments">A string representing all the arguments passed into the menu item action.</param>
        /// <returns>True if a menu item for the given controller, action and its arguments should be selected.</returns>
        bool IsMenuItemSelected(string menuItemControllerName, string menuItemAction, string menuItemArguments);

        /// <summary>
        /// Determines if a menu item is a first level one and should be highlighted.
        /// </summary>
        /// <param name="menuItemControllerName">The menu item controller name.</param>
        /// <param name="menuItemAction">The menu item action name.</param>
        /// <returns>True if the menu item is a first level (root) one and if it should be highlighted. False otherwise.</returns>
        bool IsFirstLevelHighlightableMenuItem(string menuItemControllerName, string menuItemAction);
    }
}
