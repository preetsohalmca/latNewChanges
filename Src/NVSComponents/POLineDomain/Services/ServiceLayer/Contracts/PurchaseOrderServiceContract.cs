namespace Volvo.LAT.POLineDomain.ServiceLayer.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using NHibernate;
    using Volvo.LAT.PartDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;

    [ContractClassFor(typeof(IPurchaseOrderService))]
    public abstract class PurchaseOrderServiceContract : IPurchaseOrderService
    {
        public AssignmentCode GetAssignmentCode(string purchaseorderlineid) => default(AssignmentCode);

        public PurchaseOrder GetPurchaseOrderByEBDNumber(string EBDNumber) => default(PurchaseOrder);

        /// <summary>
        /// Get Purchase Order Detail
        /// </summary>
        /// <returns>purchase order </returns>
        ////public POLine GetPurchaseOrderDetail(Guid purchaseOrderId) => default(POLine);
        //public POLine GetPurchaseOrderDetail(string purchaseOrderId) => default(POLine);

        public List<CustomModel> GetPurchaseOrderDetail(string purchaseOrderId) => default(List<CustomModel>);

        public List<PurchaseOrderLineFromEbd> GetPurchaseOrderLineEBD(Guid PurchaseOrderId) => default(List<PurchaseOrderLineFromEbd>);

        public bool SaveAssignmentCodeDetails(AssignmentCode assignmentCode) => default(bool);

        public bool SavePurchaserName(string purchaserName, string poEbd_Id) => default(bool);

    }
}
