using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivideAndConquer
{
    public class Select
    {
        public int MomSelect(int[] arr, int k)
        {
            int n = arr.Length;

            if (n <= 25)
            {
                Array.Sort(arr);
                return arr[k - 1];
            }
            else
            {
                int m = n / 5;
                int remainder = n % 5;
                int[] M;
                if (remainder == 0)
                    M = new int[m];
                else
                    M = new int[m + 1];
                int[] arrayOfFive = new int[5];

                for (int i = 0; i < m; i++)
                {
                    if (i == 0)
                        Array.Copy(arr, 0, arrayOfFive, 0, 5);
                    else
                        Array.Copy(arr, 5 * i, arrayOfFive, 0, 5);

                    M[i] = GetMedian(arrayOfFive);
                    arrayOfFive = new int[5];
                }

                if (remainder != 0)
                {
                    Array.Copy(arr, m * 5, arrayOfFive, 0, remainder);
                    while(remainder < 5)
                    {
                        arrayOfFive[remainder] = int.MaxValue;
                        remainder++;
                    }

                    M[m] = GetMedian(arrayOfFive);
                }


                int mom = MomSelect(M, M.Length / 2);

                int r = Partition(arr, mom);

                if (r == k)
                    return arr[r]-1;
                else if (k < r)
                {
                    int[] temp = new int[r];
                    Array.Copy(arr, 0, temp, 0, r);
                    return MomSelect(temp, k);
                }
                else
                {
                    int[] temp = new int[n - r];
                    Array.Copy(arr, r, temp, 0, n - r);
                    return MomSelect(temp, k - r);
                }
            }

        }

        private int Partition(int[] arr, int p)
        {
            int n = arr.Length;

            int j = 0;
            while (j < n)
            {
                if (arr[j] == p)
                    break;
                j++;
            }

            Swap(arr, j, n - 1);

            int l = 0;

            for (int i = 0; i < n; i++)
            {
                if (arr[i] < arr[n - 1])
                {
                    Swap(arr, l, i);
                    l++;
                }
            }
            Swap(arr, n - 1, l);

            return l;
        }

        private void Swap(int[] arr, int firstIndex, int secondIndex)
        {
            int temp = arr[firstIndex];
            arr[firstIndex] = arr[secondIndex];
            arr[secondIndex] = temp;
        }

        /// <summary>
        /// Returns the median of the array using selection sort.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private int GetMedian(int[] arr)
        {
            Array.Sort(arr);

            
            return arr[arr.Length / 2];
        }


        //*******************************************************************************************


        public int kthSmallest(int[] arr, int l,
                            int r, int k)
        {
            // If k is smaller than
            // number of elements in array
            if (k > 0 && k <= r - l + 1)
            {
                int n = r - l + 1; // Number of elements in arr[l..r]

                // Divide arr[] in groups of size 5,
                // calculate median of every group
                // and store it in median[] array.
                int i;

                // There will be floor((n+4)/5) groups;
                int[] median = new int[(n + 4) / 5];
                for (i = 0; i < n / 5; i++)
                    median[i] = findMedian(arr, l + i * 5, 5);

                // For last group with less than 5 elements
                if (i * 5 < n)
                {
                    median[i] = findMedian(arr, l + i * 5, n % 5);
                    i++;
                }

                // Find median of all medians using recursive call.
                // If median[] has only one element, then no need
                // of recursive call
                int medOfMed = (i == 1) ? median[i - 1] :
                                        kthSmallest(median, 0, i - 1, i / 2);

                // Partition the array around a random element and
                // get position of pivot element in sorted array
                int pos = partition(arr, l, r, medOfMed);

                // If position is same as k
                if (pos - l == k - 1)
                    return arr[pos];
                if (pos - l > k - 1) // If position is more, recur for left
                    return kthSmallest(arr, l, pos - 1, k);

                // Else recur for right subarray
                return kthSmallest(arr, pos + 1, r, k - pos + l - 1);
            }

            // If k is more than number of elements in array
            return int.MaxValue;
        }

        private int findMedian(int[] arr, int i, int n)
        {
            if (i <= n)
                Array.Sort(arr, i, n); // Sort the array
            else
                Array.Sort(arr, n, i);
            return arr[n / 2]; // Return middle element
        }

        // It searches for x in arr[l..r], and
        // partitions the array around x.
        private int partition(int[] arr, int l,
                                int r, int x)
        {
            // Search for x in arr[l..r] and move it to end
            int i;
            for (i = l; i < r; i++)
                if (arr[i] == x)
                    break;
            swap(arr, i, r);

            // Standard partition algorithm
            i = l;
            for (int j = l; j <= r - 1; j++)
            {
                if (arr[j] <= x)
                {
                    swap(arr, i, j);
                    i++;
                }
            }
            swap(arr, i, r);
            return i;
        }

        static int[] swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
            return arr;
        }
    }
}