using SitecoreBlazorHosted.Shared.Models;
using System.Collections.Generic;

namespace Foundation.BlazorExtensions.Extensions
{
  public static class PlaceholderExtensions
  {
    public static IEnumerable<KeyValuePair<string, IList<Placeholder>>> FlattenPlaceholders(this Dictionary<string, IList<Placeholder>> root)
    {
      var stack = new Stack<KeyValuePair<string, IList<Placeholder>>>();

      foreach (var top in root)
        stack.Push(top);

      ReverseStack(stack);

      while (stack.Count > 0)
      {
        var current = stack.Pop();
        yield return current;

        if (current.Value == null)
          continue;

        foreach (var child in current.Value)
        {

          if (child.Placeholders == null)
            continue;

          foreach (var keyVal in child.Placeholders)
          {
            stack.Push(keyVal);
          }

        }
      }

      void ReverseStack<T>(Stack<T> reverseStack)
      {
        for (int i = 0; i < reverseStack.Count; i++)
        {
          T targetElement = reverseStack.Pop();
          ReverseStackStep(reverseStack, reverseStack.Count - i, 0, targetElement);
        }
      }

      void ReverseStackStep<T>(Stack<T> reverseStackStep, int limit, int currentLevel, T targetElement)
      {
        if (currentLevel == limit)
        {
          reverseStackStep.Push(targetElement);
        }
        else
        {
          T element = reverseStackStep.Pop();
          ReverseStackStep(reverseStackStep, limit, currentLevel + 1, targetElement);
          reverseStackStep.Push(element);
        }
      }
    }
  }
}
