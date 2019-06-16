using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Volvo.LAT.UserDomain.DomainLayer.Entities;

namespace Volvo.LAT.MVCWebUIComponent.Models.Shared
{
    /// <summary>
    /// The UI specific application user model, entity.
    /// </summary>
    public class UserModel
    {
        public UserModel()
        {
            UserRole = new UserRole();
        }

        ///// <summary>
        ///// Gets or sets the user name (vcn id).
        ///// </summary>
        //[Required]
        //public string UserName { get; set; }

        ///// <summary>
        ///// Gets or sets the user's first name.
        ///// </summary>
        //public string FirstName { get; set; }

        ///// <summary>
        ///// Gets or sets the user's last name.
        ///// </summary>
        //public string LastName { get; set; }

        ///// <summary>
        ///// Gets or sets the user's latest login time.
        ///// </summary>
        //[SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
        //[DataType(DataType.Date)]
        //public DateTime? LatestLogin { get; set; }

        ///// <summary>
        ///// Gets or sets the role associated with this user.
        ///// </summary>
        //[Required]
        //public RoleModel Role { get; set; }
        public virtual Guid UserID { get; set; }

        public virtual Guid RoleID { get; set; }

        public virtual UserRole UserRole { get; set; }

        public virtual string Username { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime TimeStamp { get; set; }
    }
}