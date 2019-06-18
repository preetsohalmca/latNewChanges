using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Practices.Unity;
using Volvo.NVS.Core.Unity;
using Volvo.NVS.Persistence.NHibernate.Web.SessionHandling;
using Volvo.NVS.Utilities.Web.Filters;
using Volvo.NVS.Utilities.Web.Localization;
using Volvo.NVS.Utilities.Web.Messaging;
using Volvo.NVS.Utilities.Web.Models;
using Volvo.LAT.MVCWebUIComponent.App_LocalResources;
using Volvo.LAT.MVCWebUIComponent.Common.Helpers;
using Volvo.LAT.MVCWebUIComponent.Models.Shared;
using Volvo.LAT.MVCWebUIComponent.Models.Views;
using Volvo.LAT.UserDomain.ServiceLayer;
using UserDomainEntities = Volvo.LAT.UserDomain.DomainLayer.Entities;

namespace Volvo.LAT.MVCWebUIComponent.Controllers
{
    /// <summary>
    /// The controller managing users.
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        [InjectionConstructor]
        public UserController()
            : this(
                  Container.Resolve<ILocalizationHelper>(),
                  Container.Resolve<IThemesHelper>(),
                  Container.Resolve<IBundlingHelper>(),
                  Container.Resolve<IUserService>(),
                  Container.Resolve<IClaimsProviderCache>(),
                  null, // flash messenger 
                  Container.Resolve<IUserAuthorizationService>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="localizationHelper">The localization helper.</param>
        /// <param name="themesHelper">The themes helper.</param>
        /// <param name="bundlingHelper">The bundling helper.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="claimsProviderCache">The claims provider cache.</param>
        /// <param name="flashMessenger">The flash messenger.</param>
        /// <param name="orderService">The order service.</param>
        /// <param name="userAuthorizationService">A user authorization service to be used by the controller.</param>
        public UserController(
            ILocalizationHelper localizationHelper,
            IThemesHelper themesHelper,
            IBundlingHelper bundlingHelper,
            IUserService userService,
            IClaimsProviderCache claimsProviderCache,
            IFlashMessenger flashMessenger,
            IUserAuthorizationService userAuthorizationService)
            : base(localizationHelper, themesHelper, bundlingHelper)
        {
            UserService = userService;
            UserAuthorizationService = userAuthorizationService;
            ClaimsProviderCache = claimsProviderCache;
            FlashMessenger = flashMessenger ?? Messenger;
        }

        /// <summary>
        /// Gets or sets the user service to be used by the controller.
        /// </summary>
        protected IUserService UserService { get; set; }

        /// <summary>
        /// Gets or sets the user authorization service to be used by the controller.
        /// </summary>
        protected IUserAuthorizationService UserAuthorizationService { get; set; }

        /// <summary>
        /// Gets or sets the claims provider cache.
        /// </summary>
        private IClaimsProviderCache ClaimsProviderCache { get; set; }

        /// <summary>
        /// Gets or sets the flash messenger or provides the already resolved one.
        /// </summary>
        protected IFlashMessenger FlashMessenger { get; set; }

        /// <summary>
        /// The current user attribute - to store the user object
        /// </summary>
        private UserDomainEntities.User currentUser;

        /// <summary>
        /// Gets a currentUser or provides the already resolved one.
        /// This property was created to allow us to call GetCurrentUser once in the execution
        /// </summary>
        private UserDomainEntities.User CurrentUser => currentUser ?? (currentUser = UserService.GetCurrent());

        public ActionResult NotAuthorized() => RedirectToAction("NotAuthorized", "Error");

        public ActionResult ManageUsers()
        {
            // We do not want to go into the manage users view at all if we know that a user won't be able to manage identities at all.
            // For that we ask the user authorization service for an access check. The method may throw not authorization when no access is granted.
            UserAuthorizationService.EnsureCanManageUsers();

            return View();
        }

        [NHibernateMvcSessionContext]
        public ActionResult AddUser()
        {
            // creates a new user object to be added to the database
            var newUser = new UserDomainEntities.User();

            // creates the presentation model
            var model = CreatePresentationModel(newUser);

            return View("AddEditUser", model);
        }

        [NHibernateMvcSessionContext]
        public ActionResult EditUser(string userName)
        {
            // If editing own id it goes to my profile
            if (CheckIfIsMyProfile(userName))
            {
                return RedirectToAction("MyProfile");
            }

            // loads the user to be edited
            var user = UserService.GetUser(userName);

            // creates the presentation model
            var model = CreatePresentationModel(user);

            return View("AddEditUser", model);
        }

        [NHibernateMvcSessionContext]
        public ActionResult MyProfile()
        {
            return View("AddEditUser", CreatePresentationModel(CurrentUser));
        }

        [HttpPost]
        [FormAction]
        [NHibernateMvcSessionContext]
        public ActionResult UpdateUser(bool isNewUser, UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (!SaveUser(isNewUser, user))
            {
                // Something wrong happened, let's stay in the same view and show the errors to the user
                return View("AddEditUser", new UserPresentationModel { IsNewUser = isNewUser, User = user });
            }

            // User updated successfully.
            FlashMessenger.AppendMessage(string.Format(UserResource.User_UpdateUser_SaveSuccess, user.Username));

            // Remove the claims (role) cache for the current user so that if the user role has been changed in this update,
            // the cache will be recreated with the new role assigned.
            ClaimsProviderCache.RemoveCacheForCurrentUser();

            return CheckIfIsMyProfile(user.Username) ? RedirectToAction("MyProfile") : RedirectToAction("ManageUsers");
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userName">A name of an user to be deleted.</param>
        /// <returns>An action result.</returns>
        [HttpPost]
        [FormAction]
        [NHibernateMvcSessionContext]
        public ActionResult DeleteUser(string userName)
        {
            var success = true;
            var domainUser = UserService.GetUser(userName);

            if (domainUser != null)
            {

                //var results = UserService.DeleteUser(domainUser, OrderService.UserHasAnyOrder(domainUser.Id));
                //success = results.IsValid;
                if (success)
                {
                    FlashMessenger.AppendMessage(string.Format(UserResource.User_DeleteUser_Success, userName));
                }
                else
                {
                    //foreach (var validationResult in results)
                    //{
                    //    FlashMessenger.AppendMessage(validationResult.Message, MessageType.Error);
                    //}
                }
            }

            // Remove the claims(role) cache for the current user so that if the user role has been changed in this update,
            // the cache will be recreated with the new role assigned.
            ClaimsProviderCache.RemoveCacheForCurrentUser();

            return new JsonResult
            {
                Data = new RequestResult
                {
                    Success = success,
                    ErrorMessages = FlashMessenger.GetErrorMessages(),
                    WarningMessages = FlashMessenger.GetWarningMessages(),
                    InfoMessages = FlashMessenger.GetInfoMessages()
                }
            };
        }

        /// <summary>
        /// Provides all the user roles for drop downs.
        /// </summary>
        /// <returns>An action result.</returns>
        [HttpGet]
        public JsonResult GetRoles() => Json(RolesHelper.GetAllRolesLocalized(), JsonRequestBehavior.AllowGet);

        /// <summary>
        /// Creates a new user or updates the existing one.
        /// </summary>
        /// <param name="isNewUser">Indicates whether new user should be saved.</param>
        /// <param name="domainUser">A source user from which data should be taken and used in the update procedure.</param>
        /// <returns>True if successfully updated and no errors are detected.</returns>
        private bool SaveUser(bool isNewUser, UserModel domainUser)
        {
            if (domainUser == null)
            {
                throw new ArgumentNullException("domainUser");
            }

            // Try to get an existing user entity.
            var user = UserService.GetUser(domainUser.Username, true);

            if (isNewUser != (user == null))
            {
                if (isNewUser)
                {
                    FlashMessenger.AppendMessage(UserResource.User_alreadyExists, MessageType.Error);
                }
                else
                {
                    FlashMessenger.AppendMessage(UserResource.User_doesnotExists, MessageType.Error);
                }

                return false;
            }

            // Map the received user data into a brand new user or into the existing user entity object.
            user = user == null ? Mapper.Map<UserModel, UserDomainEntities.User>(domainUser) : Mapper.Map(domainUser, user);

            // Create a new user or update the existing one.
            var results = user.IsTransient() ? UserService.CreateUser(user) : UserService.SaveUser(user);

            foreach (var validationResult in results)
            {
                FlashMessenger.AppendMessage(validationResult.Message, MessageType.Error);
            }

            return results.IsValid;
        }

        /// <summary>
        /// Provides data for the user grid.
        /// </summary>
        /// <param name="request">A request object with all grid parameters.</param>
        /// <returns>An action result.</returns>
        [NHibernateMvcSessionContext]
        public virtual ActionResult UserGridRead([DataSourceRequest] DataSourceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            DataSourceResult result = UserService
                .FindUserManagementAsQueryable()
                .ToDataSourceResult(request, user => Mapper.Map<UserModel>(user));

            return Json(result);
        }

        #region Auxiliary methods

        /// <summary>
        /// Prepare the presentation model
        /// </summary>
        /// <param name="user">User that is going to be edited</param>
        /// <returns>model to the view</returns>
        private UserPresentationModel CreatePresentationModel(UserDomainEntities.User user)
        {
            var userModel = Mapper.Map<UserModel>(user);

            var model = new UserPresentationModel()
            {
                User = userModel,
                IsMyProfile = CheckIfIsMyProfile(user.Username),
                IsUserAdmin = UserService.IsCurrentUserAdmin(),
                IsNewUser = user.IsTransient()
            };

            return model;
        }

        /// <summary>
        /// Verifies if the user name is the same as the current user
        /// </summary>
        /// <param name="userName">User's name</param>
        /// <returns>If the user is the same</returns>
        public virtual bool CheckIfIsMyProfile(string userName) => userName == CurrentUser.Username;

        #endregion
    }
}