using System;
using System.Linq;
using System.Web.Mvc;
using Volvo.NVS.Core.Unity;
using Volvo.NVS.Persistence.NHibernate.SessionHandling;
using Volvo.NVS.Security.Claims;
using Volvo.NVS.Utilities.ResourceManagement;
using Volvo.LAT.UserDomain.DomainLayer.Entities; 
using Volvo.LAT.UserDomain.ServiceLayer;

using Volvo.LAT.POLineDomain.ServiceLayer;

namespace Volvo.LAT.MVCWebUIComponent.Common.Extensions
{
    /// <summary>
    /// Extends the <see cref="HtmlHelper"/> with the user related functionality.
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Gets the current user available in the session.
        /// </summary>
        /// <param name="instance">The html helper to be extended.</param>
        /// <returns>The user.</returns>
        public static string GetCurrentUser(this HtmlHelper instance)
        {
            using (new NHibernateSessionContext())
            {
                if (instance == null)
                {
                    throw new ArgumentNullException("instance");
                }

                var claimsService = Container.Resolve<IClaimsService>();
                var userService = Container.Resolve<IUserService>();
                var poLineService = Container.Resolve<IPOLineService>();

                if (!claimsService.IsPrincipal)
                {
                    return string.Empty;
                }

                var currentUser = userService.GetCurrent();
                var userName = claimsService.GetUserNameWithoutDomain();
                var role = claimsService.Roles.First();
                //var roleTranslated =
                //    UserDomain_Resources.ResourceManager.GetEnumValue((Role)Enum.Parse(typeof(Role), role));

                // This is the minimum guaranteed
                //var userText = $"{userName} ({roleTranslated})";
                var userText = string.Empty;
                // Lets' see if we can add more info
                if (currentUser != null)
                {
                    var fistName = currentUser.Name;
                    var lastName = string.Empty;

                    if (!string.IsNullOrWhiteSpace(fistName) && !string.IsNullOrWhiteSpace(lastName))
                    {
                        userText = $"{fistName} {lastName}";
                    }
                    else if (!string.IsNullOrWhiteSpace(fistName))
                    {
                        userText = $"{fistName} - {userText}";
                    }
                }

                return userText;
            }
        }
    }
}