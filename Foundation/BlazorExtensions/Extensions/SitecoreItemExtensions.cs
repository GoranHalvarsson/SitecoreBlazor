using SitecoreBlazorHosted.Shared.Models.Sitecore;
using System.Collections.Generic;

namespace Foundation.BlazorExtensions.Extensions
{
  public static class SitecoreItemExtensions
  {

    public static IEnumerable<ISitecoreItem> GetItSelfAndDescendants(this ISitecoreItem thisItem)
    {
      yield return thisItem;

      if (thisItem.HasChildren)
      {
        foreach (ISitecoreItem child in thisItem.Children)
        {
          yield return child;

          if (child.HasChildren)
          {
            foreach (ISitecoreItem i in GetItSelfAndDescendants(child))
            {
              yield return i;
            }
          }
        }

      }

    }

  }
}

