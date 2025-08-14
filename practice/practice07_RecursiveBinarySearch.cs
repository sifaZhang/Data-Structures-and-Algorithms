using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class RecursiveBinarySearch
    {
        public static void Run()
        {
            Console.WriteLine("----------------07 start----------------");

            Console.WriteLine("Please input the size of the array:");
            string strSize = Console.ReadLine() ?? string.Empty;
            int size = int.Parse(strSize);

            Console.WriteLine("Please input the maximum value for the random numbers:");
            string strMaxValue = Console.ReadLine() ?? string.Empty;
            int maxValue = int.Parse(strMaxValue);

            int[] numbers = CreateSortedArray(size, maxValue);
            Console.WriteLine("Generated array:");
            for (int number = 0; number < numbers.Length; number++)
            {
                Console.WriteLine("array[{0}]={1}", number, numbers[number]);
            }

            Console.WriteLine("Which number do you want to find:");
            string strTarget = Console.ReadLine() ?? string.Empty;
            int iTarget = int.Parse(strTarget);

            m_iLoopCount = 0;
            int result = RecursiveBinarySearchF(numbers, iTarget, 0, numbers.Length - 1);
            if (result != -1)
            {
                Console.WriteLine($"Element found at index: {result} and loop {m_iLoopCount} times");
            }
            else
            {
                Console.WriteLine("Element not found in the array.");
            }
            Console.WriteLine("----------------07 end----------------");
        }
        private static int[] CreateSortedArray(int size, int maxValue)
        {
            Random rand = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(maxValue);
            }
            Array.Sort(array);
            return array;
        }
        private static int RecursiveBinarySearchF(int[] arr, int target, int left, int right)
        {
            if (left > right)
                return -1; // Target not found
            m_iLoopCount++;

            int mid = left + (right - left) / 2;
            if (arr[mid] == target)
                return mid; // Target found
            else if (arr[mid] > target)
                return RecursiveBinarySearchF(arr, target, left, mid - 1); // Search in left half
            else
                return RecursiveBinarySearchF(arr, target, mid + 1, right); // Search in right half
        }

        private static int m_iLoopCount = 0; // Loop count for tracking
    }
}
