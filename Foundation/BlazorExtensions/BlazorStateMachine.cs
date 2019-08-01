using Foundation.BlazorExtensions.Factories;
using Microsoft.AspNetCore.Components;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.BlazorExtensions
{

    public class BlazorStateMachine
    {
        private readonly ComponentFactory _componentFactory;

        public BlazorStateMachine(ComponentFactory componentFactory)
        {
            _componentFactory = componentFactory;
        }

        public int ValidCacheInHours { get; set; } = 12;

        public string Language { get; set; }

        public string RouteId { get; set; }

        public IList<Tuple<DateTime, string, Route>> NavigatedRoutes { get; set; }

        public Route CurrentRoute { get; set; }

        public List<IBlazorSitecoreField> GetFieldsFromCurrentRoute(string placeHolder = null)
        {

            List<IBlazorSitecoreField> blazorFields = new List<IBlazorSitecoreField>();

            if (CurrentRoute == null)
                return null;

            if (string.IsNullOrWhiteSpace(placeHolder) || !CurrentPlaceholders.Any(pl => pl.Key == placeHolder))
            {
                blazorFields.AddRange(_componentFactory.CreateComponentModel(CurrentRoute.Fields));
            }

            if (CurrentPlaceholders.Any(pl => pl.Key == placeHolder))
            {

                var placeHoldersList = CurrentPlaceholders?.Where(ph => ph.Key == placeHolder).ToList();


                foreach (KeyValuePair<string, IList<Placeholder>> keyVal in placeHoldersList)
                {

                    foreach (Placeholder placeholderData in keyVal.Value)
                    {

                        blazorFields.AddRange(_componentFactory.CreateComponentModel(placeholderData.Fields));
                    }
                }

            }

            return blazorFields;

        }


        

        public IEnumerable<KeyValuePair<string, IList<Placeholder>>> CurrentPlaceholders { get; set; }


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
                NavigatedRoutes = new List<Tuple<DateTime, string, Route>>();

            Tuple<DateTime, string, Route> foundItem = NavigatedRoutes.FirstOrDefault(navRoot => navRoot.Item2 == url);

            if (foundItem?.Item3 != null)
                return;

            NavigatedRoutes.Add(new Tuple<DateTime, string, Route>(DateTime.Now, url, routeToAdd));
        }

        public void RemoveNavigatedRoute(string url, Route routeToAdd)
        {
            if (NavigatedRoutes == null)
                return;

            Tuple<DateTime, string, Route> foundItem = NavigatedRoutes.FirstOrDefault(navRoot => navRoot.Item2 == url);

            if (foundItem?.Item3 == null)
                return;

            NavigatedRoutes.Remove(foundItem);
        }

    }
}
