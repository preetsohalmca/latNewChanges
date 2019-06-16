using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Volvo.LAT.UserDomain.DomainLayer.Entities;
using Volvo.LAT.UserDomain.DomainLayer.Projections.Management;
using Volvo.LAT.UserDomain.DomainLayer.Projections.Management;
using System.Collections.Generic;
using System;

namespace Volvo.LAT.UserDomain.ServiceLayer
{
    /// <summary>
    /// Service interface for UserDomain component
    /// </summary>
    public interface IUserService
    {
        User GetUser(string userName);

        User GetUser(string userName, bool includeDeleted);

        User GetUser(long userId);

        User GetCurrent();

        ValidationResults CreateUser(User user);

        ValidationResults SaveUser(User user);

        ValidationResults DeleteUser(User user, bool hasOrder);

        IQueryable<UserManagement> FindUserManagementAsQueryable();

        bool IsCurrentUserAdmin();

        bool IsCurrentUserRegistered();

        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
        void SaveUserLoginTime();

        //List<POLineDetails> GetListOfPODatas();
        User GetUserBtUserId(Guid userId);
    }
}
