using System;
using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Exceptions;
using Volvo.NVS.Core.Unity;
using Volvo.NVS.Security.Authorization;
using Volvo.NVS.Security.Claims;
using Volvo.NVS.Security.Exceptions;
using Volvo.LAT.UserDomain.DomainLayer.Entities;
using Volvo.LAT.UserDomain.DomainLayer.RepositoryInterfaces;

using Volvo.LAT.UserDomain.ServiceLayer;

namespace Volvo.LAT.UserDomain.DomainLayer
{
    /// <summary>
    /// Provides the authorization logic protecting the user domain.
    /// </summary>
    /// <remarks>
    /// The <see cref="UserAuthorizationService"/> provides implementation for the authorization logic
    /// available out of the domain boundaries as well as the one used by the user domain check
    /// access attributes.
    /// </remarks>
    public class UserAuthorizationService : IUserAuthorizationService, IUserCheckAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthorizationService"/> class.
        /// </summary>
        [InjectionConstructor]
        public UserAuthorizationService()
            : this(Container.Resolve<IClaimsService>(), Container.Resolve<IUserRepository>(), Container.Resolve<IUserService>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthorizationService"/> class.
        /// </summary>
        /// <param name="claimsService">The claims service.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="userService">The user service.</param>
        public UserAuthorizationService(IClaimsService claimsService, IUserRepository userRepository, IUserService userService)
        {
            ClaimsService = claimsService;
            UserRepository = userRepository;
            UserService = userService;
        }

        /// <summary>
        /// Gets or sets the user service.
        /// </summary>
        protected IUserService UserService { get; set; }

        /// <summary>
        /// Gets the claims service.
        /// </summary>
        protected IClaimsService ClaimsService { get; }

        /// <summary>
        /// Gets or sets the claims service.
        /// </summary>
        protected IUserRepository UserRepository { get; set; }

        #region ICheckAccess

        /// <summary>
        /// Check access, authorization for the current identity (user) and the requested operation.
        /// </summary>
        /// <param name="context">The current check access, authorization context.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        public bool CheckAccess(CheckAccessContext context) => IsAllowed(context, null);

        /// <summary>
        /// Check access, authorization for the current identity (user), the given user and the requested operation.
        /// </summary>
        /// <param name="context">The current check access, authorization context.</param>
        /// <param name="user">The user for which access should be verified.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        public bool CheckAccess(CheckAccessContext context, User user) => IsAllowed(context, user != null ? user.Username : null);

        /// <summary>
        /// Check access, authorization for the current identity (user), the given user name and the requested operation.
        /// </summary>
        /// <param name="context">The current check access, authorization context.</param>
        /// <param name="userName">The user name for which access should be verified.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        public bool CheckAccess(CheckAccessContext context, string userName) => IsAllowed(context, userName);

        #endregion ICheckAccess

        #region IUserAuthorizationService

        /// <summary>
        /// Check access, authorization for the current identity (user) and the requested operation.
        /// </summary>
        /// <param name="operation">The requested operation.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        public bool CheckAccess(string operation) => IsAllowed(operation, null);

        /// <summary>
        /// Check access, authorization for the current identity (user), the given user name and the requested operation.
        /// </summary>
        /// <param name="operation">The requested operation.</param>
        /// <param name="userName">The userName for which access should be verified.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        public bool CheckAccess(string operation, string userName) => IsAllowed(operation, userName);

        /// <summary>
        /// Ensures that a current user can read and manage other applications users.
        /// Throws the <see cref="NotAuthorizedException"/> if not access should be granted.
        /// </summary>
        /// <exception cref="NotAuthorizedException">Thrown when a user has not access into the overall user management.</exception>
        public void EnsureCanManageUsers()
        {
            if (!IsAllowed(UserOperations.RetrieveUser, ClaimsService.UserName))
            {
                throw new NotAuthorizedException();
            }
        }

        #endregion IUserAuthorizationService

        #region IsAllowed

        /// <summary>
        /// Determines if the operation (taken from the context) is allowed for the given user name (if any) and the current identity.
        /// </summary>
        /// <param name="context">A current check access, authorization context.</param>
        /// <param name="userName">A user name for which access should be verified.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        private bool IsAllowed(CheckAccessContext context, string userName)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // We are expecting exactly one operation to always be given. We do not
            // accept multiple operation names and we do not accept zero operation names
            return context.Operation.IsSingle && IsAllowed(context.Operation.First(), userName);
        }

        /// <summary>
        /// Determines if the operation is allowed for the given user name (if any) and the current identity.
        /// </summary>
        /// <param name="operation">The requested operation for which access should be checked.</param>
        /// <param name="userName">The user name for which access should be verified.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        private bool IsAllowed(string operation, string userName)
        {
            // We must have an identity assigned and authenticated
            if (!ClaimsService.IsPrincipal)
            {
                return false;
            }

            // At this stage exactly one operation name is given. Check access depending on
            // the name of the operation and the given user name (if any)
            switch (operation)
            {
                //case UserOperations.RetrieveUser:
                   // return ClaimsService.IsInRole(Role.Admin.ToString()) || ClaimsService.IsInRole(Role.Member.ToString());
                case UserOperations.AddUser:
                    return ClaimsService.IsInRole(Role.Admin.ToString()) || IsSameUser(userName); // Admin or if I am adding my own user (same user name)
                case UserOperations.EditUser:
                    if (string.IsNullOrEmpty(userName))
                    {
                        throw new InvalidOperationException($"The user name is required for access check for {operation}");
                    }

                    return ClaimsService.IsInRole(Role.Admin.ToString()) || IsSameUser(userName); // Admin or if I am adding my own user (same user name)
                case UserOperations.DeleteUser:
                    if (string.IsNullOrEmpty(userName))
                    {
                        throw new InvalidOperationException($"The user name is required for access check for {operation}");
                    }

                    return ClaimsService.IsInRole(Role.Admin.ToString());

                default:
                    throw new NVSException("Access Denied");
            }
        }

        /// <summary>
        /// Determines if the current identity, user is the profile of the current user.
        /// </summary>
        /// <param name="userName">The user name for which the ownership should be verified.</param>
        /// <returns>True if the current user is the provided user name.</returns>
        private bool IsSameUser(string userName) =>
            ClaimsService.GetUserNameWithoutDomain().Equals(userName, StringComparison.InvariantCultureIgnoreCase);

        #endregion IsAllowed
    }
}
