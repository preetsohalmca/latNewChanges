using System.Web;
using System.Web.Optimization;
using Volvo.NVS.Utilities.Web.Cookies;

namespace Volvo.LAT.MVCWebUIComponent.Common.Helpers
{
    /// <summary>
    /// The helper managing settings and configuring the Bundling and Minification feature. The current bundling
    /// settings will be set and transported on the cookie.
    /// </summary>
    public class BundlingHelper : IBundlingHelper
    {
        /// <summary>
        /// Name of the cookie which will tell if the Bundling and Minification is enabled or not.
        /// </summary>
        private const string BundlingCookieName = "POS.BundlingMinificationEnabled";

        /// <summary>
        /// The default configuration for the bundling and minification feature for when no setting has been
        /// saved before. Default value is 'false'.
        /// </summary>
        public const bool BundlingEnabledDefaultValue = false;

        /// <summary>
        /// Enables or disables the Bundling and Minification feature for the application and also sets this setting
        /// in a cookie for future usage by the method <see cref="IsBundlingAndMinificationEnabled(HttpContextBase)"/>.
        /// </summary>
        /// <param name="context">The Http Context used for getting/setting the bundling cookie setting.</param>
        /// <param name="enable">Pass 'true' for enabling the Bundling and Minification feature. Otherwise, pass 'false'.</param>
        public void EnableBundlingAndMinification(HttpContextBase context, bool enable)
        {
            BundleTable.EnableOptimizations = enable;

            var cookie = context.GetOrCreateCookie(BundlingCookieName);
            cookie.Value = enable.ToString();
            context.SetCookie(cookie);
        }

        /// <summary>
        /// Verifies and returns whether or not the Bundling and Minification feature is enabled by reading
        /// the cookie settings.
        /// </summary>
        /// <param name="context">The Http Context used for getting the bundling cookie setting.</param>
        /// <returns>
        /// <para>If there is no cookie set for bundling and minification, or if the existing cookie does not have a valid
        /// content, then the default value (see <see cref="BundlingEnabledDefaultValue"/>) is returned.</para>
        /// <para>Otherwise, the cookie value (true or false) is returned, indicating the Bundling and Minification
        /// feature is enabled/disabled.</para>
        /// </returns>
        public bool IsBundlingAndMinificationEnabled(HttpContextBase context)
        {
            var cookie = context.GetCookie(BundlingCookieName);

            if (cookie == null)
            {
                return BundlingEnabledDefaultValue;
            }

            bool enabled;
            if (!bool.TryParse(cookie.Value, out enabled))
            {
                return BundlingEnabledDefaultValue;
            }

            return enabled;
        }

        /// <summary>
        /// <para>Tries to load the settings for the Bundling and Minification from the cookies, set the application
        /// BundleTable.EnableOptimizations property with the value and return this same value.</para>
        /// <para>If the cookie does not exist or has an invalid value, it writes a new cookie with the default
        /// value (see also <see cref="BundlingEnabledDefaultValue"/>).</para>
        /// </summary>
        /// <param name="context">The Http Context used for getting/setting the bundling cookie setting.</param>
        /// <returns>True if Bundling and Minification has been enabled. False, otherwise.</returns>
        public bool LoadSettingsForBundlingAndMinification(HttpContextBase context)
        {
            bool enableBundling = BundlingEnabledDefaultValue;
            bool writeDefaultCookie = true;

            var cookie = context.GetCookie(BundlingCookieName);
            if (cookie != null)
            {
                if (bool.TryParse(cookie.Value, out enableBundling))
                {
                    writeDefaultCookie = false;
                }
            }

            if (writeDefaultCookie)
            {
                // No cookie found, or it has an invalid value, let's fix it.
                cookie = context.GetOrCreateCookie(BundlingCookieName);
                cookie.Value = BundlingEnabledDefaultValue.ToString();
                context.SetCookie(cookie);
            }

            BundleTable.EnableOptimizations = enableBundling;
            return enableBundling;
        }

        /// <summary>
        /// <para>Tries to load the settings for the Bundling and Minification from the cookies, set the application
        /// BundleTable.EnableOptimizations property with the value and return this same value.</para>
        /// <para>If the cookie does not exist or has an invalid value, it writes a new cookie with the default
        /// value (see also <see cref="BundlingEnabledDefaultValue"/>).</para>
        /// </summary>
        /// <param name="context">The Http Context used for getting/setting the bundling cookie setting.</param>
        /// <returns>True if Bundling and Minification has been enabled. False, otherwise.</returns>
        public bool LoadSettingsForBundlingAndMinification(HttpContext context) =>
            LoadSettingsForBundlingAndMinification(new HttpContextWrapper(context));
    }
}
