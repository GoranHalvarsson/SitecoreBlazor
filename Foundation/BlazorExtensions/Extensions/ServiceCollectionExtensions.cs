using Foundation.BlazorExtensions.Factories;
using Foundation.BlazorExtensions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.BlazorExtensions.Extensions
{
  public static class ServiceCollectionExtensions
  {
    private static void AddStorage(this IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton<LocalStorage>();
      serviceCollection.AddSingleton<SessionStorage>();
    }

    
    public static void AddForFoundationBlazorExtensions(this IServiceCollection serviceCollection)
    {
      AddStorage(serviceCollection);
      serviceCollection.AddSingleton<SitecoreItemService>();
      serviceCollection.AddSingleton<InteropService>();
      serviceCollection.AddSingleton<BlazorContext>();
      serviceCollection.AddSingleton<RouteService>();
      serviceCollection.AddSingleton<LayoutService>();
      serviceCollection.AddSingleton<ComponentFactory>();
      serviceCollection.AddSingleton<RestService>();

    }

  }
}
