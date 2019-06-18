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
using Volvo.NVS.Persistence.NHibernate.Web.SessionHandling;

namespace Volvo.LAT.MVCWebUIComponent.Common.ActionFilter
{
  
    public static class AuthorizationFilter
    {
        [NHibernateMvcSessionContext]
        public static bool IsAuthorized()
        {
            using (new NHibernateSessionContext())
            {
                //var claimsService = Container.Resolve<IClaimsService>();
                var userService = Container.Resolve<IUserService>();

                //if (!claimsService.IsPrincipal)
                //{
                //    return false;
                //}

                var currentUser = userService.GetCurrent();
               // var userName = claimsService.GetUserNameWithoutDomain();
               // var role = claimsService.Roles.First();
                //var roleTranslated =
                 //   UserDomain_Resources.ResourceManager.GetEnumValue((Role)Enum.Parse(typeof(Role), role));

                // This is the minimum guaranteed
               // var userText = $"{userName} ({roleTranslated})";

                // Lets' see if we can add more info
                if (currentUser != null && (currentUser.UserRole.Name == Role.Admin.ToString() || currentUser.UserRole.Name == Role.User.ToString()))
                {
                    return true;
                }
                return false;
            }
        }
        [NHibernateMvcSessionContext]
        public static bool IsAdmin()
        {
            using (new NHibernateSessionContext())
            {
               // var claimsService = Container.Resolve<IClaimsService>();
                var userService = Container.Resolve<IUserService>();

                //if (!claimsService.IsPrincipal)
                //{
                //    return false;
                //}
                var currentUser = userService.GetCurrent();
                if (currentUser != null && (currentUser.UserRole.Name == Role.Admin.ToString()))
                {
                    return true;
                }
                return false;
            }
        }

    }
}