using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class LinkField : IField
    {
        public const string FieldType = "LinkField";

        public string FieldName { get; set; }
        public string Href { get; set; }
        public string Text { get; set; }
        public string Target { get; set; }
        public string ClassName { get; set; }

        public FieldValue<FieldValueLink> Value { get; set; }

    }
}
