/// Date: 11/4/2022
/// Author: Travis Slade
/// Notes:
///         This class is and was only used as a means to learn selection algorithms.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivideAndConquer
{
    /// <summary>
    /// A class dedicated to different selection algorithms.
    /// </summary>
    public class Select
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="S">Represents the set of "IDs"</param>
        /// <param name="W">The weight of each ID</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int WeightedMedian(int[] S, int[]W)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Median of Medians selection algorithm.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int MomSelect(int[] arr, int k)
        {
            int n = arr.Length;

            // If the desired element isn't in the range, exit.
            if (k > n || k == 0)
                return 0;

            // for n smaller than 26, brute force is fastest.
            if (n <= 25)
            {
                Array.Sort(arr);
                return arr[k - 1];
            }
            else
            {
                // m = num of full groups of 5. Remainder is how many elements make
                // up extra group of less than 5.
                int m = n / 5;
                int remainder = n % 5;
                int[] M;
                if (remainder == 0)
                    M = new int[m];
                else
                    M = new int[m + 1];

                int[] arrayOfFive = new int[5];

                // compute the median of each group of 5 and store it in M
                for (int i = 0; i < m; i++)
                {
                    if (i == 0)
                        Array.Copy(arr, 0, arrayOfFive, 0, 5);
                    else
                        Array.Copy(arr, 5 * i, arrayOfFive, 0, 5);

                    M[i] = GetMedian(arrayOfFive);
                    arrayOfFive = new int[5];
                }

                // Handle the left over elements. Fill the group of 5 with infinity.
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

                // Recursivly compute the median of the medians
                int mom = MomSelect(M, M.Length / 2);

                // Partition the array and recursively check the side that contains the desired element. 
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

        /// <summary>
        /// Helper method to partion array at a given pivot.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="p">Pivot used to partition</param>
        /// <returns></returns>
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
    }
}