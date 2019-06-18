namespace Volvo.LAT.MVCWebUIComponent.Models.Shared
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class POLineModel
    {
        public POLineModel()
        {
            contractType = new ContractTypeModel();
        }

        public virtual System.Guid InvoiceMasterId { get; set; }

        [Required]
        public virtual DateTime TimeStamp { get; set; }

        [Required]
        public virtual string EbdNumber { get; set; }

        [Required]
        public virtual int PoLine { get; set; }
    
        public virtual string ReplacedWithPo { get; set; }

        public virtual string Software { get; set; }

        public virtual string Remark { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual DateTime? DelayedDate { get; set; }

        public virtual string ContactPerson { get; set; }

        public virtual string AcOrWbs { get; set; }

        public virtual decimal? SplitLineItemAmount { get; set; }

        public virtual System.Nullable<System.Guid> ApprovedBy { get; set; }

        public virtual System.Nullable<System.Guid> UnApprovedBy { get; set; }

        public virtual DateTime? ApprovedDate { get; set; }

        public virtual DateTime? UnApprovedDate { get; set; }

        public virtual string OwnerName { get; set; }

        [Required]
        public virtual System.Guid LastChangeBy { get; set; }

        [Required]
        public virtual DateTime LastChangeDate { get; set; }

        [Required]
        public virtual bool IsSplitted { get; set; }

        public virtual string PurchaserName { get; set; }

        public virtual string RequestorName { get; set; }

        public virtual DateTime? SpeededDate { get; set; }

        [Required]
        public virtual bool Renewal { get; set; }

        public virtual int? ExchangeRateYear { get; set; }

        public virtual string ProductNumber { get; set; }

        public virtual string InvoiceNumber { get; set; }

        public virtual ContractTypeModel contractType { get; set; }
        [Required]
        public virtual bool IsNewOrder { get; set; }

        public override bool Equals(object obj) => base.Equals(obj) && this.Equals(obj as POLineModel);


        public override int GetHashCode() => base.GetHashCode();
    }
}
