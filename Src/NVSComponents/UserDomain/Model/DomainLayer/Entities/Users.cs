using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Volvo.NVS.Persistence.NHibernate.Entities;
using Volvo.LAT.UserDomain.DomainLayer.Common.Utilities.GenericExceptions;
namespace Volvo.LAT.UserDomain.DomainLayer.Entities
{
    public class Users : GenericEntity
    {
        public virtual Guid UserID { get; set; }

        public virtual Guid RoleID { get; set; }

        public virtual UserRole UserRole { get; set; }

        public virtual string Username { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime TimeStamp { get; set; }
    }
}
