using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class PurchaseOrderLineFromEbdMap : ClassMapping<PurchaseOrderLineFromEbd>
    {
        public PurchaseOrderLineFromEbdMap()
        {

            Table("PurchaseOrderLineFromEbd");

            Schema("dbo");
            Lazy(true);
            Id(x => x.PurchaseOrderLineFromEbdId, map => { map.Column("PurchaseOrderLineFromEbd_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.TimeStamp, map => map.NotNullable(true));
            Property(x => x.EbdReceiveStatusDescription);
            Property(x => x.LineItemDescription, map => map.NotNullable(true));
            Property(x => x.OrderAmount, map => map.NotNullable(true));
            Property(x => x.CreationDate, map => map.NotNullable(true));
            Property(x => x.SpendType);
            //Property(x => x.CostCenter);
            Property(x => x.CompanyId);
            Property(x => x.ShortDescription);
            Property(x => x.GeographicalSite);
            Property(x => x.LowestBorg);
            Property(x => x.RequesterEmail);
            Property(x => x.PurchaserName);
            Property(x => x.FunctionalApproverName);
            Property(x => x.ParmaNbr);
            Property(x => x.OrderedQuantity, map => map.NotNullable(true));
            Property(x => x.ReceivedQuantity, map => map.NotNullable(true));
            Property(x => x.ContractStartDate, map => map.NotNullable(true));
            Property(x => x.ContractEndDate, map => map.NotNullable(true));
            Property(x => x.LicenseType);
            Property(x => x.SoftwareName);
            Property(x => x.PoLine, map => map.NotNullable(true));
            ManyToOne(x => x.PurchaseOrder, map =>
            {
                map.Column("PurchaseOrder_ID");
                //map.PropertyRef("PurchaseOrderId");
                map.Lazy(LazyRelation.NoLazy);
                map.Cascade(Cascade.All); map.ForeignKey("PurchaseOrder_ID");
            });
            //ManyToOne(x => x.CostCenter, map =>
            //{
            //    map.Column("CostCenter_ID");
            //    map.Lazy(LazyRelation.NoLazy);
            //    map.Cascade(Cascade.None);
            //});

        }
    }
}

