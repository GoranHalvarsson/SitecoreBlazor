

using System.Threading.Tasks;
using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions.Extensions;
using Foundation.BlazorExtensions.Services;
using Microsoft.Extensions.DependencyInjection;
using Project.BlazorSite;

namespace SitecoreBlazorHosted.Client
{
    using Microsoft.AspNetCore.Blazor.Hosting;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddScoped<IRestService, RestService>();
            builder.Services.AddForFoundationBlazorExtensions();
            builder.Services.AddForFeatureNavigation();

            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }

    }
   
}   
