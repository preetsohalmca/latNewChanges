﻿namespace Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces
{
    using System;
    using System.Collections.Generic;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.NVS.Persistence.Repositories;

    /// <summary>
    /// Defines the interface for the repository of orders.
    /// </summary>
    public interface IPurchaseOrderRepository : IGenericRepository<POLine, long>
    {
        ////POLine GetPurchaseOrderDetail(Guid purchaseOrderId);
        //POLine GetPurchaseOrderDetail(string purchaseOrderId);
        List<CustomModel> GetPurchaseOrderDetail(string purchaseOrderId);
        bool SavePurchaserName(string purchaserName, string poEbd_Id);
        PurchaseOrder GetPurchaseOrderByEBDNumber(string EBDNumber);
        PurchaseOrderLineFromEbd GetPurchaseOrderLineEBD(Guid PurchaseOrderId);
        PurchaseOrder Save(PurchaseOrder purchaseOrder);
    }
}