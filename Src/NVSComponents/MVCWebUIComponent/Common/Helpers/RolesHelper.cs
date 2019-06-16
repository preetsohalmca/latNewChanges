using System;
using System.Collections.Generic;
using Volvo.NVS.Utilities.ResourceManagement;
using Volvo.LAT.MVCWebUIComponent.Models.Shared;
using Volvo.LAT.UserDomain.DomainLayer.Entities;


namespace Volvo.LAT.MVCWebUIComponent.Common.Helpers
{
    /// <summary>
    /// Helper for the <see cref="Role"/> class.
    /// </summary>
    public static class RolesHelper
    {
        /// <summary>
        /// Returns a list of <see cref="RoleModel"/> objects containing the roles
        /// in the <see cref="Role"/> enumerator but with their names localized.
        /// </summary>
        /// <returns>List of <see cref="RoleModel"/> localized.</returns>
        public static IList<RoleModel> GetAllRolesLocalized()
        {
            var roleValues = Enum.GetValues(typeof(Role));

            var roles = new List<RoleModel>();

            foreach (var roleValue in roleValues)
            {
                //var name = UserDomain_Resources.ResourceManager.GetEnumValue((Role)roleValue);
                //roles.Add(new RoleModel
                //{
                //    Name = name,
                //    Number = (int)roleValue
                //});
            }

            return roles;
        }
    }
}