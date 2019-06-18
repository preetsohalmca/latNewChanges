using System.Web;

namespace Volvo.LAT.MVCWebUIComponent.Common.Helpers
{
    /// <summary>
    /// Defines a contract for the application themes helper (managing the themes).
    /// </summary>
    public interface IThemesHelper
    {
        /// <summary>
        /// Set the current theme.
        /// </summary>
        /// <param name="context">The Http context used to set the theme cookie into.</param>
        /// <param name="themeName">The name of the theme.</param>
        void SetCurrentTheme(HttpContextBase context, string themeName);

        /// <summary>
        /// Get the current theme name.
        /// </summary>
        /// <param name="context">The Http context used to get the theme cookie from.</param>
        /// <returns>Name of the current theme.</returns>
        string GetCurrentTheme(HttpContextBase context);
    }
}
