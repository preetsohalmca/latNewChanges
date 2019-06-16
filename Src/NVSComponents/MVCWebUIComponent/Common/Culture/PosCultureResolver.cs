using System.Collections.Generic;
using System.Globalization;
using System.Web.Configuration;
using Volvo.NVS.Core.Exceptions;
using Volvo.NVS.Utilities.Web.Localization;
using Volvo.NVS.Utilities.Web.Localization.Culture;
using Volvo.LAT.MVCWebUIComponent.App_LocalResources;

namespace Volvo.LAT.MVCWebUIComponent.Common.Culture
{
    /// <summary>
    /// Provides information about available cultures in the POS sample application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Once the implementation of the <see cref="ICultureResolver"/> is provided and registered under the Unity
    /// the Utilities library will look for it and resolve it. For example the Utilities <see cref="LocalizationHelper"/>
    /// class will use the culture information given by the implementation of the <see cref="ICultureResolver"/>.
    /// </para>
    /// <para>
    /// The POS application takes the list of cultures from the application configuration file. Different approaches
    /// are possible. The Volvo Utilities library provides also various out of the box culture resolver implementations.
    /// Check the Utilities library documentation for details.
    /// </para>
    /// </remarks>
    public class PosCultureResolver : ICultureResolver
    {
        /// <summary>
        /// Provides the list of available application cultures.
        /// </summary>
        /// <returns>
        /// The collection of application cultures.
        /// Names must be compatible with <see cref="CultureInfo"/>.
        /// </returns>
        public IEnumerable<string> GetCultureNames()
        {
            string cultures = WebConfigurationManager.AppSettings["AvailableCultures"];
            if (string.IsNullOrEmpty(cultures))
            {
                throw new NVSException(CommonResource.Error_CulturesNotConfigured);
            }

            return cultures.Split(';');
        }
    }
}