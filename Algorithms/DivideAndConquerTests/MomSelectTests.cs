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
            Select s = new Select();
            int expected = 25;
            int[] arr = new int[50];

            for(int i = 0; i < 50; i++)
            {
                arr[i] = i+1;
            }

            Shuffle(arr);

            int median = s.MomSelect(arr, arr.Length/2);

            Assert.AreEqual(expected, median);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Select s = new Select();
            int expected = 12;
            int[] arr = new int[24];

            for (int i = 0; i < 24; i++)
            {
                arr[i] = i+1;
            }

            Shuffle(arr);

            int median = s.MomSelect(arr, arr.Length/2);

            Assert.AreEqual(expected, median);
        }
    }
}
