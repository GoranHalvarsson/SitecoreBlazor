using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class LanguageService
    {
        public Language GetDefaultLanguage()
        {
            //Get is from sitecore
            return new Language()
            {
                Name = "English",
                NativeName = "English",
                TwoLetterCode = "en"
            };
        }

        public IEnumerable<SitecoreBlazorHosted.Shared.Models.Language>
            GetLanguages()
        {
            yield return new SitecoreBlazorHosted.Shared.Models.Language()
            {
                Name = "en",
                NativeName = "English",
                TwoLetterCode = "en"
            };

            yield return new SitecoreBlazorHosted.Shared.Models.Language()
            {
                Name = "sv",
                NativeName = "Svenska",
                TwoLetterCode = "sv"
            };


        }

        public Language Get(string twoLetterCode)
        {
            return GetLanguages().FirstOrDefault(l => l.TwoLetterCode == twoLetterCode);
        }

        public bool IsValidLanguage(string language)
        {
            string[] validLanguages = new string[] { "sv", "en" };
            return validLanguages.Any(vl => vl == language);
        }
        
    }
}
