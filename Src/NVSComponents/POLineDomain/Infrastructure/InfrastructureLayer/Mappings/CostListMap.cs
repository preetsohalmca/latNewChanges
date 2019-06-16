using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class CostListMap : ClassMapping<CostList>
    {
        public CostListMap()
        {
            Table("CostList");
            Schema("dbo"); 
            Lazy(true);
            Id(x => x.CostListId, map => { map.Column("CostList_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.Cost, map => map.NotNullable(true));
            Property(x => x.IsDeleted);
            Property(x => x.Date);
            Property(x => x.PoLineId, map => map.NotNullable(true));
            Property(x => x.PoNumber);
        }
    }
}

