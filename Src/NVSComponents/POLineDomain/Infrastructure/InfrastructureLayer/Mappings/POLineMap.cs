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
            Table("PurchaseOrderLine");

            Schema("dbo");

            Lazy(true);
            Id(x => x.PurchaseOrderLineId, map => { map.Column("PurchaseOrderLine_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.TimeStamp, map => map.NotNullable(true));
            Property(x => x.EbdNumber, map => map.NotNullable(true));
            Property(x => x.PoLine, map => map.NotNullable(true));
            Property(x => x.ReplacedWithPo);
            Property(x => x.Software);
            Property(x => x.Remark);
            Property(x => x.StartDate);
            Property(x => x.EndDate);
            Property(x => x.DelayedDate);
            Property(x => x.ContactPerson);
            Property(x => x.AcOrWbs);
            Property(x => x.SplitLineItemAmount);
            Property(x => x.ApprovedBy);
            Property(x => x.UnApprovedBy);
            Property(x => x.ApprovedDate);
            Property(x => x.UnApprovedDate);
            Property(x => x.OwnerName);
            Property(x => x.LastChangeBy, map => map.NotNullable(true));
            Property(x => x.LastChangeDate, map => map.NotNullable(true));
            Property(x => x.IsSplitted, map => map.NotNullable(true));
            //Property(x => x.PurchaserName);
            Property(x => x.RequestorName);
            Property(x => x.EarlierPaymentDate);
            Property(x => x.Renewal, map => map.NotNullable(true));
            Property(x => x.ExchangeRateYear);
            Property(x => x.ProductNumber);
            Property(x => x.InvoiceNumber);
            Property(x => x.RenewalOrderPurchaseLine);
            Property(x => x.MonthlyRate);

            //Property(x => x.Comments);

            /// Property(x => x.ActivityTypeId);
            Property(x => x.IsNewOrder, map => map.NotNullable(true));
            //ManyToOne(x => x.StatusPo_ID);

            //ManyToOne(x => x.contractType,
            //    map =>
            //    {
            //        map.Column("ContractType_ID");
            //        map.Cascade(Cascade.None);
            //    });
            //ManyToOne(x => x.OwnerName,
            //    map =>
            //    {
            //        map.Column("Owner_ID");
            //        map.Cascade(Cascade.None);
            //        map.Class(typeof(Owner));
            //    });

           ManyToOne(x => x.App, map => { map.Column("App_ID"); map.Lazy(LazyRelation.NoLazy); map.Cascade(Cascade.None);});

           ManyToOne(x => x.ActivityType, map => { map.Column("ActivityType_ID"); map.Lazy(LazyRelation.NoLazy); map.Cascade(Cascade.None); });

            ManyToOne(x => x.CostType, map =>
            {
                map.Column("CostType_ID");
                map.Lazy(LazyRelation.NoLazy); map.Cascade(Cascade.None); map.ForeignKey("CostType_ID");
            });

           ManyToOne(x => x.Product, map => { map.Column("Product_ID"); map.Lazy(LazyRelation.NoLazy); map.Cascade(Cascade.None);  });

            ManyToOne(x => x.ContractType, map =>
            {
                map.Column("ContractType_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
                map.ForeignKey("ContractType_ID");
            });

            ManyToOne(x => x.StatusPo, map =>
            {
                map.Column("StatusPo_ID");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.None);
                map.ForeignKey("StatusPo_ID");
            });

            //ManyToOne(x => x.CostType, map =>
            //{
            //    map.Column("CostType_ID");
            //    map.Lazy(LazyRelation.NoLazy);
            //});

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

        }
    }
}
