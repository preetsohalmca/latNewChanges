using System;
using System.Collections.Generic;
using Volvo.NVS.Core.Diagnostics.Annotations;
using Volvo.LAT.UserDomain.DomainLayer.Entities;

namespace Volvo.LAT.UserDomain.DomainLayer
{
    /// <summary>
    /// Represents the cached claim roles for a single identity, user.
    /// </summary>
    internal class UserRolesCache
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRolesCache"/> class.
        /// </summary>
        /// <param name="roles">The current, resolved list of roles.</param>
        /// <param name="timestamp">The current, host related timestamp under which the cache has been created.</param>
        public UserRolesCache([NotNull] IEnumerable<UserRole> roles, DateTime timestamp)
        {
            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }

            if (timestamp.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("The timestamp must be in UTC");
            }

            Roles = roles;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Gets the currently cached collection of user roles.
        /// </summary>
        public IEnumerable<UserRole> Roles { get; }

        /// <summary>
        /// Gets the host related timestamp under which the cache has been created.
        /// </summary>
        public DateTime Timestamp { get; }
    }
}
