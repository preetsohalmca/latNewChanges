using Volvo.NVS.Integration.Messages;

namespace Volvo.LAT.IntegrationUtility.Services
{
    public interface IIntegrationHandlerService
    {
        /// <summary>
        /// Handles received messages validating and deserializing them.
        /// </summary>
        /// <param name="message">A message to be processed.</param>
        void HandleMessage(IInputMessage message);
    }
}
