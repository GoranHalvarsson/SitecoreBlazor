using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions
{
    internal struct Constants
    {
        internal struct Storage
        {
            internal struct StorageKeys
            {
                internal const string ContextLanguage = "contextLanguage";
                internal const string CurrentRouteId = "currentRouteId";

            }

           
        }

        internal struct Route
        {
             internal const string RouteError = "error=true";
        }
    }
}
