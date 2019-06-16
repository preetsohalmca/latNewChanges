using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrderDomainEntities = Volvo.POS.OrderDomain.DomainLayer.Entities;
using Volvo.POS.GeneralUtilities.Common.Utilities.Xml;
using System.Reflection;
using System.IO;
using AutoMapper;
using Volvo.POS.ProxyComponents.Warehouse.Fire.Translators;
using Volvo.POS.GeneralUtilities.Common.Utilities.AutoMapper;
using WarehouseEntities = Volvo.POS.ProxyComponents.Warehouse.Fire.Contracts;
using Volvo.POS.ProxyComponents.Warehouse.Fire.Contracts;

namespace Volvo.POS.ProxyComponents.Warehouse.Fire
{
    class PartsReservationDummy : IPartsReservation
    {
        public PartsReservationDummy()
        {
            Mapper.Configuration.AddProfileThreadSafe(new WarehouseFireProfile());
        }

        /// <summary>
        /// This is dummy method to use when there is no integration platform available
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="parts"></param>
        public void SendReservePartsRequestToWMS(int orderNo, IList<OrderDomain.DomainLayer.Entities.OrderRow> parts)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// This is dummy method to use when there is no integration platform available
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="isOrderConfirmedstatus"></param>
        public void SendOrderStatusToWMS(int orderNo, bool isOrderConfirmedstatus)
        {
            //throw new NotImplementedException();
        }

        public void SubmitPartsReservationRequest(OrderDomainEntities.Order domainOrder)
        {

        }

        public void CommitPartsReservationRequest(OrderDomainEntities.Order order)
        {

        }

        public void CancelPartsReservationRequest(OrderDomainEntities.Order order)
        {

        }
    }
}
