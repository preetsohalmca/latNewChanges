using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.PartDomain.InfrastructureLayer.Mappings
{
    public class DashboardMapping : ClassMapping<DashboardNewOrders>
    {
        public DashboardMapping()
        {
            Table("GetNewPurchaseOrders");
            Schema("dbo");
            Lazy(true);
            Id(x => x.EbdNumber);
            Property(x => x.StartDate);
            Property(x => x.VendorName);
        }
    }
}
