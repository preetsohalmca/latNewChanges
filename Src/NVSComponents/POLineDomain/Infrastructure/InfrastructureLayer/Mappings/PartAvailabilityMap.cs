using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class PartAvailabilityMap : ClassMapping<PartAvailability>
    {
        public PartAvailabilityMap()
        {
            Table("PartAvailabilities");

            Schema("Part");

            Lazy(true);

            Id(x => x.Id, m =>
               m.Generator(Generators.HighLow, g => g.Params(new
               {
                   table = "HiLoValues",
                   column = "NextHigh",
                   max_lo = 100,
                   where = $"EntityName = '{typeof(PartAvailability).Name.ToLowerInvariant()}'"
               })));

            Property(x => x.Balance, map => map.NotNullable(true));
            Property(x => x.Date, map => map.NotNullable(true));

            // NHibernate Version is used when you want to implement Optimistic concurrency control.
            Version(x => x.Version, m => m.Column("Version"));
        }
    }
}
