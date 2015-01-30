using CSM.Socrata.Models;
using Orchard;
using SODA;

namespace CSM.Socrata.Services
{
    /// <summary>
    /// Service definition for making Socrata connections using the defined site settings.
    /// </summary>
    public interface ISocrataService : IDependency
    {
        /// <summary>
        /// Get the Socrata settings defined in the current site.
        /// </summary>
        SocrataSettingsPart GetSettings();

        /// <summary>
        /// Create a new <see cref="SODA.SodaClient"/> using the specified settings.
        /// </summary>
        SodaClient GetClient(SocrataSettingsPart settings);

        /// <summary>
        /// Determine if the specificed identifier is a valid Socrata (4x4) resource identifier
        /// </summary>
        bool IsValidResourceIdentifier(string identifier);
    }
}