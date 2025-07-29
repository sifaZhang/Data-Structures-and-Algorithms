using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HelloWorld
{
    internal class practice02_FindPeakInArray
    {
        public static void Run()
        {
            uint count = 20;
            int[] numbers = new int[count];

            for (int i = 0; i < count; i++)
            {
                numbers[i] = new Random().Next(1000);
            }

            if (count > 0)
            {
                int iMin = numbers[0];
                int iMax = numbers[0];
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"Number[{i}]: {numbers[i]}");
                    if (numbers[i] > iMax)
                    {
                        iMax = numbers[i];
                    }
                    else if (numbers[i] < iMin)
                    {
                        iMin = numbers[i];
                    }
                    else
                    {

                    }
                }

                Console.WriteLine($"The max number: {iMax}");
                Console.WriteLine($"The min number:{iMin}");
            }
        }
    }
}
