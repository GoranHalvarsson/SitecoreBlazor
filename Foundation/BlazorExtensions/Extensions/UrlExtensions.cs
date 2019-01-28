using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation.BlazorExtensions.Services;
using SitecoreBlazorHosted.Shared.Models;

namespace Foundation.BlazorExtensions.Extensions
{

    public static class UrlExtensions
    {

        public static string WithBaseUrl_UglyHackForGithub(this string url, IUriHelper uriHelper)
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

     


    }
}
