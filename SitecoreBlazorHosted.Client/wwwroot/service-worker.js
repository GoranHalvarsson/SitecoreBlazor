const cacheName = 'offline-cache-16';

//var filesToCache = [
//    '/',
//    //Html and css files
//    '/index.html',
//    '/css/site.css',
//    '/css/bootstrap/bootstrap.min.css',
//    '/css/open-iconic/font/css/open-iconic-bootstrap.min.css',
//    '/open-iconic',
//    '/css/open-iconic/font/fonts/open-iconic.woff',
//    '/css/loading.css',
//    //Blazor framework
//    '_framework/blazor.webassembly.js',
//    '/_framework/blazor.boot.json',
//    //Our additional files
//    '/manifest.json',
//    '/serviceworker.js',
//    '/icons/icon-192x192.png',
//    '/icons/icon-512x512.png',
//    //The web assembly/.net dll's
//    '/_framework/wasm/mono.js',
//    '/_framework/wasm/mono.wasm',
//    '/_framework/_bin/Microsoft.AspNetCore.Blazor.Browser.dll',
//    '/_framework/_bin/Microsoft.AspNetCore.Blazor.dll',
//    '/_framework/_bin/Microsoft.Extensions.DependencyInjection.Abstractions.dll',
//    '/_framework/_bin/Microsoft.Extensions.DependencyInjection.dll',
//    '_framework/_bin/Microsoft.JSInterop.dll',
//    '/_framework/_bin/mscorlib.dll',
//    '/_framework/_bin/System.Net.Http.dll',
//    '/_framework/_bin/Mono.WebAssembly.Interop.dll',
//    '/_framework/_bin/System.dll',
//    '/_framework/_bin/System.Core.dll',
//    //Pages
//    '/counter',
//    //The compiled project .dll's
//    '/_framework/_bin/DotnetPwaSample.dll'
//];

self.addEventListener('install', async event => {
    console.log('Installing service worker...');
    await Promise.all((await caches.keys()).map(key => caches.delete(key)));
    self.skipWaiting();
});

self.addEventListener('fetch', event => {
    event.respondWith(getPossiblyCachedResponse(event.request));
});

async function getPossiblyCachedResponse(request) {
    const isInitialPageLoad = request.mode === 'navigate'
        || request.method === 'GET' && request.headers.get('accept').indexOf('text/html') > -1;
    if (isInitialPageLoad) {
        console.log('Overridding URL');
    }

    const cachedResponse = await caches.match(isInitialPageLoad ? '/' : request);
    if (cachedResponse) {
        console.log('Found ', request.url, ' in cache');
        return cachedResponse;
    } else {
        console.log('Network request for ', request.url);
        const response = await fetch(request);

        const cache = await caches.open(cacheName);
        cache.put(request.url, response.clone());

        return response;
    }
}
