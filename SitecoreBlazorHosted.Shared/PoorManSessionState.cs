using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SitecoreBlazorHosted.Shared
{
    public class PoorManSessionState
    {

        public int ValidCacheInHours { get; set; } = 12;

        public string Language { get; set; }

        public string RouteId { get; set; }

        public IList<ValueTuple<DateTime, string, Route>> NavigatedRoutes { get; set; }


        public Route GetNavigatedRoute(string url)
        {
            if (NavigatedRoutes == null)
                return null;

            DateTime CacheValidTo = DateTime.Now.AddHours(ValidCacheInHours);

            return NavigatedRoutes.Where(navRoot => navRoot.Item2 == url && navRoot.Item1 <= CacheValidTo).Select(navroot => navroot.Item3).FirstOrDefault();
        }

        public void AddNavigatedRoute(string url, Route routeToAdd)
        {
            if (NavigatedRoutes == null)
                NavigatedRoutes = new List<ValueTuple<DateTime, string, Route>>();

            (DateTime, string, Route) foundItem = NavigatedRoutes.FirstOrDefault(navRoot => navRoot.Item2 == url);

            if (foundItem.Item3 != null)
                return;

            NavigatedRoutes.Add(new ValueTuple<DateTime, string, Route>(DateTime.Now, url, routeToAdd));
        }

        public void RemoveNavigatedRoute(string url, Route routeToAdd)
        {
            if (NavigatedRoutes == null)
                return;

            (DateTime, string, Route) foundItem = NavigatedRoutes.FirstOrDefault(navRoot => navRoot.Item2 == url);

            if (foundItem.Item3 == null)
                return;

            NavigatedRoutes.Remove(foundItem);
        }

    }
}
