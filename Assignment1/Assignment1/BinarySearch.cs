using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class BinarySearch
    {
        public static void Run()
        {
            PublicFunction.SearchMethod(BinarySearchMethod, true);
        }

        /// <summary>
        /// Searches for a number in a sorted array using binary search.
        /// </summary>
        public static int BinarySearchMethod(int[] numbers, int searchNumber, out int iterCount)
        {
            iterCount = 0; // Initialize iteration count.

            // Check if the array is null or empty.
            if (numbers == null || numbers.Length == 0)
            {
                Console.WriteLine("The array is empty.");
                return -1;
            }

            // Check if the array has only one element.
            int left = 0;
            int right = numbers.Length - 1;

            // Perform binary search on the sorted array.
            while (left <= right)
            {
                iterCount++; // Increment iteration count for each loop iteration.
                int mid = left + (right - left) / 2; // Calculate the middle index.
                if (numbers[mid] == searchNumber)
                {
                    return mid; // Return the index if the number is found.
                }
                else if (numbers[mid] < searchNumber)
                {
                    left = mid + 1; // Search in the right half.
                }
                else
                {
                    right = mid - 1; // Search in the left half.
                }
            }


            return -1; // If the number is not found, return -1.
        }
    }
}
