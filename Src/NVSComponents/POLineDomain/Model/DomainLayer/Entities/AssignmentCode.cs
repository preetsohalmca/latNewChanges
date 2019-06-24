using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volvo.LAT.PartDomain.DomainLayer.Entities
{
   public class AssignmentCode
    {
        public virtual Guid AssignmentCodeID { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual DateTime? DelayedDate { get; set; }
        public virtual decimal? SplitLineItemAmount { get; set; }
        public virtual DateTime? EarlierPaymentDate { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string RequestorName { get; set; }
        public virtual int? ExchangeRateYear { get; set; }
        public virtual System.Guid PurchaseOrderLineId { get; set; }
        public virtual decimal MonthlyRate { get; set; }


        public virtual int PoLine { get; set; }
        public virtual string AmpId { get; set; }
        public virtual string Applcation { get; set; }
        public virtual string AcOrWBS { get; set; }
    }
}
