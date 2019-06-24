#pragma warning disable SA1623 // Property summary documentation must match accessors

namespace Volvo.LAT.POLineDomain.DomainLayer
{
    using UserDomain.ServiceLayer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.ProjectionRepositoryInterfaces;
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;
    using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
    using Volvo.LAT.POLineDomain.ServiceLayer;
    using NHibernate;
    using Volvo.LAT.PartDomain.DomainLayer.Entities;

    public class PurchaseOrderService : IPurchaseOrderService
    {

        protected IPurchaseOrderRepository PurchaseOrderRepository { get; }

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            this.PurchaseOrderRepository = purchaseOrderRepository;
        }

        /// <summary>
        /// Get Details service
        /// </summary>
        /// <param name="purchaseOrderId">guid purchaseOrderId</param>
        /// <returns> P order</returns>
        ////public POLine GetPurchaseOrderDetail(Guid purchaseOrderId) => this.PurchaseOrderRepository.GetPurchaseOrderDetail(purchaseOrderId);
        //public POLine GetPurchaseOrderDetail(string purchaseOrderId) => this.PurchaseOrderRepository.GetPurchaseOrderDetail(purchaseOrderId);
        public List<CustomModel> GetPurchaseOrderDetail(string purchaseOrderId) => this.PurchaseOrderRepository.GetPurchaseOrderDetail(purchaseOrderId);

        public bool SavePurchaserName(string purchaserName, string poEbd_Id) => this.PurchaseOrderRepository.SavePurchaserName(purchaserName, poEbd_Id);

        public PurchaseOrder GetPurchaseOrderByEBDNumber(string EBDNumber) => this.PurchaseOrderRepository.GetPurchaseOrderByEBDNumber(EBDNumber);

        public List<PurchaseOrderLineFromEbd> GetPurchaseOrderLineEBD(Guid PurchaseOrderId) => this.PurchaseOrderRepository.GetPurchaseOrderLineEBD(PurchaseOrderId);

        public bool SaveAssignmentCodeDetails(AssignmentCode assignmentCode) => this.PurchaseOrderRepository.SaveAssignmentCodeDetails(assignmentCode);

        public AssignmentCode GetAssignmentCode(string purchaseorderlineid) => this.PurchaseOrderRepository.GetAssignmentCode(purchaseorderlineid);
    }
}

#pragma warning restore SA1623 // Property summary documentation must match accessors