# Time travel into the future - BLAZOR + SITECORE
<a href="https://visionsincode.files.wordpress.com/2018/06/hackerman.gif"><img data-attachment-id="3491" data-permalink="https://visionsincode.wordpress.com/?attachment_id=3491" data-orig-file="https://visionsincode.files.wordpress.com/2018/06/hackerman.gif?w=748" data-orig-size="480,270" data-comments-opened="1" data-image-meta="{&quot;aperture&quot;:&quot;0&quot;,&quot;credit&quot;:&quot;&quot;,&quot;camera&quot;:&quot;&quot;,&quot;caption&quot;:&quot;&quot;,&quot;created_timestamp&quot;:&quot;0&quot;,&quot;copyright&quot;:&quot;&quot;,&quot;focal_length&quot;:&quot;0&quot;,&quot;iso&quot;:&quot;0&quot;,&quot;shutter_speed&quot;:&quot;0&quot;,&quot;title&quot;:&quot;&quot;,&quot;orientation&quot;:&quot;0&quot;}" data-image-title="Hackerman" data-image-description="" data-medium-file="https://visionsincode.files.wordpress.com/2018/06/hackerman.gif?w=748?w=300" data-large-file="https://visionsincode.files.wordpress.com/2018/06/hackerman.gif?w=748?w=480" src="https://visionsincode.files.wordpress.com/2018/06/hackerman.gif?w=480" alt="" class="aligncenter size-full wp-image-3491" width="480" height="270" srcset="https://visionsincode.files.wordpress.com/2018/06/hackerman.gif?w=480&amp;zoom=2 1.5x" src-orig="https://visionsincode.files.wordpress.com/2018/06/hackerman.gif?w=748" scale="1.5"></a>

> [Let’s get going, we are gone hack this 3.5 inch floppy disc to the year 2005](https://www.youtube.com/watch?v=KEkrWRHCDQU&amp;feature=youtu.be&amp;t=66)

For those still wondering what BLAZOR is, check out this great post – [WHAT IS BLAZOR?](https://learn-blazor.com/getting-started/what-is-blazor/ )

This repo allows you to run Sitecore app's client-side. We are also following the [HELIX concept](http://helix.sitecore.net/), the clean way.

Live: https://visionsincode.github.io/SitecoreBlazor.io/

## Setup BLAZOR
To get started with Blazor and build your first Blazor web app check out [Blazor's getting started guide](https://blazor.net/docs/get-started.html).

## Setup solution
Clone or fork this repo, build it and be happy 🙂

## UPDATE! Application can now run server/client -side
Go to Startup.cs(in SitecoreBlazorHosted.Server), locate //For server-side and //For client-side.
```csharp
public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression();
            services.AddHttpClient();

             //For server-side
            services.AddRazorComponents<Client.Startup>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });


            //For client-side
            //app.UseBlazor<Client.Startup>();
            //app.UseBlazorDebugging();

            
            //For server-side
            app.UseStaticFiles();
            app.UseRazorComponents<Client.Startup>();
        }
    }
        
```
In index.cshtml(in SitecoreBlazorHosted.Client) you need to set what javascript to use:
- Server-side: _framework/blazor.server.js
- Client-side: _framework/blazor.webassembly.js
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width">
    <title>Sitecore Blazor</title>
    <base href="/" />
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="css/site.css" rel="stylesheet" />
</head>
<body>
    <app><img src="css/SBlazor.svg" /></app>

    <script type="text/javascript" src="blazor.polyfill.min.js"></script>

    <!--<script src="_framework/blazor.webassembly.js"></script>-->
    <script src="_framework/blazor.server.js"></script>
</body>
</html>        
```

## Blog posts
[Server-side is dead, long live client-side! BLAZOR + Sitecore = a match made in heaven](https://visionsincode.wordpress.com/2018/05/13/server-side-is-dead-long-live-client-side-blazor-sitecore-a-match-made-in-heaven/)

[Time travel into the future – BLAZOR + SITECORE + HELIX](https://visionsincode.wordpress.com/2018/06/30/time-travel-into-the-future-blazor-sitecore-helix/)
