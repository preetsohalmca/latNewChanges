using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class StatusPoMap : ClassMapping<StatusPo>
    {
        public StatusPoMap()
        {

            Table("StatusPo");

            Schema("dbo");
            Lazy(true);
            Id(x => x.StatusPoId, map => { map.Column("StatusPo_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.TimeStamp, map => map.NotNullable(true));
  
        }
    }
}

