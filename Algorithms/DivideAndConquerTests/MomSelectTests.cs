using DivideAndConquer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DivideAndConquerTests
{
    [TestClass]
    public class MomSelectTests
    {
        private void Shuffle(int[] arr)
        {
            Random rng = new Random();
            int n = arr.Length;

            while (n > 1)
            {
                int k = rng.Next(n--);
                int temp = arr[n];
                arr[n] = arr[k];
                arr[k] = temp;
            }
        }
        [TestMethod]
        public void TestMethod1()
        {
            int testSize = 500;
            Select s = new Select();
            int expected;
            int[] arr = new int[testSize];

            for (int i = 0; i < testSize; i++)
            {
                arr[i] = i + 1;
            }

            int result;

            for(int i = 1; i <= testSize; i++)
            {
                expected = i;
                Shuffle(arr);
                result = s.MomSelect(arr, i);
                //result = s.kthSmallest(arr, 0, arr.Length-1, i);
                Assert.AreEqual(expected, result);
            }
        }


        [TestMethod]
        public void TestMethod2()
        {
            Select s = new Select();
            int expected = 22;
            int[] arr = new int[50];

            for (int i = 0; i < 50; i++)
            {
                arr[i] = i + 1;
            }

            Shuffle(arr);

            int median = s.MomSelect(arr, 22);

            Assert.AreEqual(expected, median);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Select s = new Select();
            int expected = 12;
            int[] arr = new int[24];

            for (int i = 0; i < 24; i++)
            {
                arr[i] = i + 1;
            }

            Shuffle(arr);

            int median = s.MomSelect(arr, arr.Length / 2);

            Assert.AreEqual(expected, median);
        }

        [TestMethod]
        public void TestGeeksMom()
        {
            int testSize = 1000;
            int[] array = new int[testSize];
            for (int i = 0; i < testSize; i++)
            {
                array[i] = i + 1;
            }

            Shuffle(array);

            Select s = new Select();

            int expected = 30;
            int result = s.MomSelect(array, 30);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestWeightedMedianSmallArray()
        {
            int[] S = new int[5] {5,3,2,4,1};
            int[] W = new int[5] {1,1,1,1,1};

            int expected = 3;

            Select s = new Select();
            int result = s.WeightedMedian(S, W);
            Assert.AreEqual(expected, result);
        }
    }
}