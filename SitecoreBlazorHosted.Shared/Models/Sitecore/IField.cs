using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models.Sitecore
{
    
    public interface IField
    {
        string FieldId { get; }

        string Name { get; }

        /// <summary>
        /// Returns field's type. Possible values are :
        /// * Text
        /// * Image
        /// * Rich Text
        /// * Checkbox
        /// * Date
        /// * Datetime
        /// * Multilist
        /// * Treelist
        /// * Checklist
        /// * Droplink
        /// * Droptree
        /// * General Link
        /// * Single-Line Text
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Returns field's raw value.
        /// </summary>
        string RawValue { get; }
    }
}
