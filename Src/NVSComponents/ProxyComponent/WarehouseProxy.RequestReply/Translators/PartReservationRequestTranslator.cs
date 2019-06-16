using AutoMapper;
using Volvo.POS.OrderDomain.DomainLayer.Entities;
using Volvo.POS.Proxy.Warehouse.RequestReply.Contracts;

namespace Volvo.POS.Proxy.Warehouse.RequestReply.Translators
{
    /// <summary>
    /// Performs translations to and from the <see cref="PartReservationRequest"/>.
    /// </summary>
    public static class PartReservationRequestTranslator
    {
        /// <summary>
        /// Creates a part reservation request for a given order.
        /// </summary>
        /// <param name="order">An order for which the request should be created.</param>
        /// <returns>A part reservation request for a given order.</returns>
        public static PartReservationRequest Create(Order order) => Mapper.Map<Order, PartReservationRequest>(order);
    }
}
