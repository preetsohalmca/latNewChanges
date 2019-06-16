using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using Volvo.NVS.Core.Unity;
using Volvo.LAT.MVCWebUIComponent.Common.Helpers;

namespace Volvo.LAT.MVCWebUIComponent.Common.Extensions
{
    /// <summary>
    /// Theme related extensions into the <see cref="HtmlHelper"/>.
    /// </summary>
    public static class ThemesExtension
    {
        /// <summary>
        /// Get the name of the currently used Theme.
        /// </summary>
        /// <param name="instance">HtmlHelper instance to be extended.</param>
        /// <returns>Name of the theme which can also map into the theme folder (under Content/nvs).</returns>
        /// <remarks>
        /// The implementation of the IThemesHelper is responsible for giving information
        /// about assigned themes and for assigning the themes as well. Check the IThemesHelper.
        /// </remarks>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "instance")]
        public static string GetCurrentTheme(this HtmlHelper instance) =>
            Container.Resolve<IThemesHelper>().GetCurrentTheme(new HttpContextWrapper(HttpContext.Current));
    }
}