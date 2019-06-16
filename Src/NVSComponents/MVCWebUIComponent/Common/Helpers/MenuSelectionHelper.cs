using System;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using MvcSiteMapProvider;
using Volvo.LAT.MVCWebUIComponent.Common.Extensions;

namespace Volvo.LAT.MVCWebUIComponent.Common.Helpers
{
    /// <summary>
    /// Helps in the selection of menu items binding the <see cref="ISiteMapNode"/> nodes to the <see cref="MenuItem"/> items.
    /// </summary>
    public static class MenuSelectionHelper
    {
        /// <summary>
        /// Determines if a menu item should be marked as selected.
        /// </summary>
        /// <param name="node">A current site map node to base the selection on.</param>
        /// <param name="model">A menu model where selection logic is placed.</param>
        /// <returns>True if the menu item should be selected.</returns>
        private static bool IsMenuSelected(ISiteMapNode node, ISelectableMenu model)
        {
            // The selection is based on the controller name.
            if (string.IsNullOrEmpty(node.Controller))
            {
                return false;
            }

            // The action is also needed in order to know what specifically is about to be selected.
            if (string.IsNullOrEmpty(node.Action))
            {
                return false;
            }

            return model.IsMenuItemSelected(node.Controller, node.Action.ToActionName(), node.Action.ToActionValue());
        }

        /// <summary>
        /// Binds the menu items from the site map nodes.
        /// </summary>
        /// <param name="builder">A builder to be extended.</param>
        /// <param name="model">A menu model according to which a menu item selection can be decided.</param>
        /// <returns>The current builder object.</returns>
        public static NavigationBindingBuilder<MenuItem, ISiteMapNode> BindAndSelectMenuItems(this NavigationBindingBuilder<MenuItem, ISiteMapNode> builder, ISelectableMenu model)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            return builder.ItemDataBound((menuItem, siteMapNode) =>
            {
                // Do the standard binding common for all the items
                menuItem.Text = siteMapNode.Title;
                menuItem.ActionName = siteMapNode.Action;
                menuItem.ControllerName = siteMapNode.Controller;
                //// menuItem.Visible = true; //NEWMODEL: use CheckAccess to fix it

                // Mark desired items as selected
                if (IsMenuSelected(siteMapNode, model))
                {
                    menuItem.HtmlAttributes.Add("class", GetSelectStyle(siteMapNode, model));
                }
            });
        }

        /// <summary>
        /// Gets the CSS style to be used depending on the menu item type.
        /// </summary>
        /// <param name="node">A current site map node to base the selection on.</param>
        /// <param name="model">A menu model where selection logic is placed.</param>
        /// <returns>The string containing the CSS style to be used for a selected menu item.</returns>
        /// <remarks>
        /// First level menu item has a different style than the sub-menu items.
        /// </remarks>
        private static string GetSelectStyle(ISiteMapNode node, ISelectableMenu model) =>
            model.IsFirstLevelHighlightableMenuItem(node.Controller, node.Action.ToActionName()) ? "k-state-highlight" : "k-state-selected";
    }
}