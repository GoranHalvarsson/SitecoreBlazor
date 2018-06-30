using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

   
    

  }
}
