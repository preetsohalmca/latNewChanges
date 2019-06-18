using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Mappings
{
    public class ContractTypeMap : ClassMapping<ContractType>
    {
        public ContractTypeMap()
        {

            Table("ContractType"); 

            Schema("dbo"); 
            Lazy(true);
            Id(x => x.ContractTypeId, map => { map.Column("ContractType_ID"); map.Generator(Generators.Assigned); });
            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.TimeStamp, map => map.NotNullable(true));
           
        }
    }

   

    //public class modelTestMap : ClassMapping<modelTest>
    //{
    //    public modelTestMap()
    //    {

    //        Table("GetTopPurchaseOrderDetail");

    //        Schema("dbo");
    //        Lazy(true);
    //        //Id(x => x.ContractTypeId, map => { map.Column("ContractType_ID"); map.Generator(Generators.Assigned); });
    //        //Property(x => x.Name, map => map.NotNullable(true));
    //        //Property(x => x.TimeStamp, map => map.NotNullable(true));

    //        Property(x => x.EarlierPaymentDate);
    //        Property(x => x.DelayedDate);
    //        Property(x => x.Activitytype);
    //        Property(x => x.WBS);
    //        Property(x => x.SoftwareName);
    //        Property(x => x.Lastmodifyname);
    //        Property(x => x.Lastmodifydate);
    //        Property(x => x.CostCenter);
    //        Property(x => x.Renewal);
    //        Property(x => x.SDU);
    //        Property(x => x.ContactPerson);

    //        Property(x => x.OrderAmount);
    //        Property(x => x.OrderamountinSEK);
    //        Property(x => x.Numberofchargableamount);
    //        Property(x => x.LineItemDescription);
    //        Property(x => x.ExchangeRateYear);
    //        Property(x => x.ShortDescription);
    //        Property(x => x.InvoiceNumber);
    //        Property(x => x.APMID);
    //        Property(x => x.ProductNumber);
    //        Property(x => x.CostType);
    //        Property(x => x.Name);
    //        Property(x => x.RequestorName);
    //        Property(x => x.OwnerName);
    //        Property(x => x.PoNumber);
    //        Property(x => x.PoLine);
    //        Property(x => x.ContractStartDate);
    //        Property(x => x.ContractEndDate);
    //        Property(x => x.StartDate);
    //        Property(x => x.EndDate);
    //        Property(x => x.Owner_ID);
    //        Property(x => x.App_ID);

    //}
    //}
}

