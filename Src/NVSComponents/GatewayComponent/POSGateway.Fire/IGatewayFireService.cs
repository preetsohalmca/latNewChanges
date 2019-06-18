using Volvo.POS.OrderDomain.DomainLayer.Entities;

namespace Volvo.POS.Gateway.Fire
{
    public interface IGatewayFireService
    {
        /// <summary>
        /// Sends information about a confirmed (commited) order into the WMS system.
        /// </summary>
        /// <param name="order">An order for which information should be send.</param>
        void CommitOrderReservation(Order order);

        /// <summary>
        /// Sends information about a canceled order into the WMS system.
        /// </summary>
        /// <param name="order">An order for which information should be send.</param>
        void CancelOrderReservation(Order order);
    }
}
