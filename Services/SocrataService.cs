using System;
using CSM.Socrata.Models;
using Orchard;
using Orchard.ContentManagement;
using SODA;
using SODA.Utilities;

namespace CSM.Socrata.Services
{
    public class SocrataService : ISocrataService
    {
        private readonly IOrchardServices _orchardServices;

        public SocrataService(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }

        public SocrataSettingsPart GetSettings()
        {
            return _orchardServices.WorkContext.CurrentSite.As<SocrataSettingsPart>();
        }

        public SodaClient GetClient(SocrataSettingsPart settings)
        {
            if (String.IsNullOrEmpty(settings.Username) || String.IsNullOrEmpty(settings.Password))
                return new SodaClient(settings.Host, settings.AppToken);
            else
                return new SodaClient(settings.Host, settings.AppToken, settings.Username, settings.Password);
        }

        public bool IsValidResourceIdentifier(string identifier)
        {
            return FourByFour.IsValid(identifier);
        }
    }
}