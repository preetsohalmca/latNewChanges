using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class PurchaseOrderLineMap11 //: ClassMapping<PurchaseOrderline>
    {
        //public PurchaseOrderLineMap()
        //{

        //    Table("PurchaseOrderLine");

        //    Schema("dbo");
        //    Lazy(true);
        //    Id(x => x.PurchaseOrderLineID, map => { map.Column("PurchaseOrderLine_ID"); map.Generator(Generators.Assigned); });
        //    Property(x => x.TimeStamp, map => map.NotNullable(true));
        //    Property(x => x.EbdNumber, map => map.NotNullable(true));
        //    Property(x => x.PoLine, map => map.NotNullable(true));
        //    Property(x => x.ReplacedWithPo);
        //    Property(x => x.Software);
        //    Property(x => x.Remark);
        //    Property(x => x.StartDate);
        //    Property(x => x.EndDate);
        //    Property(x => x.DelayedDate);
        //    Property(x => x.ContactPerson);
        //    Property(x => x.AcOrWbs);
        //    Property(x => x.SplitLineItemAmount);
        //    Property(x => x.PurchaserName);
        //    Property(x => x.ApprovedBy);
        //    Property(x => x.UnApprovedBy);
        //    Property(x => x.ApprovedDate);
        //    Property(x => x.UnApprovedDate);
        //    Property(x => x.OwnerName);
        //    Property(x => x.LastChangeBy, map => map.NotNullable(true));
        //    Property(x => x.LastChangeDate, map => map.NotNullable(true));
        //    Property(x => x.IsSplitted);
        //    Property(x => x.PurchaserName);
        //    Property(x => x.RequestorName);
        //    Property(x => x.EarlierPaymentDate);
        //    Property(x => x.Renewal);
        //    Property(x => x.ExchangeRateYear);
        //    Property(x => x.ProductNumber);
        //    Property(x => x.InvoiceNumber);
        //    Property(x => x.IsNewOrder);

        //    ManyToOne(x => x.ContractType, map =>
        //    {
        //        map.Column("ContractType_ID");
        //        map.PropertyRef("ContractTypeId");
        //        map.Cascade(Cascade.None);
        //    });

        //    ManyToOne(x => x.App, map =>
        //    {
        //        map.Column("App_ID");
        //        map.PropertyRef("AppId");
        //        map.Cascade(Cascade.None);
        //    });

        //    ManyToOne(x => x.purchaseOrderLineFromEbd, map =>
        //    {
        //        map.Column("PurchaseOrderLineFromEbd_ID");
        //        map.PropertyRef("PurchaseOrderLineFromEbdId");
        //        map.Cascade(Cascade.None);
        //    });

        //    ManyToOne(x => x.StatusPo, map =>
        //    {
        //        map.Column("StatusPo_ID");
        //        map.PropertyRef("StatusPoId");
        //        map.Cascade(Cascade.None);
        //    });

        //    ManyToOne(x => x.ActivityType, map =>
        //    {
        //        map.Column("ActivityType_ID");
        //        map.PropertyRef("ActivityTypeId");
        //        map.Cascade(Cascade.None);
        //    });

        //    ManyToOne(x => x.ActivityType, map =>
        //    {
        //        map.Column("ActivityType_ID");
        //        map.PropertyRef("ActivityTypeId");
        //        map.Cascade(Cascade.None);
        //    });


        //    ManyToOne(x => x.CostType, map =>
        //    {
        //        map.Column("CostType_ID");
        //        map.PropertyRef("CostTypeId");
        //        map.Cascade(Cascade.None);
        //    });

        //    ManyToOne(x => x.Product, map =>
        //    {
        //        map.Column("Product_ID");
        //        map.PropertyRef("ProductId");
        //        map.Cascade(Cascade.None);
        //    });

        //}
    }
}

