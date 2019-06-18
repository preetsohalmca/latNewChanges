using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Volvo.NVS.Core.Diagnostics.Annotations;
using Volvo.NVS.Persistence.NHibernate.SessionHandling;
using Volvo.NVS.Security.Claims;
using Volvo.LAT.UserDomain.DomainLayer.Entities;
using Volvo.LAT.UserDomain.ServiceLayer;

namespace Volvo.LAT.UserDomain.DomainLayer
{
    /// <summary>
    /// A user service implementing the claims related functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The service complete claims for all the users during the authentication process. The <see cref="CompleteClaims"/> method
    /// is called by the Security Library authentication handler. It adds additional claims into authenticated identities. For
    /// details check the Security Library documentation.
    /// </para>
    /// <para>
    /// The service manages the cache for user roles. The roles are added as claims into the identities but obtaining the roles
    /// is time consuming as we must read the roles from the database. As the <see cref="CompleteClaims"/> is called multiple times
    /// we resolve user roles once and take them from cache wherever possible.
    /// </para>
    /// <para>
    /// The cached list of roles must be removed for example: when the user assigement into roles changes or when a given expiriation
    /// time is exceeded. The removal of cache is managed by various methods available on the <see cref="IClaimsProviderCache"/>.
    /// </para>
    /// </remarks>
    public partial class UserService : IClaimsProvider, IClaimsProviderCache
    {
        /// <summary>
        /// Keep the cache for all the user roles (which will be added as role claims) so we do not have to ask the database
        /// for the same information multiple times which would slightly decrease the application performance.
        /// For every user name keep the collection of already detected role names (represented by the <see cref="UserRolesCache"/>)
        /// </summary>
        private static readonly ConcurrentDictionary<string, UserRolesCache> userRoles = new ConcurrentDictionary<string, UserRolesCache>();

        /// <summary>
        /// Complete claims for the already authenticated identity, user.
        /// Add all the user roles stored in the database as role claims.
        /// </summary>
        /// <param name="service">
        /// The current claims service giving access into the current identity.
        /// </param>
        /// <param name="contextTimestamp">
        /// The current host context related timestamp under which the claims completion is requested.
        /// The timestamp is given by the Security Library and depends on the configured and used authentication handler.
        /// </param>
        public void CompleteClaims([NotNull] IClaimsService service, DateTime contextTimestamp)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            // Use the user role cache so we do not have to contact database multiple times
            // It is necessary as the CompleteClaims method can be called many times (e.g. for
            // every web request when running the Web UI component)
            using (new NHibernateSessionContext())
            {
                var claimRoles = userRoles.GetOrAdd(service.UserName, name =>
                {
                    // Get current user, if there is no user in database a special guest user will be created.
                    var user = GetCurrent();
                    if(user!=null)
                    return new UserRolesCache(new List<UserRole> { user.UserRole }, contextTimestamp.ToUniversalTime());
                    return new UserRolesCache(new List<UserRole> { new UserRole() }, contextTimestamp.ToUniversalTime());
                });

                // Add roles into the current principal, identity object
                //service.AddRole(claimRoles.Roles.Select(r => r.Name.ToString()));
            }
        }

        /// <summary>
        /// Removes, clears the claims cache for the current user when a cache has been created under a different timestamp.
        /// </summary>
        /// <param name="timestamp">
        /// The current, host related timestamp. The timestamp must be provided in the same way as the current authentication handler
        /// is providing it. When not given the cache will always be removed for the current principal. If given the cache will only
        /// be removed if it has been created under a different timestamp value. See also the CompleteClaims method which is creating
        /// the cache and is receiving the timestamp value for the cache item.
        /// </param>
        public void RemoveCacheForCurrentUser(DateTime? timestamp)
        {
            // We remove for the current user so it is required
            if (!ClaimsService.IsPrincipal)
            {
                return;
            }

            var userName = ClaimsService.UserName;

            // Get the current value if that is present. If there is no current
            // value at all then there is also nothing to be removed from cache
            UserRolesCache cache;
            if (!userRoles.TryGetValue(userName, out cache))
            {
                return;
            }

            // When the current context timestamp is the same as the cache timestamp it means the cached
            // value has just been added and it should not be removed. For example: in web applications the
            // timestamp is unique for every web request. If cached timestamp is the same as the current one
            // it means the cache has been created under the web request on which we are about to remove it.
            // It may happen when the cache has been created before a new session has been started and then
            // we are staring session on trying to remove cache on every session start. Having this check
            // secures us from removal of just added cache when the request is still the same.
            if (timestamp.HasValue && timestamp.Value.ToUniversalTime() == cache.Timestamp)
            {
                return;
            }

            userRoles.TryRemove(ClaimsService.UserName, out cache);
        }

        /// <summary>
        /// Removes, clears the claims cache for the current user.
        /// </summary>
        public void RemoveCacheForCurrentUser() => RemoveCacheForCurrentUser(null);

        /// <summary>
        /// Removes, clears the claims cache for all users according to the given expiration time.
        /// </summary>
        /// <param name="expirationMinutes">
        /// The cache expiration time in minutes.
        /// </param>
        public void RemoveCacheForAll(int expirationMinutes)
        {
            var current = DateTime.UtcNow;
            foreach (var userName in userRoles
                .Where(item => (current - item.Value.Timestamp).TotalMinutes >= expirationMinutes)
                .Select(item => item.Key))
            {
                UserRolesCache removedCache;
                userRoles.TryRemove(userName, out removedCache);
            }
        }

        /// <summary>
        /// Determines if the cache is established for a user with a given name.
        /// </summary>
        /// <param name="userName">
        /// The user name for which the cache should be verified.
        /// </param>
        /// <returns>
        /// True if a user with a given name contains cached roles.
        /// </returns>
        public bool IsCache(string userName) => userRoles.ContainsKey(userName);
    }
}
