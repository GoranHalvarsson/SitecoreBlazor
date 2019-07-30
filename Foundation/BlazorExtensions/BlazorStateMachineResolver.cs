

using Microsoft.JSInterop;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions
{
    //!TODO FIX with cookies 

    public class BlazorStateMachineResolver
    {
        private readonly SessionStorage _sessionStorage;
        private readonly IJSRuntime _jsRuntime;



        public BlazorStateMachineResolver(SessionStorage sessionStorage)
        {

            if (sessionStorage == null)
            {
                throw new ArgumentException("The value cannot be null", nameof(sessionStorage));
            }

            _sessionStorage = sessionStorage;
        }


        public BlazorStateMachineResolver(SessionStorage sessionStorage, IJSRuntime jsRuntime) : this(sessionStorage)
        {

            if (jsRuntime == null)
            {
                throw new ArgumentException("The value cannot be null", nameof(jsRuntime));
            }

            _jsRuntime = jsRuntime;
        }

        public Task<string> GetContextLanguageAsync()
        {
            return _sessionStorage.GetItemAsync(Constants.Storage.StorageKeys.ContextLanguage, _jsRuntime);
        }

        public Task<string> GetContextLanguageAsync(IJSRuntime jsRuntime)
        {
            if (jsRuntime == null)
                return null;


            return _sessionStorage.GetItemAsync(Constants.Storage.StorageKeys.ContextLanguage,jsRuntime);
        }

        public Task SetContextLanguageAsync(string language)
        {

            if (String.IsNullOrWhiteSpace(language))
                return null;

            return _sessionStorage.SetItemAsync(Constants.Storage.StorageKeys.ContextLanguage, language, _jsRuntime);

        }


        public Task SetContextLanguageAsync(string language,IJSRuntime jsRuntime)
        {

            if (String.IsNullOrWhiteSpace(language))
                return null;

             return _sessionStorage.SetItemAsync(Constants.Storage.StorageKeys.ContextLanguage, language,jsRuntime);

        }

        public Task<string> GetCurrentRouteIdAsync()
        {
            return _sessionStorage.GetItemAsync(Constants.Storage.StorageKeys.CurrentRouteId, _jsRuntime);
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

        public Task SetCurrentRouteIdAsync(string routeId)
        {

            if (String.IsNullOrWhiteSpace(routeId))
                return null;

            return _sessionStorage.SetItemAsync(Constants.Storage.StorageKeys.CurrentRouteId, routeId, _jsRuntime);

        }


        public Task<IList<Tuple<DateTime, string, Route>>> GetNavigatedRouteAsync(IJSRuntime jsRuntime)
        {
            return _sessionStorage.GetItemAsync<IList<Tuple<DateTime, string, Route>>>(Constants.Storage.StorageKeys.NavigatedRoutes, jsRuntime);
        }

        public Task<IList<Tuple<DateTime, string, Route>>> GetNavigatedRouteAsync()
        {
            return _sessionStorage.GetItemAsync<IList<Tuple<DateTime, string, Route>>>(Constants.Storage.StorageKeys.NavigatedRoutes, _jsRuntime);
        }

        public Task SetCurrentNavigatedRoutesAsync(IList<Tuple<DateTime, string, Route>> navigatedRoutes, IJSRuntime jsRuntime)
        {

            if (navigatedRoutes == null)
                return null;

            return _sessionStorage.SetItemAsync<IList<Tuple<DateTime, string, Route>>>(Constants.Storage.StorageKeys.NavigatedRoutes, navigatedRoutes, jsRuntime);

        }

        public Task SetCurrentNavigatedRoutesAsync(IList<Tuple<DateTime, string, Route>> navigatedRoutes)
        {

            if (navigatedRoutes == null)
                return null;

            return _sessionStorage.SetItemAsync<IList<Tuple<DateTime, string, Route>>>(Constants.Storage.StorageKeys.NavigatedRoutes, navigatedRoutes, _jsRuntime);

        }


        //public Func<string, string> GetNavigationRootItemJson = (lang) => _sessionStorage.GetItem<string>("navigationRootItem_Json_" + lang);

        //internal Action<string, string> SetNavigationRootItemJson = (lang, json) => _sessionStorage.SetItem("navigationRootItem_Json_" + lang, json);





    }
}
