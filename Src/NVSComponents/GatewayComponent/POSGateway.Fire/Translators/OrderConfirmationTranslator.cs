using System;
using Volvo.POS.Gateway.Fire.Contracts;
using Volvo.POS.OrderDomain.DomainLayer.Entities;

namespace Volvo.POS.Gateway.Fire.Translators
{
    /// <summary>
    /// Performs translations into the <see cref="OrderConfirmation"/>
    /// </summary>
    public static class OrderConfirmationTranslator
    {
        /// <summary>
        /// Translates an order into the order confirmation.
        /// </summary>
        /// <param name="order">An order to be translated.</param>
        /// <param name="confirmed">True in order to send the confirmation of an order. False in order to send the cancellation.</param>
        /// <returns>The order confirmation.</returns>
        public static OrderConfirmation ToOrderConfirmation(Order order, bool confirmed)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return new OrderConfirmation
            {
                OrderNumber = order.Number,
                OrderIsConfirmed = confirmed
            };
        }
    }
}
