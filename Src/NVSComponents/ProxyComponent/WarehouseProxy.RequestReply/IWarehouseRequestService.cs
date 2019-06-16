using Volvo.POS.OrderDomain.DomainLayer.Entities;

namespace Volvo.POS.Proxy.Warehouse.RequestReply
{
    /// <summary>
    /// Defines a contract for the request and replay service into the Warehouse Management System.
    /// </summary>
    public interface IWarehouseRequestService
    {
        /// <summary>
        /// Sends a part reservation request into the WMS.
        /// </summary>
        /// <param name="order">An order for which part reservation request should be send.</param>
        /// <returns>An id of the message allowing us to correlate request with an response.</returns>
        byte[] SubmitOrderReservation(Order order);
    }
}
