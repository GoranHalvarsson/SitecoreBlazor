using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class BlazorExtensionsInteropService
    {
        private readonly IJSRuntime _jsRuntime;

        
        public BlazorExtensionsInteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }


        public Task<bool> HardReload()
        {
            return _jsRuntime.InvokeAsync<bool>("blazorExtensionsInterop.hardReload");
        }


        public Task<string> SetPageTitle(string pageTitle)
        {
            return _jsRuntime.InvokeAsync<string>("blazorExtensionsInterop.setPageTitle", pageTitle);
        }


    }


}
