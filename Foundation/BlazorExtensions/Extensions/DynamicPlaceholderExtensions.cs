using System;

namespace Foundation.BlazorExtensions.Extensions
{
  public static class DynamicPlaceholderExtensions
  {

    public static string ExtractPlaceholderName(this string placeholderName)
    {
      if (placeholderName.Length <= 36)
        return placeholderName;

      string possibleGuid = placeholderName.Substring(placeholderName.Length - 36, 36);

      if (IsValidGuid(possibleGuid))
        return placeholderName.Substring(0, placeholderName.Length - 37);


      return placeholderName;

    }

    public static bool IsDynamicPlaceholder(this string placeholderName)
    {
      if (placeholderName.Length <= 36)
        return false;

      string possibleGuid = placeholderName.Substring(placeholderName.Length - 36, 36);
      return IsValidGuid(possibleGuid);
    }


    private static bool IsValidGuid(string str)
    {
      Guid guid;
      return Guid.TryParse(str, out guid);
    }





  }
}
