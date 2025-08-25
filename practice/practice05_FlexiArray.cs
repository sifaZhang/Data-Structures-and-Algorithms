using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class FlexArray
    {
        public void Add(int iValue)
        {
            if (iLastPos < array.Length - 1)
            {
                iLastPos++;
                array[iLastPos] = iValue;
            }
            else
            {
                Console.WriteLine("Array is full, resizing from {0} to {1}", array.Length, array.Length * 2);
                int[] newArray = new int[array.Length * 2];
                array.CopyTo(newArray, 0);
                array = newArray;

                iLastPos++;
                array[iLastPos] = iValue;
            }
        }

        //LinerSearch
        public int LinerSearch(int target)
        {
            for (int i = 0; i <= iLastPos; i++)
            {
                if (array[i] == target)
                {
                    return i;
                }
            }
            return -1;
        }

        public int BinarySearch(int iTarget)
        {
            for (int iLow = 0, iHigh = iLastPos; iLow <= iHigh;)
            {
                int iMid = iLow + (iHigh - iLow) / 2;
                if (array[iMid] == iTarget)
                {
                    return iMid;
                }
                else if (array[iMid] > iTarget)
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

        public void Remove(int iValue)
        {
            int iIndex = BinarySearch(iValue);
            if (iIndex > 0)
            {
                for(int i = iIndex; i <= iLastPos - 1; i++)
                {
                    array[i] = array[i + 1];
                }

                iLastPos -= 1;
            }
        }

        // Selection Sort
        public void Sort()
        {
            for (int i = 0; i <= iLastPos; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j <= iLastPos; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    // Swap the found minimum element with the first element
                    int temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
        }

        public void ShowContent()
        {
            if (iLastPos == -1)
            {
                Console.WriteLine("The array is empty.");
            }
            else
            {
                Console.WriteLine("Array content:");
                for (int i = 0; i <= iLastPos; i++)
                {
                    Console.WriteLine($"array[{i}] = {array[i]}");
                }
            }
        }

        private int[] array = new int[10];
        private int iLastPos = -1;
    }

    internal class FlexiArray
    {
        public static void Run()
        {
            Console.WriteLine("----------------05 start----------------");
            
            while (true)
            {
                Console.WriteLine("Please input the size of the array:");
                string strSize = Console.ReadLine() ?? string.Empty;
                if (strSize.ToUpper() == "EXIT")
                {
                    break;
                }
                int iSize = int.TryParse(strSize, out int iNumber) ? iNumber : 0;

                Console.WriteLine("Please input the maximum value for the random numbers:");
                string strMaxValue = Console.ReadLine() ?? string.Empty;
                if (strSize.ToUpper() == "EXIT")
                {
                    break;
                }
                int iMaxValue = int.TryParse(strMaxValue, out int iMaxValueTemp) ? iMaxValueTemp : 0;

                int[] array = CreateSortedArray(iSize, iMaxValue);
                FlexArray flexArray = new FlexArray();
                foreach (int value in array)
                {
                    flexArray.Add(value);
                }

                flexArray.ShowContent();
            }

            Console.WriteLine("----------------05 end----------------");
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
    }
}
