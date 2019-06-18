using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class AppMap : ClassMapping<App>
    {
        public AppMap()
        {

            Table("App"); 

            Schema("dbo");
            Lazy(true);
            Id(x => x.AppId, map => { map.Column("App_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.TimeStamp, map => map.NotNullable(true));
            Property(x => x.ApmId, map => map.NotNullable(true));
            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.DeliveryManager);
            Property(x => x.DeliveryUnit);
            Property(x => x.Porfolio);
            Property(x => x.SubPortfolio);
     
        }
    }
}

