using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

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

        public static string Html(this string fieldValue) => @System.Web.HttpUtility.HtmlDecode(fieldValue);

        private static T BlazorItemField<T>(this List<IBlazorItemField> fields, string fieldName) where T : class
        {

            IBlazorItemField field = fields?.FirstOrDefault(f => f.FieldName == fieldName);


            return field as T;
        }



        public static RenderFragment RenderLinkField(this List<IBlazorItemField> fields, string fieldName, IDictionary<string, object>? htmlAttributes) => builder =>
        {

            BlazorItemField<BlazorFieldValueLink> linkField = BlazorItemField<BlazorItemField<BlazorFieldValueLink>>(fields, fieldName);

            var anchorAttrs = new Dictionary<string, object>
               {
                   { "href", linkField.Value.Href },
                   { "class", linkField.Value.Class },
                   { "title", linkField.Value.Text },
                   { "target", linkField.Value.Target }
               };


            builder.OpenElement(0, "a");
            builder.AddMultipleAttributes(1, anchorAttrs);

            if (htmlAttributes != null)
                builder.AddMultipleAttributes(1, htmlAttributes);

            if (!string.IsNullOrWhiteSpace(linkField.Value.Text))
                builder.AddContent(2, linkField.Value.Text);
            
            builder.CloseElement();
        };


        public static RenderFragment RenderHtmlField(this List<IBlazorItemField> fields, string fieldName, string tag, IDictionary<string, object>? htmlAttributes) => builder =>
        {

            BlazorItemField<string> htmlTextField = BlazorItemField<BlazorItemField<string>>(fields, fieldName);

            if (string.IsNullOrWhiteSpace(tag))
                tag = "p";


            builder.OpenElement(0, tag);


            if (htmlAttributes != null)
                builder.AddMultipleAttributes(1, htmlAttributes);

            builder.AddMarkupContent(2, htmlTextField.Value);
            builder.CloseElement();

        };



    }



}
