using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.UserDomain.DomainLayer.Entities;

namespace Volvo.LAT.UserDomain.InfrastructureLayer.Mappings
{
    public class UsersMap : ClassMapping<Users>
    {
        public UsersMap()
        {
            //this.Table("[User]");
            //this.Schema("[dbo]");
            //this.Id(x => x.UserID, map => {
            //    map.Column("User_ID");
            //    map.Generator(Generators.Assigned);
            //});
            //this.Property(x => x.Name);
            //this.Property(x => x.TimeStamp);

            //this.ManyToOne(x => x.UserRole, map =>
            //{
            //    map.Column("Role_ID");
            //    map.Lazy(LazyRelation.NoLazy);
            //    map.Cascade(Cascade.None);
            //});
        }
    }
}
