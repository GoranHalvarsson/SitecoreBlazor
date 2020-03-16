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


        public ValueTask<bool> HardReload()
        {
            return _jsRuntime.InvokeAsync<bool>("blazorExtensionsInterop.hardReload");
        }


        public ValueTask<string> SetPageTitle(string pageTitle)
        {
            return _jsRuntime.InvokeAsync<string>("blazorExtensionsInterop.setPageTitle", pageTitle);
        }

        public ValueTask NavigateToFragment(string elementId)
        {
            return _jsRuntime.InvokeVoidAsync("blazorExtensionsInterop.scrollToFragment", elementId);
        }


    }


}
