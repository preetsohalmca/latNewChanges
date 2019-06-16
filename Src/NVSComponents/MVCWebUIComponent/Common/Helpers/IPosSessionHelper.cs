using System.Diagnostics.CodeAnalysis;
using Volvo.NVS.Utilities.Web.Session;

namespace Volvo.LAT.MVCWebUIComponent.Common.Helpers
{
    /// <summary>
    /// Defines a contract for the POS application session helper.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "PosSession")]
    public interface IPosSessionHelper : ISessionHelper
    {
        /// <summary>
        /// Initialize the POS Session with all the obligatory and basic objects which should be present during the complete
        /// session lifetime. The initialization should be executed once at the Session StartUp.
        /// </summary>
        void InitializeOnStart();

        /// <summary>
        /// Gets a value indicating whether session is a new one (just created).
        /// </summary>
        bool IsNewSession { get; }
    }
}