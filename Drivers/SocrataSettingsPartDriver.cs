using CSM.Socrata.Models;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System;

namespace CSM.Socrata.Drivers
{
    public class SocrataSettingsPartDriver : ContentPartDriver<SocrataSettingsPart>
    {
        public Localizer T { get; set; }

        public SocrataSettingsPartDriver()
        {
            T = NullLocalizer.Instance;
        }

        protected override string Prefix
        {
            get { return "SocrataSettings"; }
        }

        protected override DriverResult Editor(SocrataSettingsPart part, dynamic shapeHelper)
        {
            var shapeName = "Parts_Socrata_Settings_Edit";

            Func<dynamic> factory = () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.Socrata.Settings",
                    Model: part,
                    Prefix: Prefix
            );

            var forContent = ContentShape(shapeName, factory);
            var forSiteSettings = ContentShape(shapeName, factory).OnGroup("Socrata");

            return Combined(forContent, forSiteSettings);
        }

        protected override DriverResult Editor(SocrataSettingsPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}