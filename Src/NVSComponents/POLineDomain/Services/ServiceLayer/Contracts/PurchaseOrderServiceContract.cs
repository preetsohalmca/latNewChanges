namespace Volvo.LAT.POLineDomain.ServiceLayer.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using NHibernate;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;

    [ContractClassFor(typeof(IPurchaseOrderService))]
    public abstract class PurchaseOrderServiceContract : IPurchaseOrderService
    {
        public PurchaseOrder GetPurchaseOrderByEBDNumber(string EBDNumber) => default(PurchaseOrder);

        /// <summary>
        /// Get Purchase Order Detail
        /// </summary>
        /// <returns>purchase order </returns>
        ////public POLine GetPurchaseOrderDetail(Guid purchaseOrderId) => default(POLine);
        //public POLine GetPurchaseOrderDetail(string purchaseOrderId) => default(POLine);

        public List<CustomModel> GetPurchaseOrderDetail(string purchaseOrderId) => default(List<CustomModel>);

        public PurchaseOrderLineFromEbd GetPurchaseOrderLineEBD(Guid PurchaseOrderId) => default(PurchaseOrderLineFromEbd);

        public PurchaseOrder Save(PurchaseOrder purchaseOrder) => default(PurchaseOrder);

        public bool SavePurchaserName(string purchaserName, string poEbd_Id) => default(bool);

    }
}
