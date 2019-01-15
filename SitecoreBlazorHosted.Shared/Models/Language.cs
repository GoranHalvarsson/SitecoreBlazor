using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    [DataContract]  
    public class Language
    {
        public string NativeName { get; set; }
        public string TwoLetterCode { get; set; }
        public string Name { get; set; }
    }
}
