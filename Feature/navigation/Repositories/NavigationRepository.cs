using Foundation.BlazorExtensions;
using Foundation.BlazorExtensions.Services;
using SitecoreBlazorHosted.Shared.Models.Navigation;
using SitecoreBlazorHosted.Shared.Models.Sitecore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feature.Navigation.Repositories
{
  public class NavigationRepository
  {
    private readonly BlazorContext _blazorContext;
    private readonly SitecoreItemService _sitecoreItemService;

    public NavigationRepository(BlazorContext blazorContext, SitecoreItemService sitecoreItemService)
    {
      _blazorContext = blazorContext;
      _sitecoreItemService = sitecoreItemService;
    }

    public Task<List<NavigationItem>> GetBreadcrumb()
    {


      return Task.Run(async () =>
      {
        var items = await GetNavigationHierarchy();
        items.Reverse();
        for (var i = 0; i < items.Count - 1; i++)
        {
          items[i].Level = i;
        }

        return items;
      });


    }

    private Task<List<NavigationItem>> GetNavigationHierarchy()
    {
      return Task.Run(async () =>
      {

        var routeId = _blazorContext.ContextRoute?.Id;

        var menuItems = new List<NavigationItem>();

        foreach (var item in await GetMenu())
        {
          menuItems.Add(item);

          if (item.Children != null && item.Children.Any())
          {
            menuItems.AddRange(item.Children);
          }


        }

        List<NavigationItem> result = new List<NavigationItem>();


        while (!string.IsNullOrWhiteSpace(routeId))
        {
          NavigationItem currentItem = menuItems.FirstOrDefault(i => i.Item.Id == routeId);

          if (currentItem != null)
          {
            result.Add(currentItem);
          }

          routeId = currentItem?.Item?.Parent?.Id;

        }

        return result;
      });

    }

    private NavigationItem CreateNavigationItem(ISitecoreItem item)
    {

      return new NavigationItem
      {
        Item = item,
        Url = item.Url,
        Children = item.HasChildren ? GetChildNavigationItems(item) : null
      };
    }

    private List<NavigationItem> GetChildNavigationItems(ISitecoreItem item)
    {
      List<NavigationItem> children = new List<NavigationItem>();

      if (!item.HasChildren)
        return children;

      foreach (var child in item.Children)
      {
        if (child == null)
          continue;

        children.Add(CreateNavigationItem(child));
      }

      return children;
    }

    public Task<List<NavigationItem>> GetMenu()
    {

      return Task.Run(() =>
      {

        ISitecoreItem rootItem = _sitecoreItemService.GetSitecoreItemRootMock(_blazorContext.ContextLanguage);


        List<NavigationItem> list = new List<NavigationItem>
          {
                   new NavigationItem() //Home
                   {
                       Item = rootItem,
                       Url = rootItem.Url,
                       Children = null
                   }
          };

        foreach (ISitecoreItem item in rootItem.Children)
        {

          if (item == null)
            continue;

          list.Add(
                   CreateNavigationItem(item)
                   );
        }


        return list;


      });
    }
  }
}
