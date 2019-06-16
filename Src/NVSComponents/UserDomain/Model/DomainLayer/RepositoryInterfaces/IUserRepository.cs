using Volvo.NVS.Persistence.Repositories;
using Volvo.LAT.UserDomain.DomainLayer.Entities;
using Volvo.LAT.UserDomain.DomainLayer.Projections.Management;
using System.Collections.Generic;
using System;

namespace Volvo.LAT.UserDomain.DomainLayer.RepositoryInterfaces
{
    /// <summary>
    /// Defines the interface for the repository of users.
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Finds the user by the user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="includeDeleted">A flag telling a user marked as removed should also be returned.</param>
        /// <returns>The user with the matching user name.</returns>
        User FindByUserName(string userName, bool includeDeleted);

        /// <summary>
        /// Determines if a user with a given name is registered in the system.
        /// </summary>
        /// <param name="userName">A name of the user to be checked.</param>
        /// <returns>True is user is registered and not marked as deleted.</returns>
        bool IsUserRegistered(string userName);
        //List<POLineDetails> GetListOfPODatas(string text="");
        User GetUserByUserId(Guid UserId);
    }
}
