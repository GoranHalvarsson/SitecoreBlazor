using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models.Sitecore
{
    public class ItemSource : ItemSourcePOD
    {
        public ItemSource(string database, string language, int? version = null)
          : base(database, language, version)
        {
        }

        public override IItemSource ShallowCopy()
        {
            return new ItemSource(this.Database, this.Language, this.VersionNumber);
        }
    }
}
