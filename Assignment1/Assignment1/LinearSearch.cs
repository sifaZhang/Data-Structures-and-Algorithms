//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2023/10/16

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class LinearSearch
    {

        public static void Run()
        {
            PublicFunction.SearchMethod(LinearSearchMethod);
        }

        /// <summary>
        /// searches for a number in an array using linear search.
        public static int LinearSearchMethod(int[] numbers, int searchNumber, out int iterCount)
        {
            iterCount = 0; // Initialize iteration count.

            // Check if the array is null or empty.
            if (numbers == null || numbers.Length == 0)
            {
                Console.WriteLine("The array is empty.");
                return -1;
            }
            
            // Perform linear search on the array.
            for (int i = 0; i < numbers.Length; i++)
            {
                iterCount++;
                if (numbers[i] == searchNumber)
                {
                    return i;
                }
            }

            return -1; // If the number is not found, return -1.
        }
    }
}
