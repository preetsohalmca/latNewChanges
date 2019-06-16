using System;

namespace Volvo.LAT.UserDomain.ServiceLayer
{
    /// <summary>
    /// Enables management of claims cache for the current user.
    /// </summary>
    public interface IClaimsProviderCache
    {
        /// <summary>
        /// Removes, clears the claims cache for the current user.
        /// </summary>
        /// <param name="timestamp">
        /// The current, host related timestamp. The timestamp must be provided in the same way as the current authentication handler
        /// is providing it. When not given the cache will always be removed for the current principal. If given the cache will only
        /// be removed if it has been created under a different timestamp value. See also the CompleteClaims method.
        /// </param>
        void RemoveCacheForCurrentUser(DateTime? timestamp);

        /// <summary>
        /// Removes, clears the claims cache for the current user.
        /// </summary>
        void RemoveCacheForCurrentUser();

        /// <summary>
        /// Removes, clears the claims cache for all users according to the given expiration time.
        /// </summary>
        /// <param name="expirationMinutes">
        /// The cache expiration time in minutes.
        /// </param>
        void RemoveCacheForAll(int expirationMinutes);

        /// <summary>
        /// Determines if the cache is established for a user with a given name.
        /// </summary>
        /// <param name="userName">
        /// The user name for which the cache should be verified.
        /// </param>
        /// <returns>
        /// True if a user with a given name contains cached roles.
        /// </returns>
        bool IsCache(string userName);
    }
}
