using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models.Sitecore
{
    public class ScField : IField
    {
        public string FieldId { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string RawValue { get; private set; }
    }
}
