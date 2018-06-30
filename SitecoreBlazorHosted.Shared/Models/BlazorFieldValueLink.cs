using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class BlazorFieldValueLink : IBlazorFieldValue
    {
        public string Href { get; set; }

        public string Text { get; set; }

        public string Target { get; set; }

        public string Class { get; set; }

    }
}
