namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    using System.Data.SQLite;
    using FluentNHibernate.MappingModel;
    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Volvo.LAT.POLineDomain.DomainLayer.Entities;

    public class PurchaseOrderMap : ClassMapping<PurchaseOrder>
    {
        public PurchaseOrderMap()
        {
            this.Table("PurchaseOrder");
            this.Schema("dbo");
            
            this.Lazy(true);
            this.Id(x => x.PurchaseOrderId, map =>
            {
                map.Column("PurchaseOrder_ID");
                map.Generator(Generators.Assigned);
            });
         
            this.Property(x => x.TimeStamp, map => map.NotNullable(true));
            this.Property(x => x.PoNumber, map => map.NotNullable(true));
            this.Property(x => x.Currency, map => map.NotNullable(true));
            this.Property(x => x.VendorName);
            this.Property(x => x.PurchaseOrderName, map => map.NotNullable(true));
            this.Property(x => x.OrderDate, map => map.NotNullable(true));
            this.Property(x => x.InvoiceNumber);
            this.Property(x => x.PurchaserName);
         
            this.Property(x => x.Comments);

            this.ManyToOne(x => x.Owner, map =>
            {
                map.Column("Owner_ID");
                map.Cascade(Cascade.None);
                map.Lazy(LazyRelation.NoLazy);
                map.ForeignKey("Owner_ID");
            });
        }
    }
}

