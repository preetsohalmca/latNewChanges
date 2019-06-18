using System;
using Microsoft.Practices.Unity;
using Volvo.NVS.Integration;
using Volvo.NVS.Utilities.Xml;
using Volvo.POS.Gateway.Fire.Contracts;
using Volvo.POS.Gateway.Fire.Translators;
using Volvo.POS.OrderDomain.DomainLayer.Entities;

namespace Volvo.POS.Gateway.Fire
{
    public class GatewayFireService : IGatewayFireService
    {
        /// <summary>
        /// An injected channel to be used by this gateway service.
        /// </summary>
        private readonly IOutputChannel injectedChannel;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayFireService"/> class.
        /// </summary>
        [InjectionConstructor]
        public GatewayFireService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayFireService"/> class.
        /// </summary>
        /// <param name="channel">An injected channel to be used by the gateway.</param>
        public GatewayFireService(IOutputChannel channel)
        {
            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            injectedChannel = channel;
        }

        /// <summary>
        /// Sends information about a confirmed (commited) order into the WMS system.
        /// </summary>
        /// <param name="order">An order for which information should be send.</param>
        public void CommitOrderReservation(Order order) => SendOrderConfirmation(order, true);

        /// <summary>
        /// Sends information about a canceled order into the WMS system.
        /// </summary>
        /// <param name="order">An order for which information should be send.</param>
        public void CancelOrderReservation(Order order) => SendOrderConfirmation(order, false);

        /// <summary>
        /// Creates a channel to be used for communication.
        /// </summary>
        /// <param name="name">A name of the channel to be created.</param>
        /// <returns>The output channel.</returns>
        private IOutputChannel CreateChannel(string name) => injectedChannel ?? ChannelFactory.CreateOutputChannel(name);

        /// <summary>
        /// Sends order confirmations to WMS.
        /// </summary>
        /// <param name="order">An order for which an order confirmation message should be send.</param>
        /// <param name="confirmed">A flag saying if an order should be confirmed or if an order is canceled and reservations can be released.</param>
        private void SendOrderConfirmation(Order order, bool confirmed)
        {
            IXmlSerializer serializer = new XmlSerializer();
            var message = serializer.Serialize(OrderConfirmationTranslator.ToOrderConfirmation(order, confirmed), Schemas.OrderConfirmationXsd);

            using (var channel = CreateChannel("WMSQueue"))
            {
                var channelMessage = channel.CreateMessage();
                channelMessage.AppendData(message);
                channel.Send(channelMessage);
            }
        }
    }
}
