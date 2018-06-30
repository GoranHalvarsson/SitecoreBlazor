using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class HtmlField : IField
    {

        public const string FieldType = "HtmlField";

        public string FieldName { get; set; }
    
      
        public FieldValue<string> Value { get; set; }

    }
}
