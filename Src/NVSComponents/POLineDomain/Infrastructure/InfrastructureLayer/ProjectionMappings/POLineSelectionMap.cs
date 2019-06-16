using NHibernate.Mapping.ByCode.Conformist;
using Volvo.NVS.Persistence.Attributes;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.ProjectionMappings
{
    [ProjectionMapping]
    public class POLineSelectionMap : ClassMapping<POLineSelection>
    {
        public POLineSelectionMap()
        {
            Mutable(false);

            Table("PurchaseOrderLine");
            Schema("dbo");

            this.Property(x => x.PoLine);
            this.Property(x => x.StartDate);
            this.Property(x => x.EndDate);
            this.Property(x => x.PoNumber);
            this.Property(x => x.RequesterName);

            this.Property(x => x.OwnerName);
        }
    }
}
