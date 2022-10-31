using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sorting;

namespace SortingTests
{
    [TestClass]
    public class MergeSort
    {
        const int STRESSTESTSIZE = 5000;

        [TestMethod]
        public void TestSimpleMerge()
        {
            SortingAlgorithms merge = new SortingAlgorithms();
            int[] arrUnsorted = new int[] { 5, 3, 6, 4, 2, 1, 8, 7, 9 };
            int[] expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int[] sorted = merge.MergeSort(arrUnsorted, 0, arrUnsorted.Length - 1);

            for(int i = 0; i < sorted.Length; i++)
            {
                Assert.AreEqual(expected[i], sorted[i]);
            }
        }

        /// <summary>
        /// This illustrates the worst case runtime for mergesort. Since the entire array
        /// is sorted in reverse order. This means each time merge is called, every index 
        /// will have to be shifted.
        /// </summary>
        [TestMethod]
        public void TestMergeSortReverseOrder()
        {
            SortingAlgorithms merge = new SortingAlgorithms();
            int[] arrUnsorted = new int[] { 9,8,7,6,5,4,3,2,1 };
            int[] expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int[] sorted = merge.MergeSort(arrUnsorted, 0, arrUnsorted.Length - 1);

            for (int i = 0; i < sorted.Length; i++)
            {
                Assert.AreEqual(expected[i], sorted[i]);
            }
        }

        [TestMethod]
        public void StressTestMergeSort()
        {
            SortingAlgorithms merge = new SortingAlgorithms();
            Random r = new Random();

            int tempRandomVal;
            int[] arrUnsorted = new int[STRESSTESTSIZE];
            int[] expected = new int[STRESSTESTSIZE];

            for(int j = 0; j < STRESSTESTSIZE; j++)
            {
                tempRandomVal = r.Next(STRESSTESTSIZE);
                arrUnsorted[j] = tempRandomVal;
                expected[j] = tempRandomVal;
            }

            Array.Sort(expected);

            int[] sorted = merge.MergeSort(arrUnsorted, 0, arrUnsorted.Length - 1);

            for (int i = 0; i < sorted.Length; i++)
            {
                Assert.AreEqual(expected[i], sorted[i]);
            }
        }
    }
}
