using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class BubbleSort
    {
        public static void Run()
        {
            PublicFunction.SortMethod(BubbleSortMethod);
        }

        /// <summary>
        /// bubblesort the array of numbers.
        /// </summary>
        public static void BubbleSortMethod(int[] numbers, bool bAsc, out int iterCount)
        {
            iterCount = 0; // Initialize iteration count.

            // Check if the array is null or empty.
            if (numbers == null || numbers.Length == 0)
            {
                Console.WriteLine("The array is empty.");
                return;
            }

            // Check if the array has only one element.
            if (numbers.Length < 2)
            {
                Console.WriteLine("The array has only one element, no need to sort.");
                return; // If the array has only one element, return it as is.
            }

            // Perform bubble sort on the array.
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = 0; j < numbers.Length - 1 - i; j++)
                {
                    iterCount++; // Increment iteration count for each swap.

                    if (bAsc)
                    {
                        if (numbers[j] > numbers[j+1])
                        {
                            // Swap the elements if they are in the wrong order.
                            PublicFunction.Swap(numbers, j, j+1);
                        }
                    }
                    else
                    {
                        if (numbers[j] < numbers[j + 1])
                        {
                            // Swap the elements if they are in the wrong order.
                            PublicFunction.Swap(numbers, j, j + 1);
                        }
                    }
                }
            }
        }
    }
}
