using System.Collections.Generic;

namespace Foundation.BlazorExtensions.CustomBlazorRouter
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
