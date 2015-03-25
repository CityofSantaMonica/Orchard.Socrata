using CSM.Socrata.Models;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;

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
            return ContentShape(
                "Parts_Socrata_Settings_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.Socrata.Settings",
                    Model: part,
                    Prefix: Prefix
                )
            )
            //important to render the shape in the "Socrata" section of Settings menu
            .OnGroup("Socrata");
        }

        protected override DriverResult Editor(SocrataSettingsPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}