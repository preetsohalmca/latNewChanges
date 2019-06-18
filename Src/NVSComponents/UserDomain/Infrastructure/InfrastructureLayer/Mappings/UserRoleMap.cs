using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.UserDomain.DomainLayer.Entities;

namespace Volvo.LAT.UserDomain.InfrastructureLayer.Mappings
{
    public class UserRoleMap : ClassMapping<UserRole>
    {
        public UserRoleMap()
        {

            this.Table("UserRole");
            this.Schema("[dbo]");
            this.Id(x => x.RoleID, map => { map.Column("Role_ID");
                map.Generator(Generators.Assigned);
            });
            this.Property(x => x.Name);
            this.Property(x => x.TimeStamp);
        }
    }
}
