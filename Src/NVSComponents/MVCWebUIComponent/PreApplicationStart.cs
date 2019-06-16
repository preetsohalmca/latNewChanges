using System.Web;
using Volvo.LAT.MVCWebUIComponent;
using Volvo.LAT.MVCWebUIComponent.Security;

// Register the initialization code to be run before the Http Application is established
[assembly: PreApplicationStartMethod(typeof(PreApplicationStart), "Initialize")]

namespace Volvo.LAT.MVCWebUIComponent
{
    public static class PreApplicationStart
    {
        /// <summary>
        /// Configures the system before the web application is initialized.
        /// </summary>
        public static void Initialize()
        {
            SecurityConfig.PreConfigure();
        }
    }
}