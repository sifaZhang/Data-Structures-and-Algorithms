using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class PerformanceTesting
    {
        public delegate int SearchFunction(int[] numbers, int searchNumber, out int iterCount);
        public delegate void SortFunction(int[] numbers, bool bAsc, out int iterCount);

        public static void Run()
        {
            Console.WriteLine("Please inut the test count for search and sort methods.");
            int testCount = PublicFunction.ReadANumber();
            Console.WriteLine("Create {0} random array and run {1} times, the average iterator and times are:", testCount, testCount);

            TestSearchMethod(LinearSearch.LinearSearchMethod, testCount, "Linear Search", "O(n)");
            TestSearchMethod(BinarySearch.BinarySearchMethod, testCount, "Binary Search", "O(log(n))");

            TestSortMethod(BubbleSort.BubbleSortMethod, testCount, "Bubble Sort", "O(n)");
            TestSortMethod(SelectionSort.SelectionSortMethod, testCount, "Selection Sort", "O(n)");
        }

        /// <summary>
        /// tests the search method by creating random arrays and measuring performance.
        /// </summary>
        public static void TestSearchMethod(SearchFunction searchFunction, int testCount, string name, string bigO)
        {
            int[] iterCounts = new int[testCount];
            int[] tickCounts = new int[testCount];
            for (int i = 0; i < testCount; i++)
            {
                int[] numbers = PublicFunction.CreateRandomArray(5);
                Random random = new Random();
                int searchNumber = 500;

                int startTick = Environment.TickCount;
                int result = searchFunction(numbers, searchNumber, out iterCounts[i]);
                int endTick = Environment.TickCount;
                tickCounts[i] = endTick - startTick;
            }

            Console.WriteLine("{0}: average iteration:{1}, average tick:{2},estamited BigO: {3}",
                name, iterCounts.Average(), tickCounts.Average(), bigO);
        }

        /// <summary>
        /// tests the sort method by creating random arrays and measuring performance.
        /// </summary>  
        public static void TestSortMethod(SortFunction sortFunction, int testCount, string name, string bigO)
        {
            int[] iterCounts = new int[testCount];
            int[] tickCounts = new int[testCount];
            for (int i = 0; i < testCount; i++)
            {
                int[] numbers = PublicFunction.CreateRandomArray(5);
                Random random = new Random();

                int startTick = Environment.TickCount;
                sortFunction(numbers, true, out iterCounts[i]);
                int endTick = Environment.TickCount;
                tickCounts[i] = endTick - startTick;
            }

            Console.WriteLine("{0}: average iteration:{1}, average tick:{2},estamited BigO: {3}",
                name, iterCounts.Average(), tickCounts.Average(), bigO);
        }
    }
}
