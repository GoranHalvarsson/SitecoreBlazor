using System.Threading.Tasks;
using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions.Extensions;
using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Components.Electron;
using Microsoft.Extensions.DependencyInjection;
using Project.BlazorSite;

namespace SitecoreBlazorHosted.Electron
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddScoped<IRestService, RestService>();
            builder.Services.AddForFoundationBlazorExtensions();
            builder.Services.AddForFeatureNavigation();

            builder.RootComponents.Add<App>("app");

            ComponentsElectron.Run<builder>("wwwroot/index.html");

            await builder.Build().RunAsync();
        }
    }
}
