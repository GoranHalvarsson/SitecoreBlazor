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



        public static RenderFragment RenderLinkField(this BlazorItemField<BlazorFieldValueLink> linkField, IDictionary<string, object>? htmlAttributes) => builder =>
        {
            if(linkField?.Value == null)
                return;


            var anchorAttrs = new Dictionary<string, object>
               {
                   { "href", linkField.Value.Href },
                   { "class", linkField.Value.Class },
                   { "title", linkField.Value.Text },
                   { "target", linkField.Value.Target }
               };


            if (htmlAttributes != null && ("_blank".Equals(anchorAttrs["target"]?.ToString(), StringComparison.InvariantCultureIgnoreCase) && !htmlAttributes.ContainsKey("rel")))
            {
                // information disclosure attack prevention keeps target blank site from getting ref to window.opener
                anchorAttrs["rel"] = "noopener noreferrer";
            }


            builder.OpenElement(0, "a");
            builder.AddMultipleAttributes(1, anchorAttrs);

            if (htmlAttributes != null)
                builder.AddMultipleAttributes(1, htmlAttributes);

            if (!string.IsNullOrWhiteSpace(linkField.Value.Text))
                builder.AddContent(2, linkField.Value.Text);
            
            builder.CloseElement();
        };


        public static RenderFragment RenderHtmlField(this BlazorItemField<string> htmlField, string tag, IDictionary<string, object>? htmlAttributes) => builder =>
        {
            if(htmlField?.Value == null)
                return;
            
          
            if (string.IsNullOrWhiteSpace(tag))
                tag = "p";


            builder.OpenElement(0, tag);


            if (htmlAttributes != null)
                builder.AddMultipleAttributes(1, htmlAttributes);

            builder.AddMarkupContent(2, htmlField.Value);
            builder.CloseElement();

        };


        public static RenderFragment RenderImageField(this BlazorItemField<BlazorFieldValueImage> imageField, ImageSizeParameters? imageParams, IDictionary<string, object>? htmlAttributes, IEnumerable<ImageSizeParameters>? srcSet) => builder =>
        {
            if (imageField?.Value == null)
                return;



            var imageAttributes = GetImageAttributes(imageField.Value.Src, srcSet, htmlAttributes, imageParams);
            if (imageAttributes == null) return;

            builder.OpenElement(0, "img");
            builder.AddMultipleAttributes(1, imageAttributes);
            builder.CloseElement();

        };

        private static IDictionary<string, object> GetImageAttributes(
            string src,
            IEnumerable<ImageSizeParameters>? srcSet,
            IDictionary<string, object>? otherAttrs,
            ImageSizeParameters? imageParams)
        {
            var newAttributes = new Dictionary<string, object>(otherAttrs);
            // update image URL for jss handler and image rendering params
            var resolvedSrc = src.UpdateImageUrl(imageParams);
            if (srcSet != null && srcSet.Any())
            {
                newAttributes["srcSet"] = resolvedSrc.ToSrcSet(srcSet, imageParams);
            }
            else
            {
                newAttributes["src"] = resolvedSrc;
            }
            return newAttributes;
        }

    }



}
