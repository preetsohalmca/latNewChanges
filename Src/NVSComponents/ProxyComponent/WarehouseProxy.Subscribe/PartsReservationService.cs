using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Volvo.NVS.Core.Logging;
using Volvo.NVS.Integration.Messages;
using Volvo.NVS.Persistence.NHibernate.SessionHandling;
using Volvo.POS.IntegrationUtility.Services;
using Volvo.POS.OrderDomain.DomainLayer.Entities;
using Volvo.POS.OrderDomain.ServiceLayer;
using Volvo.POS.Proxy.Warehouse.Subscribe.Contracts;

namespace Volvo.POS.Proxy.Warehouse.Subscribe
{
    /// <summary>
    /// Handles part reservation responses received from the WMS application.
    /// </summary>
    public class PartsReservationService : IntegrationHandlerServiceBase<PartReservationResponse>, IPartsReservationService
    {
        /// <summary>
        /// Gets the part ordering service used by the gateway.
        /// </summary>
        public IOrderService OrderService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartsReservationService"/> class.
        /// </summary>
        /// <param name="orderService">The order service.</param>
        /// <param name="logger">The logger.</param>
        /// <exception>
        ///     <cref>ArgumentNullException</cref>
        /// </exception>
        public PartsReservationService(IOrderService orderService, ILogger logger)
            : base(logger)
        {
            if (orderService == null)
            {
                throw new ArgumentNullException(nameof(orderService));
            }

            OrderService = orderService;
        }

        protected override string GetEmbeddedSchemaName() => Schemas.PartsReservationInfoXsd;

        protected override void ProcessMessage(IInputMessage channelMessage, PartReservationResponse message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            using (new NHibernateSessionContext())
            {
                var rows = Mapper.Map<IEnumerable<PartReservationResponsePart>, IEnumerable<OrderLine>>(message.OrderRows);
                Logger.LogDebug(
                    $"Requesting order service to process the response for Order Number {message.OrderNumber}");
                OrderService.ProcessPartReservationResponse(message.OrderNumber, rows.ToList());
                Logger.LogDebug("ProcessPartReservationResponse executed");
            }
        }
    }
}
