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
    internal class PublicFunction
    {
        public delegate int SearchFunction(int[] numbers, int searchNumber, out int iterCount);
        public delegate void SortFunction(int[] numbers, bool bAsc, out int iterCount);

        /// <summary>
        /// checks if the input is a valid number and returns it.
        /// </summary>
        public static int InputNumber(int index)
        {
            Console.WriteLine("Please input the {0}/5 number:", index);
            return ReadANumber();
        }

        /// <summary>
        /// reads a number from the console input and validates it.
        /// If the input is invalid, it prompts the user to input a valid number.
        /// </summary>
        public static int ReadANumber()
        {
            // Read user input from the console.
            string choice = Console.ReadLine() ?? string.Empty;
            while (choice == string.Empty
                || !int.TryParse(choice, out int number))
            {
                Console.WriteLine("Invalid input! Please input a valid number.");
                choice = string.Empty; // Reset input for next iteration
                choice = Console.ReadLine() ?? string.Empty;
            }

            return int.Parse(choice);
        }

        /// <summary>
        /// prints the array of numbers to the console.
        /// </summary>
        public static void PrintArray(int[] numbers)
        {
            string strArray = string.Join(" ", numbers);
            Console.WriteLine(strArray);
        }

        /// <summary>
        /// creates an array of integers from user input.
        /// </summary>  
        public static int[] CreateArrayFromInput(UInt32 legnth)
        {
            Console.WriteLine("Please input five numbers for searching.");

            // Initialize an array to hold five integers.
            int[] numbers = new int[legnth];
            for (int i = 0; i < legnth; i++)
            {
                numbers[i] = InputNumber(i + 1);
            }

            // Print the numbers input by the user.
            Console.WriteLine("The numbers you input are:");
            PrintArray(numbers);

            return numbers;
        }

        /// <summary>
        /// searches for a number in an array using the provided search function.
        /// </summary>  
        public static void SearchMethod(SearchFunction mySearchFunction, bool bSort = false, UInt32 legnth = 5)
        {
            Console.Clear();
            // Prompt the user to input the length of the array.
            int[] numbers = CreateArrayFromInput(legnth);

            // If bSort is true, sort the array before searching.
            if (bSort)
            {
                Array.Sort(numbers);
                Console.WriteLine("The numbers are sorted:");
                PrintArray(numbers);
            }

            // Prompt the user to input the number they want to search for.
            Console.WriteLine("Please input the number you want to search for:");
            int searchNumber = ReadANumber();

            // Perform the search using the provided search function.
            int index = mySearchFunction(numbers, searchNumber, out int iterCount);
            if (index == -1)
            {
                Console.WriteLine("-1 ( The number {0} is not found in the array.)", searchNumber);
            }
            else
            {
                Console.WriteLine("The number {0} is found at index {1}.", searchNumber, index);
            }
        }

        /// <summary>
        /// sorts an array using the provided sort function.
        /// </summary>  
        public static void SortMethod(SortFunction mySortFunction, UInt32 legnth = 5, bool bAsc = true)
        {
            Console.Clear();
            // Prompt the user to input the length of the array.
            int[] numbers = CreateArrayFromInput(legnth);

            // Sort the array using the provided sort function.
            mySortFunction(numbers, bAsc, out int iterCount);

            // Print the sorted array to the console.
            Console.WriteLine("The numbers are sorted:");
            PrintArray(numbers);
        }

        /// <summary>
        /// swaps two elements in an array at the specified indices.
        /// </summary>
        public static void Swap(int[] array, int left, int right)
        {
            // Validate the input indices to ensure they are within the bounds of the array.
            if (array == null || left < 0 || right < 0 || left >= array.Length || right >= array.Length)
            {
                Console.WriteLine("Invalid indices for swapping.");
                return;
            }

            // Swap the elements at the specified indices in the array.
            int temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }

        public static int[] CreateRandomArray(int length)
        {
            if (length <= 0)
            {
                Console.WriteLine("Invalid length for the array. Length must be greater than 0.");
                return new int[0]; // Return an empty array if the length is invalid.
            }

            Random random = new Random();
            int[] randomArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                // Generate a random number between 1 and 1000.
                randomArray[i] = random.Next(1, 1001);
            }

            return randomArray;
        }
    }
}
