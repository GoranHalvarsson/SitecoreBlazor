using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class InteropService
    {
        public Task<bool> HardReload(IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<bool>("blazorExtensions.hardReload");
        }



    }


}
