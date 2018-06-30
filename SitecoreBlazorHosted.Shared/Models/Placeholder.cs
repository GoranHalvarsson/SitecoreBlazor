using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
   public class Placeholder
    {
        public dynamic Params { get; set; }

        public string Assembly { get; set; }

        public string ComponentName { get; set; }

        public string Name { get; set; }

        public Dictionary<string, BlazorField> Fields { get; set; }

        public Dictionary<string, IList<Placeholder>> Placeholders { get; set; }
        
    }
}
