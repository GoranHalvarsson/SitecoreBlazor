

using Microsoft.JSInterop;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions
{
    //!TODO FIX with cookies 

    public class BlazorContext
    {
        private readonly SessionStorage _sessionStorage;

       
        public BlazorContext(SessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

       

        public Task<string> GetContextLanguageAsync(IJSRuntime jsRuntime)
        {
            return _sessionStorage.GetItemAsync(Constants.Storage.StorageKeys.ContextLanguage,jsRuntime);
        }

        public  Task SetContextLanguageAsync(string language,IJSRuntime jsRuntime)
        {

            if (String.IsNullOrWhiteSpace(language))
                return null;

             return _sessionStorage.SetItemAsync(Constants.Storage.StorageKeys.ContextLanguage, language,jsRuntime);

        }

        public Task<string> GetCurrentRouteIdAsync(IJSRuntime jsRuntime)
        {
            return _sessionStorage.GetItemAsync(Constants.Storage.StorageKeys.CurrentRouteId,jsRuntime);
        }

        public Task SetCurrentRouteIdAsync(string routeId,IJSRuntime jsRuntime)
        {

            if (String.IsNullOrWhiteSpace(routeId))
                return null;

            return _sessionStorage.SetItemAsync(Constants.Storage.StorageKeys.CurrentRouteId, routeId,jsRuntime);

        }


        public Task<IList<Tuple<DateTime, string, Route>>> GetNavigatedRouteAsync(IJSRuntime jsRuntime)
        {
            return _sessionStorage.GetItemAsync<IList<Tuple<DateTime, string, Route>>>(Constants.Storage.StorageKeys.NavigatedRoutes, jsRuntime);
        }

        public Task SetCurrentNavigatedRoutesAsync(IList<Tuple<DateTime, string, Route>> navigatedRoutes, IJSRuntime jsRuntime)
        {

            if (navigatedRoutes == null)
                return null;

            return _sessionStorage.SetItemAsync<IList<Tuple<DateTime, string, Route>>>(Constants.Storage.StorageKeys.NavigatedRoutes, navigatedRoutes, jsRuntime);

        }


        //public Func<string, string> GetNavigationRootItemJson = (lang) => _sessionStorage.GetItem<string>("navigationRootItem_Json_" + lang);

        //internal Action<string, string> SetNavigationRootItemJson = (lang, json) => _sessionStorage.SetItem("navigationRootItem_Json_" + lang, json);





    }
}
