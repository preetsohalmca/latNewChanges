using System;
using System.Diagnostics.CodeAnalysis;
using Volvo.LAT.UserDomain.DomainLayer.Entities;

#pragma warning disable SA1623 // Property summary documentation must match accessors

namespace Volvo.LAT.UserDomain.DomainLayer.Projections.Management
{
    /// <summary>
    /// Projects user information for user management purposes.
    /// </summary>
    public class UserManagement
    {
        ///// <summary>
        ///// A user name.
        ///// </summary>
        //public virtual string UserName { get; set; }

        ///// <summary>
        ///// A user first name.
        ///// </summary>
        //public virtual string FirstName { get; set; }

        ///// <summary>
        ///// A user last name.
        ///// </summary>
        //public virtual string LastName { get; set; }

        ///// <summary>
        ///// A time saying when a user has logged in for the last time.
        ///// </summary>
        //[SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
        //public virtual DateTime? LatestLogin { get; set; }

        ///// <summary>
        ///// A current user role.
        ///// </summary>
        //public virtual Role Role { get; set; }

        ///// <summary>
        ///// Gets or sets a flag telling if a user is marked as deleted.
        ///// </summary>
        //public virtual bool IsDeleted { get; set; }
        public virtual Guid UserID { get; set; }

        public virtual Guid RoleID { get; set; }

        public virtual UserRole UserRole { get; set; }

        public virtual string Username { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime TimeStamp { get; set; }
    }

   
}

#pragma warning restore SA1623 // Property summary documentation must match accessors
