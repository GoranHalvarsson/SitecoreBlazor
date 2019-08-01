using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.BlazorExtensions.Extensions
{
    public static class FieldExtensions
    {

        public static BlazorSitecoreField<BlazorFieldValueLink> Link(this List<IBlazorSitecoreField> fields, string fieldName) => BlazorSitecoreField<BlazorSitecoreField<BlazorFieldValueLink>>(fields, fieldName);

        public static BlazorSitecoreField<string> PlainText(this List<IBlazorSitecoreField> fields, string fieldName) => BlazorSitecoreField<BlazorSitecoreField<string>>(fields, fieldName);

        public static BlazorSitecoreField<string> HtmlText(this List<IBlazorSitecoreField> fields, string fieldName) => BlazorSitecoreField<BlazorSitecoreField<string>>(fields, fieldName);

        public static BlazorSitecoreField<BlazorFieldValueImage> Image(this List<IBlazorSitecoreField> fields, string fieldName) => BlazorSitecoreField<BlazorSitecoreField<BlazorFieldValueImage>>(fields, fieldName);

        public static BlazorSitecoreField<BlazorFieldValueMultiList> MultiList(this List<IBlazorSitecoreField> fields, string fieldName) => BlazorSitecoreField<BlazorSitecoreField<BlazorFieldValueMultiList>>(fields, fieldName);

        public static BlazorSitecoreField<bool> Checkbox(this List<IBlazorSitecoreField> fields, string fieldName) => BlazorSitecoreField<BlazorSitecoreField<bool>>(fields, fieldName);

        public static string HtmlDecode(this string fieldValue) => @System.Web.HttpUtility.HtmlDecode(fieldValue);

        private static T BlazorSitecoreField<T>(this List<IBlazorSitecoreField> fields, string fieldName) where T : class
        {

            IBlazorSitecoreField field = fields?.FirstOrDefault(f => f.FieldName == fieldName);


            return field as T;
        }


     





    }
}
