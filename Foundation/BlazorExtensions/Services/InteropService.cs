using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class InteropService
    {
       
        public Task<bool> HardReload()
        {
            return JSRuntime.Current.InvokeAsync<bool>("blazorExtensions.hardReload");
        }



    }


}
