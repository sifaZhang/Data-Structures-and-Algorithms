using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class BinarySearch
    {
        public static void Run()
        {
            Console.WriteLine("----------------04 start----------------");
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

            int iLoopCount = 0;
            int result = BinarySearchF(numbers, iTarget, out iLoopCount);
            if (result != -1)
            {
                Console.WriteLine($"Element found at index: {result} and loop {iLoopCount} times");
            }
            else
            {
                Console.WriteLine("Element not found in the array.");
            }
            Console.WriteLine("----------------04 end----------------");
        }

        public static int[] CreateSortedArray(int size, int maxValue)
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
        private static int BinarySearchF(int[] arr, int iTarget, out int iLoopCount)
        {
            iLoopCount = 0;
            for (int iLow = 0, iHigh = arr.Length - 1; iLow <= iHigh;)
            {
                iLoopCount++;
                int iMid = iLow + (iHigh - iLow) / 2;
                if (arr[iMid] == iTarget)
                {
                    return iMid;
                }
                else if (arr[iMid] > iTarget)
                {
                    iHigh = iMid - 1;
                }
                else
                {
                    iLow = iMid + 1;
                }
            }

            return -1;
        }
    }
}
