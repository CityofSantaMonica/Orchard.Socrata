using CSM.Socrata.Models;
using Orchard;
using SODA;

namespace CSM.Socrata.Services
{
    /// <summary>
    /// Service definition for making Socrata connections using the defined site settings.
    /// </summary>
    public interface ISocrataConnectionService : IDependency
    {
        /// <summary>
        /// Get the Socrata settings defined in the current site.
        /// </summary>
        SocrataSettingsPart GetSettings();

        /// <summary>
        /// Create a new <see cref="SODA.SodaClient"/> for read-access using the defined site settings.
        /// </summary>
        SodaClient GetAnonymousClient();

        /// <summary>
        /// Create a new <see cref="SODA.SodaClient"/> for read/write-access using the defined site settings.
        /// </summary>
        SodaClient GetAuthenticatedClient();
    }
}