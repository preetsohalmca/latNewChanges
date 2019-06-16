using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Volvo.NVS.Persistence.NHibernate.Entities;
using Volvo.LAT.UserDomain.DomainLayer.Common.Utilities.GenericExceptions;

namespace Volvo.LAT.UserDomain.DomainLayer.Entities
{
    [HasSelfValidation]
    public class User : GenericEntity
    {
        private const string ValidateExceptionMessageStarter = "Validation errors include: ";
        private const string ValidateExceptionMessageSeparator = ". ";

        // NEWMODEL UserName must be unique. Validation should be implemented and displayed on the UI ("user already exists").
        //[NotNullValidator]
        //public virtual string UserName { get; set; }

        //public virtual string FirstName { get; set; }

        //public virtual string LastName { get; set; }


        //[SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
        //public virtual DateTime? LatestLogin { get; set; }

        //public virtual Role Role { get; set; }

        //public virtual bool IsDeleted { get; set; }

        public virtual Guid UserID { get; set; }

        public virtual Guid RoleID { get; set; }

        public virtual UserRole UserRole { get; set; }

        public virtual string Username { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime TimeStamp { get; set; }


        /// <summary>
        /// Creates a guest user.
        /// </summary>
        /// <param name="userName">A user name.</param>
        /// <returns>A guest user.</returns>
        //public static User CreateGuest(string userName)
        //{
        //    return new User
        //    {
        //        Username = userName,
        //        Role = Role.Guest
        //    };
        //}

        public virtual bool IsInRole(UserRole userRole) => this.UserRole == userRole;

        ///// <summary>
        ///// Marks a user as deleted.
        ///// </summary>
        //public virtual void MarkAsDeleted() => IsDeleted = true;

        ///// <summary>
        ///// Marks the current user as active (non-deleted).
        ///// </summary>
        //public virtual void MarkAsActive() => IsDeleted = false;

        ///// <summary>
        ///// Sets information about the latest login on the current user.
        ///// </summary>
        //[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Logon")]
        //public virtual void Logon() => LatestLogin = DateTime.Now;

        #region Equality

        //protected bool Equals(User other) => base.Equals(other) && string.Equals(UserName, other.UserName)
        //    && string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName)
        //    && IsDeleted == other.IsDeleted && Role.Equals(other.Role);

        protected bool Equals(User other)
        {
            return base.Equals(other) && string.Equals(Username, other.Username) && UserRole.RoleID.Equals(other.UserRole.RoleID);
        }

        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        public override bool Equals(object obj) =>
            !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) || Equals(obj as User));

        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        var hashCode = base.GetHashCode();
        //        hashCode = (hashCode * 397) ^ (UserName?.GetHashCode() ?? 0);
        //        hashCode = (hashCode * 397) ^ (FirstName?.GetHashCode() ?? 0);
        //        hashCode = (hashCode * 397) ^ (LastName?.GetHashCode() ?? 0);
        //        hashCode = (hashCode * 397) ^ IsDeleted.GetHashCode();
        //        hashCode = (hashCode * 397) ^ Role.GetHashCode();
        //        return hashCode;
        //    }
        //}

        public static bool operator ==(User left, User right) => Equals(left, right);

        public static bool operator !=(User left, User right) => !Equals(left, right);

        #endregion

        #region Validation

        /// <summary>
        /// Validates the entire entity and throws an exception if not valid
        /// </summary>
        public virtual void ValidateElseThrowOnError()
        {
            if (IsValid)
            {
                return;
            }

            var messageString = new StringBuilder(ValidateExceptionMessageStarter);
            var i = 1;
            foreach (var result in ValidationResults)
            {
                messageString.Append(i + ValidateExceptionMessageSeparator + result.Message);
                i++;
            }

            throw new UserValidationException(messageString.ToString());
        }

        #endregion
    }
}
