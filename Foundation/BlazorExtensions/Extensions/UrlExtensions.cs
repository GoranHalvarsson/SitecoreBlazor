using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SitecoreBlazorHosted.Shared.Models;


namespace Foundation.BlazorExtensions.Extensions
{

    public static class UrlExtensions
    {

        private static readonly Regex ImageTagRegex = new Regex("<img([^>]+)/>", RegexOptions.IgnoreCase);

        private static readonly Regex HtmlAttributesRegex = new Regex("([^=\\s]+)(=\"([^\"]*)\")?", RegexOptions.IgnoreCase);

        private static readonly Regex MediaUrlPrefixRegex = new Regex("/([-~]{1})/media/", RegexOptions.IgnoreCase);



        public static string AddBaseUrl(this string url, NavigationManager navigationManager)
        {

            if (!string.IsNullOrWhiteSpace(url) && url.StartsWith("/"))
                url = url.Substring(1);

            return new Uri(new Uri(navigationManager.BaseUri), url).ToString();
        }

        //TODO Not sure where to put this one...
        public static bool HasRouteError(this string url)
        {
            return !string.IsNullOrWhiteSpace(url) && url.Contains(Constants.Route.RouteError);
        }

        public static string RemoveFilePrefix(this string url)
        {

            url = url.Replace(Constants.UrlFixes.FilePrefix, "");

            return url;
        }

        public static string ToRelative(this Uri uri)
        {
            // Null-check

            return uri.IsAbsoluteUri ? uri.PathAndQuery : uri.OriginalString;
        }

        public static string? ToAbsolute(this Uri uri, string baseUrl)
        {
            // Null-checks

            var baseUri = new Uri(baseUrl);

            return uri.ToAbsolute(baseUri);
        }

        public static string? ToAbsolute(this Uri uri, Uri baseUri)
        {
            // Null-checks

            var relative = uri.ToRelative();

            if (Uri.TryCreate(baseUri, relative, out var absolute))
            {
                return absolute.ToString();
            }

            return uri.IsAbsoluteUri ? uri.ToString() : null;
        }


        public static string UpdateImageUrl(
            this string url,
            ImageSizeParameters? parameters)
        {
            var parsedUrl = new Uri(url, UriKind.RelativeOrAbsolute);

            var uriBuilder = parsedUrl.IsAbsoluteUri ?
                new UriBuilder(parsedUrl) :
                new UriBuilder("http://www.tempuri.org" + parsedUrl);

            var imageParameters = parameters?.ToString();
            if (!string.IsNullOrWhiteSpace(imageParameters))
            {
                uriBuilder.Query = imageParameters;
            }

            var match = MediaUrlPrefixRegex.Match(uriBuilder.Path);
            if (match.Length > 1)
            {
                // regex will provide us with /-/ or /~/ type
                uriBuilder.Path = MediaUrlPrefixRegex.Replace(uriBuilder.Path, $"/{match.Groups[1].Value}/jssmedia/");
            }

            return parsedUrl.IsAbsoluteUri ?
                uriBuilder.Uri.GetComponents(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped) :
                uriBuilder.Uri.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
        }

        public static string ToSrcSet(
            this string url,
            IEnumerable<ImageSizeParameters> srcSet,
            ImageSizeParameters? imageParams)
        {
            var srcSetParameters = srcSet.Select(parameters =>
            {
                var newParams = new ImageSizeParameters(imageParams)
                {
                    W = parameters.W,
                    H = parameters.H,
                    Mw = parameters.Mw,
                    Mh = parameters.Mh,
                    Iar = parameters.Iar,
                    As = parameters.As,
                    Sc = parameters.Sc
                };
                var imageWidth = newParams.W ?? newParams.Mw;
                return imageWidth == null ? null : $"{UpdateImageUrl(url, newParams)} {imageWidth}w";
            }).Where(p => p != null);
            return string.Join(", ", srcSetParameters);
        }

        //public static string GetLanguageSegment(this string url, string baseUrl)
        //{

        //    Uri uri = new Uri(new Uri(baseUrl), url);

        //    string segment = uri?.Segments?.ElementAt(1);

        //    if (string.IsNullOrWhiteSpace(segment))
        //        return false;

        //    url = url.Replace(Constants.UrlFixes.FilePrefix, "");

        //    return url;
        //}


    }
}
