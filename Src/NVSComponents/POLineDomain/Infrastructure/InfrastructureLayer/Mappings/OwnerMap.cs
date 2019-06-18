using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class OwnerMap : ClassMapping<Owner>
    {
        public OwnerMap()
        {

            Table("Owner");

            Schema("dbo");
            Lazy(true);
            Id(x => x.OwnerId, map => { map.Column("Owner_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.TimeStamp, map => map.NotNullable(true));
            Property(x => x.Name);
        }
    }
}

