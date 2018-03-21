using System;
using System.Text;
using CSM.Socrata.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Security;

namespace CSM.Socrata.Handlers
{
    public class SocrataSettingsPartHandler : ContentHandler
    {
        private readonly IEncryptionService _encryptionService;

        public new ILogger Logger { get; set; }
        public Localizer T { get; set; }

        public SocrataSettingsPartHandler(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;

            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;

            //attach the part when the Site is activated
            Filters.Add(new ActivatingFilter<SocrataSettingsPart>("Site"));

            OnActivated<SocrataSettingsPart>((_, part) => lazyLoadHandlers(part));
            OnCreating<SocrataSettingsPart>((_, part) => lazyLoadHandlers(part));
            OnLoaded<SocrataSettingsPart>((_, part) => lazyLoadHandlers(part));
        }

        private void lazyLoadHandlers(SocrataSettingsPart part)
        {
            //decrypt stored password on read
            part.PasswordField.Getter(() => {
                try
                {
                    var encryptedPassword = part.Retrieve(x => x.Password);
                    return String.IsNullOrWhiteSpace(encryptedPassword)
                        ? String.Empty
                        : Encoding.UTF8.GetString(_encryptionService.Decode(Convert.FromBase64String(encryptedPassword)));
                }
                catch
                {
                    Logger.Error("The Socrata password could not be decrypted. It might be corrupted, try to reset it.");
                    return null;
                }
            });

            // encrypt plaintext password on write
            part.PasswordField.Setter(value => {
                var encryptedPassword = String.IsNullOrWhiteSpace(value)
                    ? String.Empty
                    : Convert.ToBase64String(_encryptionService.Encode(Encoding.UTF8.GetBytes(value)));

                part.Store(x => x.Password, encryptedPassword);
            });
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;

            base.GetItemMetadata(context);

            //groups the settings UI under the "Socrata" menu item in Settings menu
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Socrata")));
        }
    }
}