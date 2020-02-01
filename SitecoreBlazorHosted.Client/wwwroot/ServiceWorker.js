const baseURL = '/';
const indexURL = '/index.html';
const networkFetchEvent = 'fetch';
const swInstallEvent = 'install';
const swInstalledEvent = 'installed';
const swActivateEvent = 'activate';
const staticCachePrefix = 'blazor-cache-v';
const staticCacheName = 'blazor-cache-v1';
const requiredFiles = [
"/_framework/blazor.boot.json",
"/_framework/blazor.webassembly.js",
"/_framework/wasm/dotnet.js",
"/_framework/wasm/dotnet.wasm",
"/_framework/_bin/Feature.Identity.dll",
"/_framework/_bin/Feature.Identity.pdb",
"/_framework/_bin/Feature.Navigation.dll",
"/_framework/_bin/Feature.Navigation.pdb",
"/_framework/_bin/Feature.PageContent.dll",
"/_framework/_bin/Feature.PageContent.pdb",
"/_framework/_bin/Feature.Teasers.dll",
"/_framework/_bin/Feature.Teasers.pdb",
"/_framework/_bin/Foundation.BlazorExtensions.dll",
"/_framework/_bin/Foundation.BlazorExtensions.pdb",
"/_framework/_bin/Microsoft.AspNetCore.Blazor.dll",
"/_framework/_bin/Microsoft.AspNetCore.Components.dll",
"/_framework/_bin/Microsoft.AspNetCore.Components.Web.dll",
"/_framework/_bin/Microsoft.AspNetCore.Http.Abstractions.dll",
"/_framework/_bin/Microsoft.AspNetCore.Http.Features.dll",
"/_framework/_bin/Microsoft.Bcl.AsyncInterfaces.dll",
"/_framework/_bin/Microsoft.Extensions.Configuration.Abstractions.dll",
"/_framework/_bin/Microsoft.Extensions.Configuration.dll",
"/_framework/_bin/Microsoft.Extensions.DependencyInjection.Abstractions.dll",
"/_framework/_bin/Microsoft.Extensions.DependencyInjection.dll",
"/_framework/_bin/Microsoft.Extensions.Logging.Abstractions.dll",
"/_framework/_bin/Microsoft.Extensions.Primitives.dll",
"/_framework/_bin/Microsoft.JSInterop.dll",
"/_framework/_bin/Mono.Security.dll",
"/_framework/_bin/Mono.WebAssembly.Interop.dll",
"/_framework/_bin/mscorlib.dll",
"/_framework/_bin/netstandard.dll",
"/_framework/_bin/Project.BlazorSite.dll",
"/_framework/_bin/Project.BlazorSite.pdb",
"/_framework/_bin/SitecoreBlazorHosted.Client.dll",
"/_framework/_bin/SitecoreBlazorHosted.Client.pdb",
"/_framework/_bin/SitecoreBlazorHosted.Shared.dll",
"/_framework/_bin/SitecoreBlazorHosted.Shared.pdb",
"/_framework/_bin/System.Core.dll",
"/_framework/_bin/System.dll",
"/_framework/_bin/System.Net.Http.dll",
"/_framework/_bin/System.Runtime.CompilerServices.Unsafe.dll",
"/_framework/_bin/System.Runtime.Serialization.dll",
"/_framework/_bin/System.Text.Encodings.Web.dll",
"/_framework/_bin/System.Text.Json.dll",
"/_framework/_bin/WebAssembly.Bindings.dll",
"/_framework/_bin/WebAssembly.Net.Http.dll",
"/css/open-iconic/FONT-LICENSE",
"/css/open-iconic/font/css/open-iconic-bootstrap.min.css",
"/css/open-iconic/font/fonts/open-iconic.eot",
"/css/open-iconic/font/fonts/open-iconic.otf",
"/css/open-iconic/font/fonts/open-iconic.svg",
"/css/open-iconic/font/fonts/open-iconic.ttf",
"/css/open-iconic/font/fonts/open-iconic.woff",
"/css/open-iconic/ICON-LICENSE",
"/css/open-iconic/README.md",
"/css/site.css",
"/css/site.min.css",
"/data/routes/en.json",
"/data/routes/error/en.json",
"/data/routes/error/sv.json",
"/data/routes/habitat-stuff/carousels/en.json",
"/data/routes/habitat-stuff/carousels/sv.json",
"/data/routes/habitat-stuff/teasers/en.json",
"/data/routes/habitat-stuff/teasers/sv.json",
"/data/routes/sv.json",
"/data/routes/weather/en.json",
"/data/routes/weather/sv.json",
"/data/weather.json",
"/default-icon-512x512.png",
"/icons/icon-192x192.png",
"/icons/icon.png",
"/images/banner.jpg",
"/images/Habitat-004-wide.jpg",
"/images/Habitat-007-wide.jpg",
"/images/icon-192x192.png",
"/images/SBlazor.svg",
"/images/sc_logo.png",
"/index.html",
"/scripts/interop.js",
"/scripts/interop.min.js",
"/ServiceWorkerRegister.js",
"/manifest.json"
];
// * listen for the install event and pre-cache anything in filesToCache * //
self.addEventListener(swInstallEvent, event => {
    self.skipWaiting();
    event.waitUntil(
        caches.open(staticCacheName)
            .then(cache => {
                return cache.addAll(requiredFiles);
            })
    );
});
self.addEventListener(swActivateEvent, function (event) {
    event.waitUntil(
        caches.keys().then(function (cacheNames) {
            return Promise.all(
                cacheNames.map(function (cacheName) {
                    if (staticCacheName !== cacheName && cacheName.startsWith(staticCachePrefix)) {
                        return caches.delete(cacheName);
                    }
                })
            );
        })
    );
});
self.addEventListener(networkFetchEvent, event => {
    const requestUrl = new URL(event.request.url);
    if (requestUrl.origin === location.origin) {
        if (requestUrl.pathname === baseURL) {
            event.respondWith(caches.match(indexURL));
            return;
        }
    }
    event.respondWith(
        caches.match(event.request)
            .then(response => {
                if (response) {
                    return response;
                }
                return fetch(event.request)
                    .then(response => {
                        if (response.ok) {
                            if (requestUrl.origin === location.origin) {
                                caches.open(staticCacheName).then(cache => {
                                    cache.put(event.request.url, response);
                                });
                            }
                        }
                        return response.clone();
                    });
            }).catch(error => {
                console.error(error);
            })
    );
});
