using System;
using AutoMapper;
using Volvo.NVS.Core.Logging;
using Volvo.NVS.Integration.Messages;
using Volvo.NVS.Persistence.NHibernate.SessionHandling;
using Volvo.POS.IntegrationUtility.Services;
using Volvo.POS.PartDomain.DomainLayer.Projections;
using Volvo.POS.PartDomain.ServiceLayer;
using Volvo.POS.Proxy.Warehouse.Subscribe.Contracts;

namespace Volvo.POS.Proxy.Warehouse.Subscribe
{
    /// <summary>
    /// Handles part availability changes received from the WMS application.
    /// </summary>
    public class PartsAvailabilityService : IntegrationHandlerServiceBase<PartsAvailability>, IPartsAvailabilityService
    {
        /// <summary>
        /// Gets the part reservation service to be used.
        /// </summary>
        private IPartService PartService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartsAvailabilityService"/> class.
        /// </summary>
        /// <param name="partService">A part reservation service to be used.</param>
        /// <param name="logger">A logger to be used.</param>
        /// <exception>
        ///     <cref>ArgumentNullException</cref>
        /// </exception>
        public PartsAvailabilityService(IPartService partService, ILogger logger)
            : base(logger)
        {
            if (partService == null)
            {
                throw new ArgumentNullException(nameof(partService));
            }

            PartService = partService;
        }

        protected override string GetEmbeddedSchemaName() => Schemas.PartsAvailabilityInfoXsd;

        protected override void ProcessMessage(IInputMessage channelMessage, PartsAvailability message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            using (new NHibernateSessionContext())
            {
                var parts = Mapper.Map<PartsAvailabilityPart[], PartNewAvailability[]>(message.Parts);
                Logger.LogDebug("ProcessPartsAvailability from wms");
                PartService.UpdatePartAvailability(parts);
                Logger.LogDebug("UpdatePartAvailabilty executed");
            }
        }
    }
}
