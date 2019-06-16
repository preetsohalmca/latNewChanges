using System.Web;

namespace Volvo.LAT.MVCWebUIComponent.Common.Helpers
{
    /// <summary>
    /// Defines a contract for the bundling helper managing settings and configuring the Bundling and Minification feature.
    /// </summary>
    /// <remarks>
    /// See also <see cref="BundlingHelper"/>.
    /// </remarks>
    public interface IBundlingHelper
    {
        /// <summary>
        /// Enables or disables the Bundling and Minification feature for the application and also sets this setting
        /// in a cookie for future usage by the method <see cref="IsBundlingAndMinificationEnabled(HttpContextBase)"/>.
        /// </summary>
        /// <param name="context">The Http Context used for getting/setting the bundling cookie setting.</param>
        /// <param name="enable">Pass 'true' for enabling the Bundling and Minification feature. Otherwise, pass 'false'.</param>
        void EnableBundlingAndMinification(HttpContextBase context, bool enable);

        /// <summary>
        /// Verifies and returns whether or not the Bundling and Minification feature is enabled by reading
        /// the cookie settings.
        /// </summary>
        /// <param name="context">The Http Context used for getting the bundling cookie setting.</param>
        /// <returns>
        /// <para>If there is no cookie set for bundling and minification, or if the existing cookie does not have a valid
        /// content, then the default value is returned.</para>
        /// <para>Otherwise, the cookie value (true or false) is returned, indicating the Bundling and Minification
        /// feature is enabled/disabled.</para>
        /// </returns>
        bool IsBundlingAndMinificationEnabled(HttpContextBase context);

        /// <summary>
        /// <para>Tries to load the settings for the Bundling and Minification from the cookies, set the application
        /// BundleTable.EnableOptimizations property with the value and return this same value.</para>
        /// <para>If the cookie does not exist or has an invalid value, it writes a new cookie with the default
        /// value.</para>
        /// </summary>
        /// <param name="context">The Http Context used for getting/setting the bundling cookie setting.</param>
        /// <returns>True if Bundling and Minification has been enabled. False, otherwise.</returns>
        bool LoadSettingsForBundlingAndMinification(HttpContextBase context);

        /// <summary>
        /// <para>Tries to load the settings for the Bundling and Minification from the cookies, set the application
        /// BundleTable.EnableOptimizations property with the value and return this same value.</para>
        /// <para>If the cookie does not exist or has an invalid value, it writes a new cookie with the default
        /// value.</para>
        /// </summary>
        /// <param name="context">The Http Context used for getting/setting the bundling cookie setting.</param>
        /// <returns>True if Bundling and Minification has been enabled. False, otherwise.</returns>
        bool LoadSettingsForBundlingAndMinification(HttpContext context);
    }
}
