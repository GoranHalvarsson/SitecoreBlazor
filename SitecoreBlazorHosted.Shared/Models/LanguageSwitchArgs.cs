using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class LanguageSwitchArgs : EventArgs
    {

        public Language Language { get; set; }

    }
}
