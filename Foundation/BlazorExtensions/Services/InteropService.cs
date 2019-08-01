using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class InteropService
    {
        private readonly IJSRuntime _jsRuntime;

        public InteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }


        public Task<bool> HardReload()
        {
            return _jsRuntime.InvokeAsync<bool>("blazorExtensions.hardReload");
        }


        public Task<string> SetPageTitle(string pageTitle)
        {
            return _jsRuntime.InvokeAsync<string>("blazorExtensions.setPageTitle", pageTitle);
        }



    }


}
