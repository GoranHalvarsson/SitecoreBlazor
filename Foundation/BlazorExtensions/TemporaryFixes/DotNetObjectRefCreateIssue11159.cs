using Microsoft.JSInterop;

namespace Foundation.BlazorExtensions.TemporaryFixes
{
    //public class DotNetObjectRefCreateIssue11159
    //{
    //    private IJSRuntime JsRuntime { get; set; }

    //    public DotNetObjectRefCreateIssue11159(IJSRuntime jsRuntime)
    //    {
    //        JsRuntime = jsRuntime;
    //    }

    //    private static object CreateDotNetObjectRefSyncObj = new object();

    //    public DotNetObjectRef<T> CreateDotNetObjectRef<T>(T value) where T : class
    //    {
    //        lock (CreateDotNetObjectRefSyncObj)
    //        {
    //            JSRuntime.SetCurrentJSRuntime(JsRuntime);
    //            return DotNetObjectRef.Create(value);
    //        }
    //    }

    //    public void DisposeDotNetObjectRef<T>(DotNetObjectRef<T> value) where T : class
    //    {
    //        if (value != null)
    //        {
    //            lock (CreateDotNetObjectRefSyncObj)
    //            {
    //                JSRuntime.SetCurrentJSRuntime(JsRuntime);
    //                value.Dispose();
    //            }
    //        }
    //    }
    //}
}
