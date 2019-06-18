using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class ActivityTypeMap : ClassMapping<ActivityType>
    {
        public ActivityTypeMap()
        {

            Table("ActivityType");

            Schema("dbo"); 
            Lazy(true);
            Id(x => x.ActivityTypeId, map => { map.Column("ActivityType_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.IsDefault, map => map.NotNullable(true));
            Property(x => x.TimeStamp, map => map.NotNullable(true));
          
        }
    }
}

