using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class InvoicingReportMap : ClassMapping<InvoicingReport>
    {
        public InvoicingReportMap()
        {
            Table("InvoicingReport");
            Schema("dbo"); 
            Lazy(true);
            Id(x => x.InvoicingReportID, map => { map.Column("InvoicingReport_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.Year, map => map.NotNullable(true));
            Property(x => x.Month, map => map.NotNullable(true));
            Property(x => x.Xml, map => { map.NotNullable(true); map.Type(NHibernateUtil.StringClob); });
            Property(x => x.TimeStamp, map => map.NotNullable(true));          
        }
    }
}

