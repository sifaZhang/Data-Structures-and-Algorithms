using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class SelectionSort
    {
        public static void Run()
        {
            PublicFunction.SortMethod(SelectionSortMethod);
        }

        /// <summary>
        /// selection sort the array of numbers.
        /// </summary>
        public static void SelectionSortMethod(int[] numbers, bool bAsc, out int iterCount)
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

            // Perform selection sort on the array.
            for (int i = numbers.Length - 1; i > 0; i--)
            {
                int temp = numbers[i];
                int index = i;
                for (int j = 0; j < i; j++)
                {
                    iterCount++; // Increment iteration count for each swap.
                    if (bAsc)
                    {
                        if (numbers[j] > temp)
                        {
                            index = j;
                            temp = numbers[j];
                        }
                    }
                    else
                    {
                        if (numbers[j] < temp)
                        {
                            index = j;
                            temp = numbers[j];
                        }
                    }
                }

                if(i != index)
                {
                    PublicFunction.Swap(numbers, i, index);
                }
            }
        }
    }
}
