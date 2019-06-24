using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Volvo.LAT.PartDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using Volvo.LAT.POLineDomain.ServiceLayer.Contracts;

namespace Volvo.LAT.POLineDomain.ServiceLayer
{
    /// <summary>
    /// Service interface for POLineDomain component
    /// </summary>
    [ContractClass(typeof(PurchaseOrderServiceContract))]
    public interface IPurchaseOrderService
    {
        ////POLine GetPurchaseOrderDetail(Guid purchaseOrderId);
        //POLine GetPurchaseOrderDetail(string purchaseOrderId);

        List<CustomModel> GetPurchaseOrderDetail(string purchaseOrderId);
        PurchaseOrder GetPurchaseOrderByEBDNumber(string EBDNumber);
        List<PurchaseOrderLineFromEbd> GetPurchaseOrderLineEBD(Guid PurchaseOrderId);
        bool SavePurchaserName(string purchaserName, string poEbd_Id);

        bool SaveAssignmentCodeDetails(AssignmentCode assignmentCode);
        AssignmentCode GetAssignmentCode(string purchaseorderlineid);
    }
}
