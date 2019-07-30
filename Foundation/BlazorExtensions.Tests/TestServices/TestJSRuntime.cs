using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Tests.TestServices
{
    public class TestJSRuntime : IJSRuntime
    {
        public List<(string Identifier, object[] Args)> Invocations { get; }
            = new List<(string Identifier, object[] Args)>();

        public object NextInvocationResult { get; set; }

        public Task<TValue> InvokeAsync<TValue>(string identifier, params object[] args)
        {
            Invocations.Add((identifier, args));
            return (Task<TValue>)NextInvocationResult;
        }
    }
}
