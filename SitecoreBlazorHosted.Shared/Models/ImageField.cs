using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class ImageField : IField
    {
        public const string FieldType = "ImageField";

        public string FieldName { get; set; }
        public string Src { get; set; }
        public string Alt { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }

        public FieldValue<FieldValueImage> Value { get; set; }

    }
}
