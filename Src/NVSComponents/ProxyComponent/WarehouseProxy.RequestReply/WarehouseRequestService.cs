using System;
using Volvo.NVS.Integration;
using Volvo.NVS.Utilities.Xml;
using Volvo.POS.OrderDomain.DomainLayer.Entities;
using Volvo.POS.Proxy.Warehouse.RequestReply.Contracts;
using Volvo.POS.Proxy.Warehouse.RequestReply.Translators;

namespace Volvo.POS.Proxy.Warehouse.RequestReply
{
    /// <summary>
    /// A request and replay service into the Warehouse Management System.
    /// </summary>
    public class WarehouseRequestService : IWarehouseRequestService
    {
        /// <summary>
        /// Sends a part reservation request into the WMS.
        /// </summary>
        /// <param name="order">An order for which part reservation request should be send.</param>
        /// <returns>An id of the message allowing us to correlate request with an response.</returns>
        public byte[] SubmitOrderReservation(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            IXmlSerializer serializer = new XmlSerializer();
            var message = serializer.Serialize(PartReservationRequestTranslator.Create(order), Schemas.PartsReservationRequestXsd);

            using (var channel = ChannelFactory.CreateOutputChannel("WMSRequestQueue"))
            {
                var msg = channel.CreateMessage();
                msg.AppendData(message);
                return channel.AsyncRequest(msg);
            }
        }
    }
}
