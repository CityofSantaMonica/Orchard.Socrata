﻿using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;

namespace CSM.Socrata.Models
{
    /// <summary>
    /// Defines a part for basic Socrata connection information.
    /// </summary>
    public class SocrataSettingsPart : ContentPart
    {
        private readonly ComputedField<string> _password = new ComputedField<string>();
        
        internal ComputedField<string> PasswordField
        {
            get { return _password; }
        }

        public string Host
        {
            get { return this.Retrieve(x => x.Host); }
            set { this.Store(x => x.Host, value); }
        }

        public string AppToken
        {
            get { return this.Retrieve(x => x.AppToken); }
            set { this.Store(x => x.AppToken, value); }
        }

        public string Username
        {
            get { return this.Retrieve(x => x.Username); }
            set { this.Store(x => x.Username, value); }
        }

        public string Password
        {
            get { return PasswordField.Value; }
            set { PasswordField.Value = value; }
        }

        public int? Timeout
        {
            get { return this.Retrieve(x => x.Timeout); }
            set { this.Store(x => x.Timeout, value); }
        }
    }
}
