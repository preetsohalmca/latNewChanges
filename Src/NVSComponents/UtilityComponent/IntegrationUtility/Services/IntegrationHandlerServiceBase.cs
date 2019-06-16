using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Volvo.NVS.Core.Logging;
using Volvo.NVS.Integration;
using Volvo.NVS.Integration.Messages;
using Volvo.NVS.Utilities.Exceptions;
using Volvo.NVS.Utilities.Xml;

namespace Volvo.LAT.IntegrationUtility.Services
{
    /// <summary>
    /// The class provides common and base logic for message processing services.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The class automatically validates and deserializes received messages according to the provided embedded schema name
    /// from the <see cref="GetEmbeddedSchemaName"/>. The message to be deserialized is determined from the type of the generic
    /// parameter provided into the <see cref="IntegrationHandlerServiceBase{TMessage}"/>.
    /// </para>
    /// <para>
    /// When the message cannot be deserialized for any of the reasons it is marked as <see cref="TransactionBehavior.MarkAsPoison"/>.
    /// </para>
    /// </remarks>
    /// <typeparam name="TMessage">A type of the message which is handled by the service.</typeparam>
    public abstract class IntegrationHandlerServiceBase<TMessage> : IIntegrationHandlerService
        where TMessage : class
    {
        /// <summary>
        /// Gets the logger used by the service.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationHandlerServiceBase{TMessage}"/> class.
        /// </summary>
        /// <param name="logger">A logger used by the service.</param>
        protected IntegrationHandlerServiceBase(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            Logger = logger;
        }

        /// <summary>
        /// Returns a name of the schema which should be used in order to validate incoming messages.
        /// </summary>
        /// <returns>The schema name.</returns>
        protected abstract string GetEmbeddedSchemaName();

        /// <summary>
        /// Processes the already validated and deserialized message.
        /// </summary>
        /// <param name="channelMessage">An original channel message object.</param>
        /// <param name="message">A message to be processed.</param>
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected abstract void ProcessMessage(IInputMessage channelMessage, TMessage message);

        /// <summary>
        /// Handles received messages validating and deserializing them.
        /// </summary>
        /// <param name="message">A message to be processed.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public void HandleMessage(IInputMessage message)
        {
            Logger.LogInfo($"Received {typeof(TMessage).Name}");

            // Validate and deserialize the message. Mark as poison if unable to parse.
            var request = TryDeserializeMessage(message);
            if (request == null)
            {
                message.TransactionBehavior = TransactionBehavior.MarkAsPoison;
                return;
            }

            try
            {
                ProcessMessage(message, request);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                Logger.LogInfo($"{typeof(TMessage).Name} Errors. Rollback Message: {message.MessageId}");
                throw;
            }

            Logger.LogInfo($"{typeof(TMessage).Name} Processed");
        }

        /// <summary>
        /// Gets the stream into the schema from the manifest resource stream located in the specified assembly.
        /// </summary>
        /// <param name="embeddedSchemaResourceName">Full name of the manifest, embedded resource which holds the xml schema document.</param>
        /// <param name="schemaAssembly">Assembly containing the <paramref name="embeddedSchemaResourceName"/> xml schema.</param>
        /// <returns>The stream into the xml schema.</returns>
        private static Stream GetSchemaFromAssembly(string embeddedSchemaResourceName, Assembly schemaAssembly)
        {
            if (schemaAssembly == null)
            {
                throw new ArgumentNullException(nameof(schemaAssembly));
            }

            var schema = schemaAssembly.GetManifestResourceStream(embeddedSchemaResourceName);
            if (schema == null)
            {
                throw new XmlValidationException($"Unable to find {embeddedSchemaResourceName} schema in {schemaAssembly.FullName}");
            }

            return schema;
        }

        /// <summary>
        /// Validates and deserializes the incoming message.
        /// </summary>
        /// <param name="message">A message to be validated and deserialized.</param>
        /// <returns>The message.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        private TMessage TryDeserializeMessage(IInputMessage message)
        {
            try
            {
                IXmlSerializer serializer = new XmlSerializer();
                using (var schema = GetSchemaFromAssembly(GetEmbeddedSchemaName(), typeof(TMessage).Assembly))
                {
                    return serializer.Deserialize<TMessage>(message.Value, schema);
                }
            }
            catch (Exception e)
            {
                Logger.LogInfo($"Unable to parse: {message.MessageId}. Error: {e.Message}");
                return null;
            }
        }
    }
}
