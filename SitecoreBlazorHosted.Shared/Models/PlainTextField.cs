using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class PlainTextField : IField
    {
        public const string FieldType = "PlainTextField";

        public string FieldName { get; set; }
        public FieldValue<string> Value { get; set; }
    }
}
