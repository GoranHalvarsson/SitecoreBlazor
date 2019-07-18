using Microsoft.AspNetCore.Components;
using System;


namespace Foundation.BlazorExtensions.Extensions
{

    public static class UrlExtensions
    {

        public static string AddBaseUrl(this string url, IUriHelper uriHelper)
        {

            if (url.StartsWith("/"))
                url = url.Substring(1);

            return new Uri(new Uri(uriHelper.GetBaseUri()), url).ToString();
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

    }
}
