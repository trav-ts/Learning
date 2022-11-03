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
                return GetMedian(arr);
            else
            {
                int m = n / 5;
                int[] M = new int[m];
                int[] arrayOfFive = new int[5];

                for(int i = 0; i < m; i++)
                {
                    if(i == 0)
                        Array.Copy(arr, 0, arrayOfFive, 0, 5);
                    else
                        Array.Copy(arr, 5 * i, arrayOfFive, 0, 5);

                    M[i] = GetMedian(arrayOfFive);
                    arrayOfFive = new int[5];
                }

                int mom = MomSelect(M, m / 2);

                int r = Partition(arr, mom);

                if (k < r)
                {
                    int[] temp = new int[r];
                    Array.Copy(arr, 0, temp, 0, r - 2);
                    return MomSelect(temp, k);
                }
                else if (k > r)
                {
                    int[] temp = new int[r];
                    Array.Copy(arr, r, temp, 0, n - 1);
                    return MomSelect(temp, k - r - 1);
                }
                else
                    return mom;
                    
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

            for(int i = 0; i < n; i++)
            {
                if (arr[i] <= arr[n-1])
                {
                    Swap(arr, l, i);
                    l++;
                }
            }
            Swap(arr, n-1, l + 1);

            return l + 1;
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

            // If the length of the array is even, take the left element. 
            if (arr.Length % 2 == 0)
                return arr[arr.Length / 2 - 1];
            else
                return arr[arr.Length / 2];
        }
    }
}
