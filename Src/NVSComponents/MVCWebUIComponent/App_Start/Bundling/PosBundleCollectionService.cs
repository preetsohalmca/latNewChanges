using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web.Optimization;
using Volvo.NVS.Utilities.Web.Bundling;

namespace Volvo.LAT.MVCWebUIComponent.Bundling
{
    /// <summary>
    /// Performs the bundle configuration for the POS sample application.
    /// </summary>
    /// <remarks>
    /// The <see cref="PosBundleCollectionService"/> is used by the implementation of the <see cref="IBundleConfig"/>
    /// in order to run the configuration process. Check the <see cref="BundleConfig"/> for details and see the
    /// <see cref="BundleConfig.Configure"/> method.
    /// </remarks>
    public class PosBundleCollectionService : BundleCollectionServiceBase
    {
        private readonly string kendoVersion = ConfigurationManager.AppSettings["KendoVersion"];
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private readonly string telerikReportVersion = ConfigurationManager.AppSettings["telerikReportVersion"];

        /// <summary>
        /// Current available brands
        /// </summary>
        private readonly IList<string> availableBrands = new List<string>
        {
            "Mack",
            "MackDual",
            "Renault",
            "Violin",
            "VolvoBA",
            "VolvoGroup"
        };

        public override void RegisterScriptBundles(BundleCollection bundles)
        {
            if (bundles == null)
            {
                throw new ArgumentNullException("bundles");
            }

            bundles.Add(new ScriptBundle("~/Scripts/Js/CommonJsBundle").Include(
                "~/Scripts/jquery-{version}.min.js",
                $"~/Scripts/kendo/{kendoVersion}/kendo.all.min.js",
                $"~/Scripts/kendo/{kendoVersion}/kendo.aspnetmvc.min.js",
                "~/Scripts/nvs/nvs.common.js",
                "~/Scripts/pos/common.js",
                "~/Scripts/pos/formUtility.js",
                "~/Scripts/pos/cartItem.js",
                "~/Scripts/pos/cartService.js",
                "~/Scripts/pos/shoppingCartSummary.js",
                "~/Scripts/pos/order/checkout.js",
                "~/Scripts/pos/order/trackOrders.js",
                "~/Scripts/pos/order/viewOrder.js",
                "~/Scripts/pos/part/searchParts.js",
                "~/Scripts/pos/shoppingCart/viewShoppingCart.js",
                "~/Scripts/pos/user/addEditUser.js",
                "~/Scripts/pos/user/manageUsers.js").DisableOrdering());

            // Validation (this one is minified)
            bundles.Add(new ScriptBundle("~/Scripts/Js/ValidationJsBundle").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/nvs/nvs.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/nvs/nvs.validate.unobtrusive.adapters.js").DisableOrdering());
        }

        /// <summary>
        /// Registers a CSS bundle for each of the specific brands/themes
        /// </summary>
        /// <param name="bundles">The bundle collection</param>
        public override void RegisterStyleBundles(BundleCollection bundles)
        {
            if (bundles == null)
            {
                throw new ArgumentNullException("bundles");
            }

            foreach (var brand in availableBrands)
            {
                var bundleName = $"~/Content/nvs/{brand}/CssBundle";
                bundles.Add(
                    new StyleBundle(bundleName)

                        // NVS base styles
                        .Include("~/Content/nvs/nvs.base.common.css", new CssRewriteUrlTransformWrapper())
                        .Include($"~/Content/nvs/nvs.base.{brand}.css", new CssRewriteUrlTransformWrapper())

                        // Kendo styles
                        .Include($"~/Content/kendo/{kendoVersion}/kendo.common.min.css", new CssRewriteUrlTransformWrapper()) // NVS Kendo styles
                        .Include($"~/Content/nvs/nvs.kendo.themebuilder.{brand}.css", new CssRewriteUrlTransformWrapper())
                        .Include("~/Content/nvs/nvs.kendo.common.css", new CssRewriteUrlTransformWrapper())
                        .Include($"~/Content/nvs/nvs.kendo.{brand}.css", new CssRewriteUrlTransformWrapper())

                        // POS styles
                        .Include("~/Content/pos.css", new CssRewriteUrlTransformWrapper())
                        .DisableOrdering());
            }
        }
    }
}