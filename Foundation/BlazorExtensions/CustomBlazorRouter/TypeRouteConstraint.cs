namespace Foundation.BlazorExtensions.CustomBlazorRouter
{
  /// <summary>
  /// A route constraint that requires the value to be parseable as a specified type.
  /// </summary>
  /// <typeparam name="T">The type to which the value must be parseable.</typeparam>
  internal class TypeRouteConstraint<T> : RouteConstraint
  {
    public delegate bool TryParseDelegate(string str, out T result);

    private readonly TryParseDelegate _parser;

    public TypeRouteConstraint(TryParseDelegate parser)
    {
      _parser = parser;
    }

    public override bool Match(string pathSegment, out object convertedValue)
    {
      if (_parser(pathSegment, out var result))
      {
        convertedValue = result;
        return true;
      }
      else
      {
        convertedValue = null;
        return false;
      }
    }
  }
}
