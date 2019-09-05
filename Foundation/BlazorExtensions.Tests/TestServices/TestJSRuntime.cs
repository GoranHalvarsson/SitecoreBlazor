using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Tests.TestServices
{
    public class TestJSRuntime : IJSRuntime
    {
        public List<(string Identifier, object[] Args)> Invocations { get; }
            = new List<(string Identifier, object[] Args)>();

        public object NextInvocationResult { get; set; }


        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object[] args)
        {
            Invocations.Add((identifier, args.ToArray()));
            return (ValueTask<TValue>)NextInvocationResult;
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object[] args)
        {
            Invocations.Add((identifier, args));
            return (ValueTask<TValue>)NextInvocationResult;
        }
    }
}
