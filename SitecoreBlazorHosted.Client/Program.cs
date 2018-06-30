using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using Foundation.BlazorExtensions.Extensions;
using Feature.Navigation.Extensions;
using System;


namespace SitecoreBlazorHosted.Client
{
  public class Program
  {
    static void Main(string[] args)
    {
      var serviceProvider = new BrowserServiceProvider(services =>
      {
        services.AddForFoundationBlazorExtensions();
        services.AddForFeatureNavigation();

      });


      new BrowserRenderer(serviceProvider).AddComponent<App>("app");
    }
  }
}
