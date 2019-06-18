using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.NVS.Persistence.Attributes;
using Volvo.LAT.UserDomain.DomainLayer.Entities;
using Volvo.LAT.UserDomain.DomainLayer.Projections.Management;

#pragma warning disable SA1642 // Constructor summary documentation must begin with standard text

namespace Volvo.LAT.UserDomain.InfrastructureLayer.ProjectionMappings
{
    /// <summary>
    /// Provides mapping of <see cref="UserManagement"/> projections.
    /// </summary>
    [ProjectionMapping]
    public class UserManagementMap : ClassMapping<UserManagement>
    {
        /// <summary>
        /// Initializes an instance of the user management projection mapping and defines a map.
        /// </summary>
        public UserManagementMap()
        {
            Mutable(false);

            Table("Users");
            Schema("[dbo]");

            Property(x => x.Username);
            Property(x => x.Name);
            Property(x => x.RoleID);
           // Property(x => x.LatestLogin);
            Property(x => x.UserRole.Name, m => m.Type<EnumStringType<Role>>());
           // Property(x => x.IsDeleted);
        }
    }
}

#pragma warning restore SA1642 // Constructor summary documentation must begin with standard text
