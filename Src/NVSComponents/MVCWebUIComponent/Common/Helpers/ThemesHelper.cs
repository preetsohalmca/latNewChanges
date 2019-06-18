using System.Web;
using Volvo.NVS.Utilities.Web.Cookies;

namespace Volvo.LAT.MVCWebUIComponent.Common.Helpers
{
    /// <summary>
    /// The POS application theme helper managing the themes.
    /// </summary>
    public class ThemesHelper : IThemesHelper
    {
        /// <summary>
        /// The cookie name storing theme information.
        /// </summary>
        public const string SelectedThemeCookie = "POS.CurrentSelectedTheme";

        /// <summary>
        /// The default / fall-back theme name used when a theme has never been selected by the user before.
        /// </summary>
        public const string DefaultThemeName = "Violin";

        /// <summary>
        /// Set the current theme
        /// </summary>
        /// <param name="context">The Http context used to set the theme cookie into.</param>
        /// <param name="themeName">Theme name</param>
        public void SetCurrentTheme(HttpContextBase context, string themeName)
        {
            var cookie = context.GetOrCreateCookie(SelectedThemeCookie);
            cookie.Value = themeName;
            context.SetCookie(cookie);
        }

        /// <summary>
        /// Get the current theme name
        /// </summary>
        /// <param name="context">The Http context used to get the theme cookie from.</param>
        /// <returns>Name of the current theme</returns>
        public string GetCurrentTheme(HttpContextBase context)
        {
            var cookie = context.GetOrCreateCookie(SelectedThemeCookie);

            if (string.IsNullOrEmpty(cookie.Value))
            {
                // Defaults
                cookie.Value = DefaultThemeName;
                context.SetCookie(cookie);
            }

            return cookie.Value;
        }
    }
}
