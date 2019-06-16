using System.Linq;
using Volvo.NVS.Persistence.NHibernate.Repositories;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
using System.Collections.Generic;
using System;
using NHibernate.Transform;


namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Repositories
{
    /// <summary>
    /// The Purchase Order Repository
    /// </summary>
    public class PurchaseOrderRepository : GenericRepository<POLine>, IPurchaseOrderRepository
    {
        public PurchaseOrder GetPurchaseOrderByEBDNumber(string EBDNumber)
        {
            var POrder= this.Session.QueryOver<PurchaseOrder>().Where(x => x.PoNumber == EBDNumber).List<PurchaseOrder>();
            if (POrder.Count() > 0)
            {
                return POrder.FirstOrDefault();
            }
            else
                return new PurchaseOrder();
        }

        public List<CustomModel> GetPurchaseOrderDetail(string purchaseOrderId)
        {
            List<CustomModel> customModelList = new List<CustomModel>();
            DateTime orderDate = DateTime.MinValue;
            string ebdCostCenter = string.Empty;
            string ebdOrderAmount = string.Empty;
            string ebdOrderamountinSEK = string.Empty;
            string ebdLineItemDescription = string.Empty;
            string ebdShortDescription = string.Empty;
            string ebdPoLine = string.Empty;
            string ebdContractStartDate = string.Empty;
            string ebdContractEndDate = string.Empty;
            string ebdVendorName = string.Empty;
            string owner_ID = string.Empty;
            string poNumber = string.Empty;
            string productId = string.Empty;
            string statusPoId = string.Empty;
            string aPMID = string.Empty;
            string sDU = string.Empty;
            string ownerName = string.Empty;
            string costTypeId = string.Empty;
            string costType = string.Empty;
            string activityTypeId = string.Empty;
            string contractTypeId = string.Empty;
            string purchaseOrderComment = string.Empty;
            string purchaseOrderName = string.Empty;
            string currency = string.Empty;
            string costCenterID = string.Empty;
            string costCenterName = string.Empty;
            string purchaserName = string.Empty;
            int numberofchargableamount = 0;
            string monthlyRate = string.Empty;
            var purchaseOrder = this.Session.CreateSQLQuery("exec GetTopPurchaseOrderDetail :purchaseOrderId")
                                .AddEntity(typeof(POLine))
                                .SetParameter("purchaseOrderId", purchaseOrderId)
                                .List<POLine>();
            foreach (var item in purchaseOrder)
            {
                if (item != null)
                {
                    CustomModel obj = null;
                    if (item.PurchaseOrderLineFromEbd != null)
                    {
                        ebdOrderAmount = Convert.ToString(item.PurchaseOrderLineFromEbd.OrderAmount);
                        ebdOrderamountinSEK = Convert.ToString(item.PurchaseOrderLineFromEbd.OrderAmount);
                        ebdLineItemDescription = item.PurchaseOrderLineFromEbd.LineItemDescription;
                        ebdShortDescription = item.PurchaseOrderLineFromEbd.ShortDescription;
                        ebdPoLine = Convert.ToString(item.PurchaseOrderLineFromEbd.PoLine);
                        ebdContractStartDate = Convert.ToString(item.PurchaseOrderLineFromEbd.ContractStartDate);
                        ebdContractEndDate = Convert.ToString(item.PurchaseOrderLineFromEbd.ContractEndDate);

                        if (item.PurchaseOrderLineFromEbd.PurchaseOrder != null)
                        {
                            owner_ID = Convert.ToString(item.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.OwnerId);
                            poNumber = item.PurchaseOrderLineFromEbd.PurchaseOrder.PoNumber;
                            currency = item.PurchaseOrderLineFromEbd.PurchaseOrder.Currency;
                            ebdVendorName = item.PurchaseOrderLineFromEbd.PurchaseOrder.VendorName;
                            orderDate = item.PurchaseOrderLineFromEbd.PurchaseOrder.OrderDate;
                            purchaseOrderComment = item.PurchaseOrderLineFromEbd.PurchaseOrder.Comments;
                            purchaseOrderName = item.PurchaseOrderLineFromEbd.PurchaseOrder.PurchaseOrderName;

                            if (item.PurchaseOrderLineFromEbd.PurchaseOrder.Owner != null)
                            {
                                ownerName = item.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name;
                            }
                        }
                    }

                    monthlyRate = (item.MonthlyRate > 0) ? Convert.ToString(item.MonthlyRate) : string.Empty;

                    if (item.App != null)
                    {
                        aPMID = item.App.Name;
                        sDU = item.App.DeliveryUnit;
                    }

                    if (item.CostType != null)
                    {
                        costTypeId = Convert.ToString(item.CostType.CostTypeId);
                        costType = item.CostType.Name;
                    }

                    if (item.Product != null)
                    {
                        productId = Convert.ToString(item.Product.ProductId);
                    }

                    if (item.ActivityType != null)
                    {
                        activityTypeId = Convert.ToString(item.ActivityType.ActivityTypeId);
                    }

                    if (item.StatusPo != null)
                    {
                        statusPoId = Convert.ToString(item.StatusPo.StatusPoId);
                    }

                    if (item.ContractType != null)
                    {
                        contractTypeId = Convert.ToString(item.ContractType.ContractTypeId);
                    }

                    if (item != null && item.CostCenter != null && item.CostCenter.CostCenterId != null)
                    {
                        costCenterID = Convert.ToString(item.CostCenter.CostCenterId);
                        costCenterName = item.CostCenter.FullName;
                    }
                    var wbs = Session.QueryOver<WbsElement>().Where(w => w.AssignmentCode == item.AcOrWbs).List().FirstOrDefault();

                    obj = new CustomModel
                    {
                        AcOrWbs = item.AcOrWbs,
                        APMID = wbs != null ? wbs.ApmId : string.Empty,
                        ApprovedDate = item.ApprovedDate,
                        App_ID = Convert.ToString(item.App.AppId),
                        Software = item.Software,
                        SoftwareName = item.Software,
                        ContactPerson = item.ContactPerson,
                        ContractEndDate = ebdContractEndDate,
                        ContractStartDate = ebdContractStartDate,
                        CostType = item.CostType.Name,
                        CostTypeId = Convert.ToString(item.CostType.CostTypeId),
                        Numberofchargableamount = numberofchargableamount,
                        OrderAmount = ebdOrderAmount,
                        OrderamountinSEK = ebdOrderAmount,
                        OwnerName = ownerName,
                        Owner_ID = owner_ID,
                        PoLine = Convert.ToInt32(ebdPoLine),
                        PoNumber = poNumber,
                        Remark = item.Remark,
                        Renewal = item.Renewal,
                        RequestorName = item.RequestorName,
                        ShortDescription = ebdShortDescription,
                        LineItemDescription = ebdLineItemDescription,
                        PurchaserName = item.PurchaseOrderLineFromEbd.PurchaseOrder.PurchaserName,
                        PurchaserOrderName = purchaseOrderName,
                        SDU = sDU,
                        EndDate = item.EndDate,
                        StartDate = item.StartDate,
                        Comments = purchaseOrderComment,
                        TimeStamp = item.TimeStamp,
                        WBS = wbs != null ? wbs.WbsElementId.ToString() : string.Empty,
                        DelayedDate = item.DelayedDate,
                        SplitLineItemAmount = item.SplitLineItemAmount.HasValue ? Math.Round(item.SplitLineItemAmount.Value, 2, MidpointRounding.AwayFromZero) : 0,
                        PurchaseOrderLineId = item.PurchaseOrderLineId,
                        UnApprovedDate = item.UnApprovedDate,
                        ReplacedWithPo = item.ReplacedWithPo,
                        IsSplitted = item.IsSplitted,
                        EbdNumber = item.EbdNumber,
                        ProductId = productId,
                        VendorName = ebdVendorName,
                        Currency = currency,
                        StatusPoid = statusPoId,
                        OrderDate = orderDate,
                        ContractTypeId = contractTypeId,
                        ActivityTypeId = Convert.ToString(item.ActivityType.ActivityTypeId),
                        ExchangeRateYear = item.ExchangeRateYear,
                        EarlierPaymentDate = item.EarlierPaymentDate,
                        LastChangeBy = item.LastChangeBy,
                        InvoiceNumber = item.InvoiceNumber,
                        InvoiceNumberHeader = item.PurchaseOrderLineFromEbd.PurchaseOrder.InvoiceNumber,
                        ProductNumber = item.ProductNumber,
                        LastChangeDate = item.LastChangeDate,
                        Lastmodifydate = item.LastChangeDate,
                        Lastmodifyname = Convert.ToString(item.LastChangeBy),
                        RenewalOrderPurchaseLine = item.RenewalOrderPurchaseLine,
                        CostCenterId = costCenterID,
                        CostCenterName = costCenterName,
                        MonthlyRate = monthlyRate
                    };
                    customModelList.Add(obj);
                }
            }

            return customModelList.OrderBy(x => x.PoLine).ToList();
        }

        public PurchaseOrderLineFromEbd GetPurchaseOrderLineEBD(Guid PurchaseOrderId)
        {
            var POrder = this.Session.QueryOver<PurchaseOrderLineFromEbd>().Where(x => x.PurchaseOrder.PurchaseOrderId == PurchaseOrderId).List<PurchaseOrderLineFromEbd>();
            if (POrder.Count() > 0)
            {
                return POrder.FirstOrDefault();
            }
            else
                return new PurchaseOrderLineFromEbd();
        }

        public PurchaseOrder Save(PurchaseOrder purchaseOrder)
        {
            using (var transaction = this.Session.BeginTransaction())
            {
                this.Session.Save(purchaseOrder);
                transaction.Commit();
            }
            return purchaseOrder;
        }

        //////public POLine GetPurchaseOrderDetail(Guid purchaseOrderId)
        ////public POLine GetPurchaseOrderDetail(string purchaseOrderId)
        //public List<CustomModel> GetPurchaseOrderDetail(string purchaseOrderId)
        //{
        //    List<CustomModel> customModelList = new List<CustomModel>();

        //    var ebdCostCenter = string.Empty;
        //    var ebdOrderAmount = string.Empty;
        //    var ebdOrderamountinSEK = string.Empty;
        //    var ebdLineItemDescription = string.Empty;
        //    var ebdShortDescription = string.Empty;
        //    var ebdPoLine = string.Empty;
        //    var ebdContractStartDate = string.Empty;
        //    var ebdContractEndDate = string.Empty;
        //    var ebdVendorName = string.Empty;


        //    var Numberofchargableamount = "12";

        //    var Owner_ID = string.Empty;
        //    var poNumber = string.Empty;

        //    var productId = string.Empty;
        //    var StatusPoId = string.Empty;


        //    //purchaseOrderId= new Guid("6EF636CB-8897-4FA7-969F-6A9D3B7937C9");
        //    var purchaseOrder = this.Session.CreateSQLQuery("exec GetTopPurchaseOrderDetail :purchaseOrderId")
        //                        .AddEntity(typeof(POLine))
        //                        .SetParameter("purchaseOrderId", purchaseOrderId)
        //                        .List<POLine>();


        //    var productOrderData = this.Session.QueryOver<Product>().List();
        //    var purchaseOrderData = this.Session.QueryOver<PurchaseOrder>().List();
        //    var OWNERData = this.Session.QueryOver<Owner>().List();
        //    var PurchaseOrderLineFromEbdData = this.Session.QueryOver<PurchaseOrderLineFromEbd>().List();
        //    var ContractTypeData = this.Session.QueryOver<ContractType>().List();
        //    var AppData = this.Session.QueryOver<App>().List();
        //    var CostTypeData = this.Session.QueryOver<CostType>().List();
        //    var contractTypeData = this.Session.QueryOver<ContractType>().List();
        //    var ActivityTypeData = this.Session.QueryOver<ActivityType>().List();
        //    var statusPData = this.Session.QueryOver<StatusPo>().List();
        //    var CostCenterData = this.Session.QueryOver<CostCenter>().List();

        //    var APMID = string.Empty;
        //    var SDU = string.Empty;

        //    var ownerName = string.Empty;

        //    var costTypeId = string.Empty;
        //    var costType = string.Empty;

        //    DateTime orderDate = DateTime.MinValue;

        //    var activityTypeId = string.Empty;
        //    var contractTypeId = string.Empty;
        //    var purchaseOrderComment = string.Empty;
        //    var purchaseOrderName = string.Empty;

        //    var currency = string.Empty;
        //    var costCenterID = string.Empty;
        //    var costCenterName = string.Empty;

        //    foreach (var item in purchaseOrder)
        //    {
        //        if (item != null)
        //        {
        //            CustomModel obj = null;

        //            var edbData = PurchaseOrderLineFromEbdData.FirstOrDefault(x => x.PurchaseOrderLineFromEbdId == item.PurchaseOrderLineFromEbd.PurchaseOrderLineFromEbdId);
        //            if (edbData != null)
        //            {
        //                //ebdCostCenter = edbData.CostCenter;
        //                ebdOrderAmount = Convert.ToString(edbData.OrderAmount);
        //                ebdOrderamountinSEK = Convert.ToString(edbData.OrderAmount);
        //                ebdLineItemDescription = edbData.LineItemDescription;
        //                ebdShortDescription = edbData.ShortDescription;
        //                ebdPoLine = Convert.ToString(edbData.PoLine);
        //                ebdContractStartDate = Convert.ToString(edbData.ContractStartDate);
        //                ebdContractEndDate = Convert.ToString(edbData.ContractEndDate);



        //                var pOrderData = purchaseOrderData.FirstOrDefault(x => x.PurchaseOrderId == edbData.PurchaseOrder.PurchaseOrderId);
        //                if (pOrderData != null)
        //                {
        //                    Owner_ID = Convert.ToString(pOrderData.Owner.OwnerId);
        //                    poNumber = pOrderData.PoNumber;
        //                    currency = pOrderData.Currency;
        //                    ebdVendorName = pOrderData.VendorName;
        //                    orderDate = pOrderData.OrderDate;
        //                    purchaseOrderComment = pOrderData.Comments;
        //                    purchaseOrderName = pOrderData.PurchaseOrderName;

        //                    var ownerData = OWNERData.FirstOrDefault(x => x.OwnerId == pOrderData.Owner.OwnerId);

        //                    if(ownerData != null)
        //                    {
        //                        ownerName = ownerData.Name;
        //                    }
        //                }
        //            }

        //            var appData = AppData.FirstOrDefault(x => x.AppId == item.App.AppId);
        //            if (appData != null)
        //            {
        //                APMID = appData.Name;
        //                SDU = appData.DeliveryUnit;
        //            }

        //            var coostTypeData = CostTypeData.FirstOrDefault(x => x.CostTypeId == item.CostType.CostTypeId);
        //            if (coostTypeData != null)
        //            {
        //                costTypeId = Convert.ToString(coostTypeData.CostTypeId);
        //                costType = coostTypeData.Name;
        //            }

        //            var productData = productOrderData.FirstOrDefault(x => x.ProductId == item.Product.ProductId);
        //            if (productData != null)
        //            {
        //                productId =Convert.ToString( productData.ProductId);
        //            }


        //            var activityData = ActivityTypeData.FirstOrDefault(x => x.ActivityTypeId == item.ActivityType.ActivityTypeId);
        //            if (activityData != null)
        //            {
        //                activityTypeId = Convert.ToString(activityData.ActivityTypeId);
        //            }

        //            var statusData = statusPData.FirstOrDefault(x => x.StatusPoId == item.StatusPo.StatusPoId);
        //            if (statusData != null)
        //            {
        //                StatusPoId = Convert.ToString(statusData.StatusPoId);
        //            }

        //            var cotractTypeData = contractTypeData.FirstOrDefault(x => x.ContractTypeId == item.ContractType.ContractTypeId);
        //            if (cotractTypeData != null)
        //            {
        //                contractTypeId = Convert.ToString(cotractTypeData.ContractTypeId);
        //            }

        //            //var cCData = CostCenterData.FirstOrDefault(x => x.CostCenterId == item.CostCenter.CostCenterId);
        //            if (item != null && item.CostCenter !=null && item.CostCenter.CostCenterId != null)
        //            {
        //                costCenterID = Convert.ToString(item.CostCenter.CostCenterId);
        //                costCenterName = item.CostCenter.Name;
        //            }

        //            obj = new CustomModel
        //            {
        //                AcOrWbs = item.AcOrWbs,
        //                //Activitytype = item.ActivityType;
        //                APMID = APMID,
        //                ApprovedDate = item.ApprovedDate,
        //                App_ID = Convert.ToString(item.App.AppId),
        //                Software = item.Software,
        //                SoftwareName = item.Software,
        //                ContactPerson = item.ContactPerson,
        //                ContractEndDate = ebdContractEndDate,
        //                ContractStartDate = ebdContractStartDate,
        //                //CostCenter = ebdCostCenter,
        //                CostType = item.CostType.Name,
        //                CostTypeId = Convert.ToString(item.CostType.CostTypeId),
        //                Numberofchargableamount = Numberofchargableamount,
        //                OrderAmount = ebdOrderAmount,
        //                OrderamountinSEK = ebdOrderAmount,
        //                OwnerName = ownerName,
        //                Owner_ID = Owner_ID,
        //                PoLine = Convert.ToInt32(ebdPoLine),
        //                PoNumber = poNumber,
        //                Remark = item.Remark,
        //                Renewal = item.Renewal,
        //                RequestorName = item.RequestorName,
        //                ShortDescription = ebdShortDescription,
        //                LineItemDescription = ebdLineItemDescription,
        //                PurchaserName = item.PurchaserName,
        //                PurchaserOrderName = purchaseOrderName,
        //                //ProductNumber = item.ProductNumber;
        //                SDU = SDU,
        //                EndDate = item.EndDate,
        //                StartDate = item.StartDate,
        //                Comments = purchaseOrderComment,//item.Comments,
        //                TimeStamp = item.TimeStamp,
        //                WBS = item.AcOrWbs,
        //                DelayedDate = item.DelayedDate,
        //                SplitLineItemAmount = item.SplitLineItemAmount,
        //                PurchaseOrderLineId = item.PurchaseOrderLineId,
        //                UnApprovedDate = item.UnApprovedDate,
        //                ReplacedWithPo = item.ReplacedWithPo,
        //                IsSplitted = item.IsSplitted,
        //                EbdNumber = item.EbdNumber,
        //                ProductId = productId,//Convert.ToString(item.Product.ProductId),
        //                VendorName = ebdVendorName,
        //                Currency = currency,
        //                StatusPoid = StatusPoId,
        //                OrderDate = orderDate,
        //                ContractTypeId = contractTypeId,
        //                ActivityTypeId = Convert.ToString(item.ActivityType.ActivityTypeId),//activityTypeId,
        //                ExchangeRateYear = item.ExchangeRateYear,
        //                EarlierPaymentDate = item.EarlierPaymentDate,
        //                LastChangeBy = item.LastChangeBy,
        //                InvoiceNumber = item.InvoiceNumber,
        //                ProductNumber = item.ProductNumber,
        //                LastChangeDate = item.LastChangeDate,
        //                Lastmodifydate = item.LastChangeDate,
        //                Lastmodifyname = Convert.ToString(item.LastChangeBy),   
        //                RenewalOrderPurchaseLine = item.RenewalOrderPurchaseLine,
        //                CostCenterId = costCenterID,
        //                CostCenterName = costCenterName
        //            };
        //            customModelList.Add(obj);
        //        }
        //    }

        //    //var purchaseOrder = this.Session.CreateSQLQuery("exec GetTopPurchaseOrderDetail :purchaseOrderId")
        //    //                        .AddEntity(typeof(modelTest))
        //    //                        .SetParameter("purchaseOrderId", purchaseOrderId)
        //    //                        .List<modelTest>();


        //    //var purchaseOrder1 = this.Session.GetNamedQuery("GetTopPurchaseOrderDetail")
        //    //                        .SetString("purchaseOrderId", purchaseOrderId);
        //    //                        //.SetResultTransformer(
        //    //                        //    Transformers.AliasToBean(typeof(modelTest)))
        //    //                        //.UniqueResult<modelTest>();

        //    //var purchaseOrder1 = this.Session.CreateSQLQuery("exec GetTopPurchaseOrderDetail ?, ?")
        //    //                        .AddEntity(typeof(modelTest))
        //    //                       .SetString("purchaseOrderId", purchaseOrderId)
        //    //                       .List<modelTest>();

        //    //.SetResultTransformer(
        //    //    Transformers.AliasToBean(typeof(modelTest)))
        //    //.UniqueResult<modelTest>();



        //    //return purchaseOrder.FirstOrDefault();

        //    return customModelList.OrderBy(x=>x.PoLine).ToList();

        //    //return new POLine();
        //}


        public bool SavePurchaserName(string purchaserName, string poEbd_Id)
        {
            PurchaseOrder purchaseOrderData = null;
            var edbData = this.Session.QueryOver<PurchaseOrderLineFromEbd>().List();
            var final = edbData.Where(x => x.PurchaseOrderLineFromEbdId.ToString() == poEbd_Id).FirstOrDefault();
            if (final != null)
            {
                purchaseOrderData = this.Session.QueryOver<PurchaseOrder>().Where(x => x.PurchaseOrderId == final.PurchaseOrder.PurchaseOrderId).List().FirstOrDefault();
            }
            if(purchaseOrderData != null)
            {
                using (var transaction = this.Session.BeginTransaction())
                {
                   purchaseOrderData.PurchaserName = purchaserName;
                   this.Session.Save("PurchaseOrder", purchaseOrderData);
                   transaction.Commit();
                }
                return true;
            }
            return false;
        }
    }


    
}
