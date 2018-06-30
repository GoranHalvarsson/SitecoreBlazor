using System;
using System.Collections.Generic;

namespace Foundation.BlazorExtensions.CustomBlazorRouter
{
  internal class RouteEntry
  {
    public RouteEntry(RouteTemplate template, Type handler)
    {
      Template = template;
      Handler = handler;
    }

    public RouteTemplate Template { get; }

    public Type Handler { get; }

    internal void Match(RouteContext context)
    {
      if (Template.Segments.Length != context.Segments.Length)
      {
        return;
      }

      // Parameters will be lazily initialized.
      IDictionary<string, object> parameters = null;
      for (int i = 0; i < Template.Segments.Length; i++)
      {
        var segment = Template.Segments[i];
        var pathSegment = context.Segments[i];
        if (!segment.Match(pathSegment, out var matchedParameterValue))
        {
          return;
        }
        else
        {
          if (segment.IsParameter)
          {
            GetParameters()[segment.Value] = matchedParameterValue;
          }
        }
      }

      context.Parameters = parameters;
      context.Handler = Handler;

      IDictionary<string, object> GetParameters()
      {
        if (parameters == null)
        {
          parameters = new Dictionary<string, object>();
        }

        return parameters;
      }
    }
  }
}
