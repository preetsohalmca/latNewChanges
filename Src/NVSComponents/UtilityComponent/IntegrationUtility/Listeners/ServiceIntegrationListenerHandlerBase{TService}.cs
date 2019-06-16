using Volvo.NVS.Core.Exceptions;
using Volvo.NVS.Core.Logging;
using Volvo.NVS.Core.Unity;
using Volvo.NVS.Integration.Events;
using Volvo.NVS.Integration.Messages;
using Volvo.NVS.Integration.WindowsServices.Proxies;
using Volvo.NVS.Integration.WindowsServices.Services;
using Volvo.LAT.IntegrationUtility.Services;

namespace Volvo.LAT.IntegrationUtility.Listeners
{
    /// <summary>
    /// Provides a base implementation for an integration handled delegating message processing into a specific service.
    /// </summary>
    /// <typeparam name="TService">A type of an interface which implementation handled the message processing.</typeparam>
    public abstract class ServiceIntegrationListenerHandlerBase<TService> : IntegrationListenerHandlerBase
        where TService : IIntegrationHandlerService
    {
        /// <summary>
        /// Gets the service which is used in order to handle the message.
        /// </summary>
        protected TService Service { get; }

        /// <summary>
        /// Gets the application specific implementation of ILogger.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceIntegrationListenerHandlerBase{TService}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="logger">The logger.</param>
        protected ServiceIntegrationListenerHandlerBase(TService service, ILogger logger)
        {
            Service = service;
            Logger = logger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceIntegrationListenerHandlerBase{TService}"/> class.
        /// </summary>
        protected ServiceIntegrationListenerHandlerBase()
        {
            Service = Container.Resolve<TService>();
            Logger = Container.Resolve<ILogger>();
        }

        /// <summary>
        /// Executes when a new message arrives.
        /// </summary>
        /// <param name="channel">The channel for which the event is raised.</param>
        /// <param name="message">The message received.</param>
        public override void OnListenerMessageReceived(IOperativeChannelProxy channel, IInputMessage message) =>
            Service.HandleMessage(message);

        public override void OnListenerErrorOccured(IOperativeChannelProxy channel, CommunicationEventArgs<string> e) =>
            Logger.LogError($"Channel {channel.Name} got error: {e.Value}. Recommended to stop:{e.RecommendedForStop}");

        public override void OnListernerStatusChanged(IOperativeChannelProxy channel, CommunicationStatusChanged e)
        {
            switch (e.Value)
            {
                case CommunicationStatus.Disconnected:
                    // We should always send an alert to concerned person/group/system in case there is a network issue and because
                    // of that, the service is not able to communicate with Integration platform.
                    // This notify method should map with some Alert system such as Email or Talert etc
                    Logger.LogInfo($"Window service listener is NOT able to get connection for {GetType().Name}.");
                    Logger.LogInfo("It will try to reconnect...");
                    break;
                case CommunicationStatus.Connected:
                    // We should always send an alert to concerned person/group/system in case there is a network issue and because
                    // of that, the service is not able to communicate with Integration platform.
                    // This notify method should map with some Alert system such as Email or Talert etc
                    Logger.LogInfo($"{GetType().Name} Connected...");
                    break;
                case CommunicationStatus.Offline:
                    var exception = new NVSException(
                        $"Lost all connections to integration platform. {GetType().Name} Channel Offline");
                    Logger.LogError(exception);
                    break;
                case CommunicationStatus.Connecting:
                    Logger.LogInfo($"Connecting to {GetType().Name}...");
                    break;
                default:
                    Logger.LogError("Unknown error in Listener occurred.");
                    break;
            }
        }
    }
}
