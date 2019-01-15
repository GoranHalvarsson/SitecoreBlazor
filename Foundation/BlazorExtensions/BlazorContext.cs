

using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions
{


    public class BlazorContext
    {
        private static SessionStorage _sessionStorage;

        public BlazorContext(SessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        [Obsolete("Please use GetContextLanguageAsync")]
        public string ContextLanguage
        {
            get
            {

                if (_sessionStorage?.GetItem("contextLanguage") == null)
                    _sessionStorage?.SetItem("contextLanguage", "en");

                return _sessionStorage?.GetItem("contextLanguage");
            }
        }

        public Task<string> GetContextLanguageAsync()
        {
            return _sessionStorage.GetItemAsync(Constants.Storage.StorageKeys.ContextLanguage);
        }

        public  Task SetContextLanguageAsync(string language)
        {

            if (String.IsNullOrWhiteSpace(language))
                return null;

             return _sessionStorage.SetItemAsync(Constants.Storage.StorageKeys.ContextLanguage, language);

        }

        public Task<string> GetCurrentRouteIdAsync()
        {
            return _sessionStorage.GetItemAsync(Constants.Storage.StorageKeys.CurrentRouteId);
        }

        public Task SetCurrentRouteIdAsync(string routeId)
        {

            if (String.IsNullOrWhiteSpace(routeId))
                return null;

            return _sessionStorage.SetItemAsync(Constants.Storage.StorageKeys.CurrentRouteId, routeId);

        }

        [Obsolete("Please use GetCurrentRouteIdAsync")]
        public Route ContextRoute
        {
            get
            {
                return _sessionStorage.GetItem<Route>("contextRoute");
            }
        }


        //public Func<string, string> GetNavigationRootItemJson = (lang) => _sessionStorage.GetItem<string>("navigationRootItem_Json_" + lang);

        //internal Action<string, string> SetNavigationRootItemJson = (lang, json) => _sessionStorage.SetItem("navigationRootItem_Json_" + lang, json);





    }
}
