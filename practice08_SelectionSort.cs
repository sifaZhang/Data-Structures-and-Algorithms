using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class SelectionSort
    {
        public static void Run()
        {
            Console.WriteLine("----------------08 start----------------");
            Console.WriteLine("Please input the array(seperated by space):");
            string strData = Console.ReadLine() ?? string.Empty;
            string[] strArray = strData.Split(' ');
            int[] numbers = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                numbers[i] = int.Parse(strArray[i]);
            }

            // Selection Sort
            for (int i = 0; i < numbers.Length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < numbers[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    // Swap the found minimum element with the first element
                    int temp = numbers[i];
                    numbers[i] = numbers[minIndex];
                    numbers[minIndex] = temp;
                }
            }

            Console.WriteLine("Sorted array:");
            string sortedArray = "";
            for (int i = 0; i < numbers.Length; i++)
            {
                sortedArray += numbers[i] + " ";
            }
            Console.WriteLine(sortedArray);

            Console.WriteLine("----------------08 end----------------");
        }
    }
}
