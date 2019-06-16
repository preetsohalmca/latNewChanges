using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volvo.LAT.MVCWebUIComponent.Models.ViewModel
{
    public class POLineSaveViewModel
    {
        public string PurchaseOrderLineId { get; set; }
        public string OwnerId { get; set; }
        public string StatusPoId { get; set; }
        public string Remark { get; set; }
        public string ContractStartDate { get; set; }
        public string AcOrWbs { get; set; }
        public string ContractEndDate { get; set; }
        public string ContractTypeId { get; set; }
        public string AppId { get; set; }
        public string EarlierPaymentDate { get; set; }
        public int? ExchangeRateYear { get; set; }
        public bool Renewal { get; set; }
        public string ActivityTypeId { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceNumberHeader { get; set; }
        public string ContactPerson { get; set; }
        public string ProductNumber { get; set; }
        public string ProductId { get; set; }
        public string CostTypeId { get; set; }
        public string Comments { get; set; }
        public string DelayedPaymentDate { get; set; }
        public decimal? RechargeAmount { get; set; }
        public string RenewalOrderPurchaseLine { get; set; }
        public string RequestorName { get; set; }
        public string PurchaserName { get; set; }
    }
}