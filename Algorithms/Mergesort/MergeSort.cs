using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mergesort
{
    internal class MergeSort
    {

        /// <summary>
        /// This method will preform mergesort inplace. 
        /// 
        /// </summary>
        /// <param name="arr"> The array to be sorted</param>
        /// <param name="left"> The start index </param>
        /// <param name="right"> The end index </param>
        /// <returns></returns>
        private int[] Sort(int[] arr, int left, int right)
        {
            // This is insuring that the arrry length is never less than 1.
            if(left < right) {
                // Get the middle by getting the length and dividing by 2.
                int mid = left + (right - left) / 2;

                // Recursively call the left side of the arry to the midpoint.
                Sort(arr, left, mid);

                // Recursively call the right side of the array from the midpoint to the end.
                Sort(arr, mid + 1, right);

                return Merge(arr, left, right, mid);
            }
            return arr;
        }

        private int[] Merge(int[] arr, int left, int right, int mid)
        {
            int i = 1, j = mid + 1, n;
            int[] temp = new int[left.Length + right.Length];
            
            if(left.Length > right.Length) 
                n = left.Length;
            else
                n = right.Length;

            for (int k = 1; k < n; k++)
            {
                if (j > n)
                {
                    temp[k] = left[i];
                    i = j + 1;
                }
                else if(i> mid)
                {
                    temp[k] = left[j]
                }
            }

            throw new NotImplementedException();
        }
    }
}
