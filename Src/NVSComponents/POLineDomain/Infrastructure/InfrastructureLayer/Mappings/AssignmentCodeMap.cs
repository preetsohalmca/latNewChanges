namespace Volvo.LAT.PartDomain.InfrastructureLayer.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using Volvo.LAT.PartDomain.DomainLayer.Entities;

    public class AssignmentCodeMap : ClassMapping<AssignmentCode>
    {
        public AssignmentCodeMap()
        {
            this.Table("AssignmentCode");
            this.Schema("dbo");
            this.Lazy(true);
            this.Id(x => x.AssignmentCodeID, map =>
            {
                map.Column("AssignmentCode_ID");
                map.Generator(Generators.Assigned);
            });
            
            this.Property(x => x.StartDate);
            this.Property(x => x.EndDate);
            this.Property(x => x.DelayedDate);
            this.Property(x => x.ContactPerson);
            this.Property(x => x.SplitLineItemAmount);
            this.Property(x => x.RequestorName);
            this.Property(x => x.EarlierPaymentDate);
            this.Property(x => x.ExchangeRateYear);
            this.Property(x => x.MonthlyRate);
            //this.Property(x => x.AmpId);
            //this.Property(x => x.AcOrWBS);
            //this.Property(x => x.PoLine);
            //this.Property(x => x.Applcation);
            this.Property(x => x.PurchaseOrderLineId,map=> {
                map.Column("PurchaseOrderLine_ID");
            });
        }
    }
}
