using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class POLineMap : ClassMapping<POLine>
    {
        public POLineMap()
        {
            this.Table("PurchaseOrderLine");
            this.Schema("dbo");
            this.Lazy(true);
            this.Id(x => x.PurchaseOrderLineId, map =>
            {
                map.Column("PurchaseOrderLine_ID");
                map.Generator(Generators.Assigned);
            });
            this.Property(x => x.TimeStamp, map => map.NotNullable(true));
            this.Property(x => x.EbdNumber, map => map.NotNullable(true));
            this.Property(x => x.PoLine, map => map.NotNullable(true));
            this.Property(x => x.ReplacedWithPo);
            this.Property(x => x.Software);
            this.Property(x => x.Remark);
            this.Property(x => x.AcOrWbs);
            this.Property(x => x.ApprovedBy);
            this.Property(x => x.UnApprovedBy);
            this.Property(x => x.ApprovedDate);
            this.Property(x => x.UnApprovedDate);
            this.Property(x => x.OwnerName);
            this.Property(x => x.LastChangeBy, map => map.NotNullable(true));
            this.Property(x => x.LastChangeDate, map => map.NotNullable(true));
            this.Property(x => x.IsSplitted, map => map.NotNullable(true));
            this.Property(x => x.Renewal, map => map.NotNullable(true));
            this.Property(x => x.ProductNumber);
            this.Property(x => x.InvoiceNumber);
            this.Property(x => x.RenewalOrderPurchaseLine);
            this.Property(x => x.IsNewOrder, map => map.NotNullable(true));
            this.ManyToOne(x => x.App, map =>
            {
                map.Column("App_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
            });

            this.ManyToOne(x => x.ActivityType, map =>
            {
                map.Column("ActivityType_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
            });

            this.ManyToOne(x => x.CostType, map =>
            {
                map.Column("CostType_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
                map.ForeignKey("CostType_ID");
            });

            this.ManyToOne(x => x.Product, map =>
            {
                map.Column("Product_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
            });

            this.ManyToOne(x => x.ContractType, map =>
            {
                map.Column("ContractType_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
                map.ForeignKey("ContractType_ID");
            });

            this.ManyToOne(x => x.StatusPo, map =>
            {
                map.Column("StatusPo_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
                map.ForeignKey("StatusPo_ID");
            });

            this.ManyToOne(x => x.PurchaseOrderLineFromEbd, map =>
            {
                map.Column("PurchaseOrderLineFromEbd_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.All);
                map.ForeignKey("PurchaseOrderLineFromEbd_ID");
                map.Update(true);
            });

            this.ManyToOne(x => x.CostCenter, map =>
            {
                map.Column("CostCenter_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
                map.ForeignKey("CostCenter_ID");
            });

            this.ManyToOne(x => x.Currency, map =>
            {
                map.Column("Currency_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
                map.ForeignKey("Currency_ID");
            });

            this.ManyToOne(x => x.WbsElement, map =>
            {
                map.Column("WbsElement_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
                map.ForeignKey("WbsElement_ID");
            });
            //this.ManyToOne(x => x.AssignmentCode, map =>
            //{
            //    map.Column("PurchaseOrderLine_ID");
            //    map.Lazy(LazyRelation.NoLazy);
            //    map.Cascade(Cascade.None);
            //    map.ForeignKey("PurchaseOrderLine_ID");
            //});
        }
    }
}
