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
        /// Returns the weighted median. 
        /// </summary>
        /// <param name="S">Represents the set of "IDs"</param>
        /// <param name="W">The weight of each ID</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int WeightedMedian(int[] S, int[]W)
        {
            int sum = Sum(W, 0, W.Length);
            return WeightedSelect(S, W, sum / 2, sum / 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="S">Represents the set of "IDs"</param>
        /// <param name="L">Return value must be less than or equal to this number</param>
        /// <param name="G">Return value must be less than or equal to this number</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private int WeightedSelect(int[] S, int[] W, int L, int G)
        {
            int n = S.Length;
            // Use MomSelect to find the median of the set.
            int k = (n % 2 == 0) ? n / 2 : n / 2 + 1;
            int medianOfS = MomSelect(S, k);

           

            // Find index of median
            int j = 0;
            while (j < n)
            {
                if (S[j] == medianOfS)
                    break;
                j++;
            }
            SwapBoth(S, W, j, n - 1);

            // Split the array into two halfs based on the weight.
            int l = 0;
            int pivotIndex = 0;
            for (int i = 0; i < n; i++)
            {
                if (S[i] < S[n - 1])
                {
                    SwapBoth(S, W, l, i);
                    l++;
                    SwapBoth(S, W, pivotIndex, i);
                    pivotIndex++;
                }
            }

            // Put the median into its true median index.
            // l now represents the new index for the median.
            SwapBoth(S, W, n - 1, l);


            int[] leftS = new int[pivotIndex];
            int[] leftW = new int[pivotIndex];
            int[] rightS = new int[n - pivotIndex-1];
            int[] rightW = new int[n - pivotIndex-1];
            Array.Copy(S, 0, leftS, 0, pivotIndex);
            Array.Copy(W, 0, leftW, 0, pivotIndex);
            Array.Copy(S, pivotIndex+1, rightS, 0, n - pivotIndex-1);
            Array.Copy(W, pivotIndex+1, rightW, 0, n - pivotIndex-1);

            int sumLessThanM = Sum(W, 0, pivotIndex);
            int sumGreaterThanM = Sum(W, pivotIndex + 1, W.Length);

            // Check either sub array for the weighted median or return the median if it has already been found. 
            if (sumLessThanM <= L && sumGreaterThanM <= G)
                return S[pivotIndex];
            else if (sumLessThanM > L)
                return WeightedSelect(leftS, leftW, L, G - W[pivotIndex] - sumGreaterThanM);
            else
                return WeightedSelect(rightS, rightW, L - W[pivotIndex] - sumLessThanM, G);
            
        }


        /// <summary>
        /// Calculate the sum of weights in an array for a given range.
        /// </summary>
        /// <param name="weights"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        private int Sum(int[] weights, int startIndex, int endIndex)
        {
            int sum = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                sum += weights[i];
            }
            return sum;
        }

        /// <summary>
        /// Swaps both elements in two arrays. This keeps the weight in the same index 
        /// as it's corisponding "ID"
        /// </summary>
        /// <param name="S"></param>
        /// <param name="W"></param>
        /// <param name="firstIndex"></param>
        /// <param name="secondIndex"></param>
        private void SwapBoth(int[] S, int[] W, int firstIndex, int secondIndex)
        {
            int temp = S[firstIndex];
            S[firstIndex] = S[secondIndex];
            S[secondIndex] = temp;

            temp = W[firstIndex];
            W[firstIndex] = W[secondIndex];
            W[secondIndex] = temp;
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
                int[] temp = new int[arr.Length];
                arr.CopyTo(temp, 0);
                Array.Sort(temp);
                return temp[k - 1];
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