using System;
using CSM.Socrata.Models;
using Orchard;
using Orchard.ContentManagement;
using SODA;

namespace CSM.Socrata.Services
{
    public class SocrataConnectionService : ISocrataConnectionService
    {
        private readonly IOrchardServices _orchardServices;

        public SocrataConnectionService(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }

        public SocrataSettingsPart GetSettings()
        {
            return _orchardServices.WorkContext.CurrentSite.As<SocrataSettingsPart>();
        }

        public SodaClient GetAnonymousClient()
        {
            var settings = GetSettings();

            if (settings == null)
                throw new InvalidOperationException("No Socrata settings have been defined.");

            return new SodaClient(settings.Host, settings.AppToken);
        }

        public SodaClient GetAuthenticatedClient()
        {
            var settings = GetSettings();

            if (settings == null)
                throw new InvalidOperationException("No Socrata settings have been defined.");

            if (String.IsNullOrEmpty(settings.Username) || String.IsNullOrEmpty(settings.Password))
                throw new InvalidOperationException("No Socrata user credentials have been defined.");

            return new SodaClient(settings.Host, settings.AppToken, settings.Username, settings.Password);
        }
    }
}