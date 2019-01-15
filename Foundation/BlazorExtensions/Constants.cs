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

            internal struct MethodNames
            {
                public const string LENGTH_METHOD = "BlazorExtensions.Storage.Length";
                public const string KEY_METHOD = "BlazorExtensions.Storage.Key";
                public const string GET_ITEM_METHOD = "BlazorExtensions.Storage.GetItem";
                public const string SET_ITEM_METHOD = "BlazorExtensions.Storage.SetItem";
                public const string REMOVE_ITEM_METHOD = "BlazorExtensions.Storage.RemoveItem";
                public const string CLEAR_METHOD = "BlazorExtensions.Storage.Clear";
            }

            internal struct StorageTypeNames
            {
                public const string SESSION_STORAGE = "sessionStorage";
                public const string LOCAL_STORAGE = "localStorage";
            }
        }
    }
}
