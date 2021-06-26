using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            while (right - left > 1)
            {
                var middle = left + (right - left) / 2;
                if (string.Compare(phrases[middle], prefix) >= 0 && !phrases[middle].StartsWith(prefix))
                    right = middle;
                else
                    left = middle;
            }

            return right;
        }
    }
}