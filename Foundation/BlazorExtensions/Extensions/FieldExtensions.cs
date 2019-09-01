using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.BlazorExtensions.Extensions
{
    public static class FieldExtensions
    {

        public static BlazorItemField<BlazorFieldValueLink> Link(this List<IBlazorItemField> fields, string fieldName) => BlazorItemField<BlazorItemField<BlazorFieldValueLink>>(fields, fieldName);

        public static BlazorItemField<string> PlainText(this List<IBlazorItemField> fields, string fieldName) => BlazorItemField<BlazorItemField<string>>(fields, fieldName);

        public static BlazorItemField<string> HtmlText(this List<IBlazorItemField> fields, string fieldName) => BlazorItemField<BlazorItemField<string>>(fields, fieldName);

        public static BlazorItemField<BlazorFieldValueImage> Image(this List<IBlazorItemField> fields, string fieldName) => BlazorItemField<BlazorItemField<BlazorFieldValueImage>>(fields, fieldName);

        public static BlazorItemField<BlazorFieldValueMultiList> MultiList(this List<IBlazorItemField> fields, string fieldName) => BlazorItemField<BlazorItemField<BlazorFieldValueMultiList>>(fields, fieldName);

        public static BlazorItemField<bool> Checkbox(this List<IBlazorItemField> fields, string fieldName) => BlazorItemField<BlazorItemField<bool>>(fields, fieldName);

        public static string HtmlDecode(this string fieldValue) => @System.Web.HttpUtility.HtmlDecode(fieldValue);

        private static T BlazorItemField<T>(this List<IBlazorItemField> fields, string fieldName) where T : class
        {

            IBlazorItemField field = fields?.FirstOrDefault(f => f.FieldName == fieldName);


            return field as T;
        }


     





    }
}
