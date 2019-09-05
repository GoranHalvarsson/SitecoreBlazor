using SitecoreBlazorHosted.Shared.Models;
using System.Collections.Generic;

namespace Foundation.BlazorExtensions.Extensions
{
    public static class BlazorItemExtensions
    {

        public static IEnumerable<IBlazorItem> GetItSelfAndDescendants(this IBlazorItem thisItem)
        {
            yield return thisItem;

            if (!thisItem.HasChildren)
                yield break;

            foreach (IBlazorItem child in thisItem.Children)
            {
                yield return child;

                if (!child.HasChildren)
                    continue;

                foreach (IBlazorItem i in GetItSelfAndDescendants(child))
                {
                    yield return i;
                }
            }

        }

        public static bool GetBoolValue(this IBlazorItem thisItem, string fieldName, bool defaultValue)
        {
            return thisItem.Fields.Checkbox(fieldName) == null ? defaultValue : thisItem.Fields.Checkbox(fieldName).Value;
        }

    }
}

