using Microsoft.AspNetCore.Components.Electron;

namespace SitecoreBlazorHosted.Electron
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ComponentsElectron.Run<Startup>("wwwroot/index.html");
        }
    }
}
