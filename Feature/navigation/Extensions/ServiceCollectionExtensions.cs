
using Feature.Navigation.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Feature.Navigation.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddForFeatureNavigation(this IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton<NavigationRepository>();
     
    }

  }
}
