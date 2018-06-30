using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models.Sitecore
{
    public class ItemSourcePOD : IItemSource
    {
        public ItemSourcePOD(string Database, string Language, int? VersionNumber = null)
        {
            this.Database = Database;
            this.Language = Language;
            this.VersionNumber = VersionNumber;
        }

        public virtual IItemSource ShallowCopy()
        {
            return new ItemSourcePOD(this.Database, this.Language, this.VersionNumber);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            ItemSourcePOD other = (ItemSourcePOD)obj;
            if (null == other)
            {
                return false;
            }

            bool isDbEqual = object.Equals(this.Database, other.Database);
            bool isLangEqual = object.Equals(this.Language, other.Language);
            bool isVersionEqual = object.Equals(this.VersionNumber, other.VersionNumber);

            return isDbEqual && isLangEqual && isVersionEqual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + this.Database.GetHashCode() + this.Language.GetHashCode() + this.VersionNumber.GetHashCode();
        }

        public string Database { get; protected set; }
        public string Language { get; protected set; }
        public int? VersionNumber { get; protected set; }
    }
}
