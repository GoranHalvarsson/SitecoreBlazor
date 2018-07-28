

namespace SitecoreBlazorHosted.Client
{
    using Microsoft.AspNetCore.Blazor.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }

    //  public class Program
    //{
    //  static void Main(string[] args)
    //  {
    //    var serviceProvider = new BrowserServiceProvider(services =>
    //    {
    //      services.AddForFoundationBlazorExtensions();
    //      services.AddForFeatureNavigation();

    //    });


    //    new BrowserRenderer(serviceProvider).AddComponent<App>("app");
    //  }
//}
}
