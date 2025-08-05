using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class RecursiveLinerSearch
    {
        public static void Run()
        {
            Console.WriteLine("----------------06 start----------------");
            Console.WriteLine("Please input the size of the array:");
            string strSize = Console.ReadLine() ?? string.Empty;
            int size = int.Parse(strSize);

            Console.WriteLine("Please input the maximum value for the random numbers:");
            string strMaxValue = Console.ReadLine() ?? string.Empty;
            int maxValue = int.Parse(strMaxValue);

            int[] numbers = CreateRandomArray(size, maxValue);
            Console.WriteLine("Generated array:");
            for (int number = 0; number < numbers.Length; number++)
            {
                Console.WriteLine("array[{0}]={1}", number, numbers[number]);
            }

            Console.WriteLine("Which number do you want to find:");
            string strTarget = Console.ReadLine() ?? string.Empty;
            int iTarget = int.Parse(strTarget);

            int iIndex = 0;
            int result = RecursiveSearch(numbers, iTarget, iIndex);
            if (result != -1)
            {
                Console.WriteLine($"Element found at index: {result} and loop {result + 1} times");
            }
            else
            {
                Console.WriteLine("Element not found in the array.");
            }

            Console.WriteLine("----------------06 end----------------");
        }
        private static int[] CreateRandomArray(int size, int maxValue)
        {
            Random rand = new Random();
            return Enumerable.Range(0, size).Select(_ => rand.Next(maxValue)).ToArray();
        }
        private static int RecursiveSearch(int[] arr, int target, int index)
        {
            if (index >= arr.Length)
            {
                return -1; // Target not found
            }

            if (target == arr[index])
            {
                return index;
            }
            else
            {
                return RecursiveSearch(arr, target, ++index);
            }
        }
    }
}
