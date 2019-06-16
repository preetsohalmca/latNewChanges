using System.Linq;
using Volvo.NVS.Persistence.NHibernate.Repositories;
using Volvo.LAT.UserDomain.DomainLayer.Entities;
using Volvo.LAT.UserDomain.DomainLayer.RepositoryInterfaces;
using Volvo.LAT.UserDomain.DomainLayer.Projections.Management;
using System.Collections.Generic;
using System;

namespace Volvo.LAT.UserDomain.InfrastructureLayer.Repositories
{
    /// <summary>
    /// The user repository.
    /// </summary>
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        /// <inheritdoc/>
        public User FindByUserName(string userName, bool includeDeleted) =>
            includeDeleted ? FindAllByUserName(userName) : FindActiveByUserName(userName);

        private User FindActiveByUserName(string userName) =>
            Session.QueryOver<User>().Where(user => user.Username == userName ).List<User>().FirstOrDefault();

        private User FindAllByUserName(string userName) =>
            Session.QueryOver<User>().Where(user => user.Username == userName).List<User>().FirstOrDefault();

        /// <summary>
        /// Determines if a user with a given name is registered in the system.
        /// </summary>
        /// <param name="userName">A name of the user to be checked.</param>
        /// <returns>True is user is registered and not marked as deleted.</returns>
        public bool IsUserRegistered(string userName) =>
            Session.QueryOver<User>().Where(user => user.Username == userName ).RowCount() == 1;

        public User GetUserByUserId(Guid UserId) =>
           Session.QueryOver<User>().Where(user => user.UserID == UserId).List<User>().FirstOrDefault();
        //public List<POLineDetails> GetListOfPODatas(string text)=>Session.QueryOver<PurchaseOrderLine>
    }
}
