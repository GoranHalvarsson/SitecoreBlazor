using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitecoreBlazorHosted.Client.RouterExtended.Models
{
  public class RouterData
  {
    public string Path { get; set; }
    public string Page { get; set; }
    public List<RouterData> Children { get; set; }
  }

  public class RouterDataRoot
  {
    public List<RouterData> Routes { get; set; }
  }


}
