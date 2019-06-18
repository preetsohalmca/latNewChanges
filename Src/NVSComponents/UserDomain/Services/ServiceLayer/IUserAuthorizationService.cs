using Volvo.NVS.Security.Exceptions;

namespace Volvo.LAT.UserDomain.ServiceLayer
{
    /// <summary>
    /// Defines a contract for the order authorization service exposing authorization related actions
    /// out of the order domain boundaries.
    /// </summary>
    public interface IUserAuthorizationService
    {
        /// <summary>
        /// Check access, authorization for the current identity (user), the given user record id and the requested operation.
        /// </summary>
        /// <param name="operation">The requested operation.</param>
        /// <param name="userName">The user name for which access should be verified.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        bool CheckAccess(string operation, string userName);

        /// <summary>
        /// Check access, authorization for the current identity (user) and the requested operation.
        /// </summary>
        /// <param name="operation">The requested operation.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        bool CheckAccess(string operation);

        /// <summary>
        /// Ensures that a current user can read and manage other applications users.
        /// Throws the <see cref="NotAuthorizedException"/> if not access should be granted.
        /// </summary>
        /// <exception cref="NotAuthorizedException">Thrown when a user has not access into the overall user management.</exception>
        void EnsureCanManageUsers();
    }
}
