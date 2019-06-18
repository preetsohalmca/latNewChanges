using System;
using System.Diagnostics.CodeAnalysis;
using Volvo.NVS.Security.Attributes;

namespace Volvo.LAT.UserDomain.DomainLayer.Attributes
{
    /// <summary>
    /// Checks the user authorization according to the given (requested) operation and user itself (if available).
    /// </summary>
    /// <remarks>
    /// See also the <see cref="IUserCheckAccess"/> interface.
    /// </remarks>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class UserCheckAccessAttribute : CheckAccessAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCheckAccessAttribute"/> class.
        /// </summary>
        /// <param name="operation">The operation for which the access should be verified.</param>
        public UserCheckAccessAttribute(string operation)
            : base(operation, typeof(IUserCheckAccess))
        {
        }
    }
}
