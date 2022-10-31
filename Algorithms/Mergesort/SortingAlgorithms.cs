using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class SortingAlgorithms
    {

        /// <summary>
        /// This method will preform mergesort inplace. 
        /// 
        /// </summary>
        /// <param name="arr"> The array to be sorted</param>
        /// <param name="left"> The start index </param>
        /// <param name="right"> The end index (size-1) </param>
        /// <returns></returns>
        public int[] MergeSort(int[] arr, int left, int right)
        {
            // This is insuring that the arrry length is never less than 1.
            if(left < right) {
                // Get the middle by getting the length and dividing by 2.
                int mid = left + (right - left) / 2;

                // Recursively call the left side of the arry to the midpoint.
                MergeSort(arr, left, mid);

                // Recursively call the right side of the array from the midpoint to the end.
                MergeSort(arr, mid + 1, right);

                Merge(arr, left, right, mid);
            }
            return arr;
        }

        private void Merge(int[] arr, int left, int right, int mid)
        {
            int secondStart = mid + 1;

            if (arr[mid] <= arr[secondStart])
                return;

            while(left <= mid && secondStart <= right)
            {
                if (arr[left] <= arr[secondStart])
                    left++;
                else
                {
                    int value = arr[secondStart];
                    int index = secondStart;

                    while(index != left)
                    {
                        arr[index] = arr[index - 1];
                        index--;
                    }
                    arr[left] = value;

                    left++;
                    mid++;
                    secondStart++;
                }
            }
        }
    }
}
