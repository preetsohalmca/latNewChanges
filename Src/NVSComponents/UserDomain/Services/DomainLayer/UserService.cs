using System;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Volvo.NVS.Core.Diagnostics.Annotations;
using Volvo.NVS.Logging;
using Volvo.NVS.Persistence.NHibernate.SessionHandling;
using Volvo.NVS.Security.Claims;
using Volvo.LAT.UserDomain.DomainLayer.Attributes;
using Volvo.LAT.UserDomain.DomainLayer.Entities;
using Volvo.LAT.UserDomain.DomainLayer.ProjectionRepositoryInterfaces;
using Volvo.LAT.UserDomain.DomainLayer.Projections.Management;
using Volvo.LAT.UserDomain.DomainLayer.RepositoryInterfaces;
using Volvo.LAT.UserDomain.ServiceLayer;
using System.Collections.Generic;

namespace Volvo.LAT.UserDomain.DomainLayer
{
    /// <summary>
    /// The user service managing users, roles and user claims.
    /// </summary>
    public partial class UserService : IUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="userManagementRepository">A user management repository.</param>
        /// <param name="claimsService">The claims service to be used.</param>
        public UserService(IUserRepository userRepository, IUserManagementRepository userManagementRepository, IClaimsService claimsService = null)
        {
            UserRepository = userRepository;
            UserManagementRepository = userManagementRepository;
            ClaimsService = claimsService;
        }

        /// <summary>
        /// Gets the user repository.
        /// </summary>
        protected IUserRepository UserRepository { get; }

        /// <summary>
        /// Gets the user management repository.
        /// </summary>
        protected IUserManagementRepository UserManagementRepository { get; }

        /// <summary>
        /// Gets the claims service.
        /// </summary>
        protected IClaimsService ClaimsService { get; }

        /// <summary>
        /// Method to get user by user name (login)
        /// </summary>
        /// <param name="userName">user login</param>
        /// <returns>User entity</returns>
        public virtual User GetUser(string userName) => GetUser(userName, false);

        /// <summary>
        /// Method to get user by user name (login)
        /// </summary>
        /// <param name="userName">user login</param>
        /// <param name="includeDeleted">A flag telling if a user marked as deleted should be returned.</param>
        /// <returns>User entity</returns>
        public virtual User GetUser(string userName, bool includeDeleted) => UserRepository.FindByUserName(userName, includeDeleted);

        /// <summary>
        /// Method to get user by Id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>User entity</returns>
        public virtual User GetUser(long userId) => UserRepository.Load(userId);

        /// <summary>
        /// Method to get currently logged in user.
        /// </summary>
        /// <returns>Logged in user</returns>
        public virtual User GetCurrent() =>
            GetUser(ClaimsService.GetUserNameWithoutDomain())/* ?? User.CreateGuest(ClaimsService.GetUserNameWithoutDomain())*/;

        /// <summary>
        /// Method for creating new user
        /// </summary>
        /// <param name="user">The user to create</param>
        /// <returns>Validation results</returns>
        [UserCheckAccess(UserOperations.AddUser)]
        public virtual ValidationResults CreateUser([NotNull] User user)
        {
            var results = ValidateUser(user);
            if (!results.IsValid)
            {
                return results;
            }

            using (var scope = new TransactionScope())
            {
                UserRepository.Save(user);
                scope.Complete();
            }

            Log.Info($"New user has been created (UserName: {user.Username}).");

            return results;
        }

        /// <summary>
        /// Method for saving existing user
        /// </summary>
        /// <param name="user">user to be updated</param>
        /// <returns>Validation results</returns>
        [UserCheckAccess(UserOperations.EditUser)]
        public virtual ValidationResults SaveUser([NotNull] User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // When a user is marked as deleted and already exists we will mark it back as active.
            //user.MarkAsActive();

            var results = ValidateUser(user);
            if (results.IsValid)
            {
                UpdateUser(user);
            }

            return results;
        }

        /// <summary>
        /// It deletes if the user has no orders or set IsDeleted when has orders
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="hasOrder">If has orders</param>
        /// /// <returns>Validation results</returns>
        [UserCheckAccess(UserOperations.DeleteUser)]
        public virtual ValidationResults DeleteUser(User user, bool hasOrder)
        {
            var result = ValidateUser(user);
            if (!result.IsValid)
            {
                return result;
            }

            if (hasOrder)
            {
              //  user.MarkAsDeleted();
                UpdateUser(user);
            }
            else
            {
                DeleteUser(user);
            }

            return result;
        }

        /// <summary>
        /// Removes an user.
        /// </summary>
        /// <param name="user">A user.</param>
        private void DeleteUser(User user)
        {
            using (var scope = new TransactionScope())
            {
                UserRepository.Remove(user);
                scope.Complete();
            }

            Log.Info($"User has been deleted (User Id: {user.Id}).");
        }

        /// <summary>
        /// Finds user management projections.
        /// </summary>
        /// <returns>A queryable over the user management projection.</returns>
        [UserCheckAccess(UserOperations.RetrieveUser)]
        public virtual IQueryable<UserManagement> FindUserManagementAsQueryable() => UserManagementRepository.FindNonDeleted();

        /// <summary>
        /// Verifies if the current logged in user is an administrator
        /// </summary>
        /// <returns>True if user Role is Admin. False otherwise</returns>
        public bool IsCurrentUserAdmin() => GetCurrent().UserRole.Name == Role.Admin.ToString();

        /// <summary>
        /// Verifies if the current logged in user is registered in the application (DB)
        /// </summary>
        /// <returns>True if the user is registered, false otherwise.</returns>
        public bool IsCurrentUserRegistered() => UserRepository.IsUserRegistered(ClaimsService.GetUserNameWithoutDomain());

        /// <summary>
        /// If the user exists it logs the date and time of log in
        /// </summary>
        public void SaveUserLoginTime()
        {
            using (new NHibernateSessionContext())
            {
                // Get current user to set the log-in time
                var currentUser = GetCurrent();

                // if the user is not registered yet, it simply returns
                if (currentUser == null)
                {
                    return;
                }

                //currentUser.Logon();
               // UpdateUser(currentUser);
            }
        }

        private static ValidationResults ValidateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Validate;
        }

        /// <summary>
        /// Updates an user.
        /// </summary>
        /// <param name="user">A user.</param>
        private void UpdateUser(User user)
        {
            using (var scope = new TransactionScope())
            {
                UserRepository.Merge(user);
                scope.Complete();
            }

            Log.Info($"User has been updated (UserName: {user.Username}).");
        }
        public User GetUserBtUserId(Guid userId) => UserRepository.GetUserByUserId(userId);
        //public List<POLineDetails> GetListOfPODatas()
        //{
        //    return UserRepository.BulkInsert
        //}
    }
}
