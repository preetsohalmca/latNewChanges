namespace Volvo.LAT.MVCWebUIComponent
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using App_LocalResources;
    using App_Start.Logging;
    using AutoMapper;
    using Common.Helpers;
    using Controllers;
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
    using Microsoft.Practices.ServiceLocation;
    using NVS.Core.Unity;
    using NVS.Logging;
    using NVS.Security.SystemWeb.Extensions;
    using NVS.Utilities.Web.Localization;
    using Routes;
    using Security;
    using Unity;
    using UserDomain.ServiceLayer;
    using BundleConfig = Bundling.BundleConfig;

#pragma warning disable SA1649 // File name must match first type name
    public class MvcApplication : HttpApplication
#pragma warning restore SA1649 // File name must match first type name
    {
        /// <summary>
        /// Gets the auxiliary property which returns an instance of a ILocalizationHelper resolved by Unity container.
        /// </summary>
        private static ILocalizationHelper LocalizationHelper
        {
            get { return Container.Resolve<ILocalizationHelper>(); }
        }

        private static IUserService UserService
        {
            get { return Container.Resolve<IUserService>(); }
        }

        /// <summary>
        /// Gets the auxiliary property which returns the current Http Context wrapped in a new HttpContextWrapper object.
        /// </summary>
        private HttpContextBase HttpContextBase
        {
            get { return new HttpContextWrapper(Context); }
        }

        /// <summary>
        /// Handle the application start event and configure the complete application domain.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            // This is a temporary fix to avoid an error in NVS Integration
            // TODO: Remove this line after fixing NVS Integration
            ServiceLocator.SetLocatorProvider(() => null);

            // Configure the unity container
            ContainerConfig.Configure();

            // Configure Logging for Application
            NVSLogging.Configure();

            // Configure the Security Library
            SecurityConfig.Configure();

            // Register all the areas in the MVC application
            AreaRegistration.RegisterAllAreas();

            // Configure the bundling and minification
            BundleConfig.Configure();

            // Configure AutoMapper registering all the Profiles
            AutoMapperConfig.Configure();

            // Register Web Api routes
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            // Register all the routes
            RoutesConfig.RegisterRoutes(RouteTable.Routes);

            // Configure the persistence library.
            // If you want to configure the Persistence library by code, uncomment the
            // method call below and follow the instructions found in the method.
            // PersistenceConfig.Configure();
        }

        /// <summary>
        /// Initialize the session for the current user.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        protected void Session_Start(object sender, EventArgs e)
        {
            var sessionHelper = Container.Resolve<IPosSessionHelper>();
            sessionHelper.InitializeOnStart();

            // Save user's datetime log in
            UserService.SaveUserLoginTime();
        }

        /// <summary>
        /// Acquires the current state (for example, session state) that is associated with the current request.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            // We set the culture in the AcquireRequestState so the MVC binders will also execute under
            // the set and correct culture. This method is called before the MVC binders run.
            // The culture itself is stored in cookies so earlier events would also not function as
            // there would be no possibility to set the cookie. There would also be no session yet.
            LocalizationHelper.SetCulture(HttpContextBase);
        }

        /// <summary>
        /// Retrieves the http error code if an exception is the http exception.
        /// </summary>
        /// <param name="exception">The exception to be examined and from which the error code should be returned.</param>
        /// <returns>The http error code is exception is the http exception or the internal server error code.</returns>
        private static HttpStatusCode GetHttpErrorCode(Exception exception)
        {
            HttpException httpException = exception as HttpException;
            if (httpException != null)
            {
                return (HttpStatusCode)httpException.GetHttpCode();
            }

            return HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Replace an original exception into the one which can be passed into the error view.
        /// </summary>
        /// <param name="exception">The original exception object.</param>
        /// <returns>The replaced exception object.</returns>
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private static Exception ReplaceException(Exception exception)
        {
            Exception exceptionToThrow;

            switch (GetHttpErrorCode(exception))
            {
                case HttpStatusCode.NotFound:
                    exceptionToThrow = new Exception(CommonResource.Error_404Message);
                    break;

                default:
                    var errorId = Guid.NewGuid();
                    Log.Error("ErrorId: " + errorId.ToString(), exception);
                    exceptionToThrow = new Exception(string.Format(CommonResource.Error_500Message, errorId.ToString()));
                    break;
            }

            return exceptionToThrow;
        }

        /// <summary>
        /// Write the error view into the <see cref="HttpResponse"/>.
        /// </summary>
        /// <param name="application">The current http application object.</param>
        /// <param name="exception">The exception object with the current application error.</param>
        /// <param name="actionName">The Error controller action to be executed.</param>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static void WriteErrorView(HttpApplication application, Exception exception, string actionName)
        {
            const string controllerName = "Error";

            var controller = new ErrorController
            {
                ViewData = { Model = new HandleErrorInfo(exception, controllerName, actionName) }
            };

            var routeData = new RouteData();
            routeData.Values["controller"] = controllerName;
            routeData.Values["action"] = actionName;

            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(application.Context), routeData));
            application.CompleteRequest();
        }

        /// <summary>
        /// Verifies if the exception is of critical type, which prevents the application from running correctly.
        /// </summary>
        /// <param name="exception">The exception to be verified.</param>
        /// <returns>True if the exception is of critical type. False, otherwise</returns>
        private static bool IsImportantException(Exception exception)
        {
            return exception.InnerException is System.Data.SqlClient.SqlException;
        }

        /// <summary>
        /// Gets the proper error action name to be called, depending on the Exception type.
        /// </summary>
        /// <param name="exception">The exception to be verified</param>
        /// <returns>The action name to be called.</returns>
        private static string GetErrorActionFromException(Exception exception)
        {
            return IsImportantException(exception) ? "ErrorImportantResource" : "Error";
        }

        /// <summary>
        /// Write the not authorized view into the response.
        /// </summary>
        /// <param name="application">The current http application object.</param>
        /// <param name="exception">The current authorization related exception.</param>
        private static void WriteNotAuthorized(HttpApplication application, Exception exception)
        {
            WriteErrorView(application, exception, "NotAuthorized");
        }

        /// <summary>
        /// Write the error view into the response.
        /// </summary>
        /// <param name="application">The current http application object.</param>
        /// <param name="exception">The current authorization related exception.</param>
        private static void WriteError(HttpApplication application, Exception exception)
        {
            var actionName = GetErrorActionFromException(exception);

            WriteErrorView(application, exception, actionName);
        }

        /// <summary>
        /// Handle the application error redirecting into the error page.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;

            // Get information about the exception detected for the current request
            // At this stage we know it is not authorization related exception
            var exception = Server.GetLastError();

            // Handle the authorization related error in the unified way
            // Use the extension method given by the Security library
            if (this.HandleAuthorizationError(WriteNotAuthorized))
            {
                Log.Error("Access is denied", exception);

                // The not authorized exception has been handled
                return;
            }

            // Make sure the cache is disabled for the currently executing request
            application.Context.DisableCache();

            // Clear all the errors collected so far
            application.Context.ClearError();

            // Clear the current response and write a new one. For any other error handling
            // (other than authorization) the status code is alway 500 (InternalServerError)
            HttpResponse response = application.Response;
            response.Clear();
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.TrySkipIisCustomErrors = true;

            var friendlyException = ReplaceException(exception);

            // For Ajax request we are only retuning the error code and the friendly message into the client
            // and so the Ajax on error event handler can be called on the client side
            // For non Ajax requests we are writing the view error into the response
            if (!application.Request.IsAjaxRequest())
            {
                WriteError(application, friendlyException);
            }
            else
            {
                response.Write(friendlyException.Message);
            }
        }
    }
}
