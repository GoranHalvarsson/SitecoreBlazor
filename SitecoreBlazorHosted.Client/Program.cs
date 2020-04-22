

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions.Extensions;
using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Project.BlazorSite;


namespace SitecoreBlazorHosted.Client
{

    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IRestService, RestService>();
            builder.Services.AddForFoundationBlazorExtensions();
            builder.Services.AddForFeatureNavigation();

            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }

    }
   
}   
