using Volvo.NVS.Security.Authorization;
using Volvo.LAT.UserDomain.DomainLayer.Entities;

namespace Volvo.LAT.UserDomain.DomainLayer
{
    /// <summary>
    /// Defines a contract for the order related authorization checks.
    /// Those checks are run the by authorization related attributes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface defines a contract providing the CheckAccess methods definitions. The CheckAccess methods can be
    /// executed by the Security Library CheckAccess attributes.
    /// </para>
    /// <para>
    /// As multiple CheckAccess methods are defined on the single interface the 'multiple' security library binder (accepting
    /// more than one CheckAccess method on a single interface) must be configured. E.g. <see cref="MultiCheckAccessNameTypeBinder"/>.
    /// </para>
    /// </remarks>
    public interface IUserCheckAccess
    {
        /// <summary>
        /// Check access for the current operation (taken from the context).
        /// </summary>
        /// <param name="context">The current authorization context.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        bool CheckAccess(CheckAccessContext context);

        /// <summary>
        /// Check access for the current operation (taken from the context) and the order (taken from the order id).
        /// </summary>
        /// <param name="context">The current authorization context.</param>
        /// <param name="userName">The user name for which the access should be checked.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        bool CheckAccess(CheckAccessContext context, string userName);

        /// <summary>
        /// Check access for the current operation (taken from the context) and the order.
        /// </summary>
        /// <param name="context">The current authorization context.</param>
        /// <param name="user">The user for which the access should be checked.</param>
        /// <returns>True if access should be granted and false is access should be denied.</returns>
        bool CheckAccess(CheckAccessContext context, User user);
    }
}
