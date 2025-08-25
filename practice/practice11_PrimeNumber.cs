using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HelloWorld
{
    internal class PrimeNumber
    {
        public static void Run()
        {
            Console.WriteLine("Please input a number to caculate:");
            string strData = Console.ReadLine() ?? string.Empty;
            int number = 0;
            bool success = Int32.TryParse(strData, out number);
            if (success && number > 1)
            {
                bool result = IsPrimeNumber(number, number - 1);
                Console.WriteLine("{0} is {1}", number, result ? "prime number" : "not prime number");
            }
            else
            {
                Console.WriteLine("Please input a number which is more than 1.");
            }
        }

        public static bool IsPrimeNumber(int number, int start)
        {
            if(number > 1 && start > 1)
            {
                if (number % start == 0)
                {
                    return false;
                }
                else
                {
                    return IsPrimeNumber(number, start - 1);
                }
            }

            return true;
        }
    }
}
