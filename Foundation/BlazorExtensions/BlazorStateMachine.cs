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

        public bool IsNavBarCollapsed { get; set; } = true;

        public void ToggleNavBar()
        {
            IsNavBarCollapsed = !IsNavBarCollapsed;
        }

        public IList<Tuple<DateTime, string, BlazorRoute>> NavigatedRoutes { get; set; }

        public BlazorRoute CurrentRoute { get; set; }

        public List<IBlazorItemField> GetFieldsFromCurrentRoute(string placeHolder = null)
        {

            List<IBlazorItemField> blazorFields = new List<IBlazorItemField>();

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


        public BlazorRoute GetNavigatedRoute(string url)
        {
            if (NavigatedRoutes == null)
                return null;

            DateTime CacheValidTo = DateTime.Now.AddHours(ValidCacheInHours);

            return NavigatedRoutes.Where(navRoot => navRoot.Item2 == url && navRoot.Item1 <= CacheValidTo).Select(navroot => navroot.Item3).FirstOrDefault();
        }

        public void AddNavigatedRoute(string url, BlazorRoute routeToAdd)
        {
            if (NavigatedRoutes == null)
                NavigatedRoutes = new List<Tuple<DateTime, string, BlazorRoute>>();

            Tuple<DateTime, string, BlazorRoute> foundItem = NavigatedRoutes.FirstOrDefault(navRoot => navRoot.Item2 == url);

            if (foundItem?.Item3 != null)
                return;

            NavigatedRoutes.Add(new Tuple<DateTime, string, BlazorRoute>(DateTime.Now, url, routeToAdd));
        }

        public void RemoveNavigatedRoute(string url, BlazorRoute routeToAdd)
        {
            if (NavigatedRoutes == null)
                return;

            Tuple<DateTime, string, BlazorRoute> foundItem = NavigatedRoutes.FirstOrDefault(navRoot => navRoot.Item2 == url);

            if (foundItem?.Item3 == null)
                return;

            NavigatedRoutes.Remove(foundItem);
        }

    }
}
