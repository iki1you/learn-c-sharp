using System;
using System.Collections.Generic;


namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (right == 0)
                return right;
            left++;
            right--;
            while (left < right)
            {
                int medium = (right + left) / 2;
                if (string.Compare(prefix, phrases[medium], StringComparison.OrdinalIgnoreCase) <= 0
                     && !phrases[medium].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    right = medium;
                else
                    left = medium + 1;
            }
            if (string.Compare(prefix, phrases[right], StringComparison.OrdinalIgnoreCase) <= 0
                && !phrases[left].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return right;
            return right + 1;
        }
    }
}

// static int FindIndexByBinarySearch(int[] array, int element)
// {
//     var left = 0;
//     var right = array.Length - 1;
//     while (left < right)
//     {
//         var middle = (right + left) / 2;
//         if (element <= array[middle])
//             right = middle;
//         else left = middle + 1;
//     }
//     if (array[right] == element)
//         return right;
//     return -1;
// }