using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HelloWorld
{
    internal class Factorial
    {
        public static void Run()
        {
            Console.WriteLine("Please input a number to caculate:");
            string strData = Console.ReadLine() ?? string.Empty;
            int number = 0;
            bool success = Int32.TryParse(strData, out number);
            if (success && number > 1)
            {
                int result = TryFactorial(number);
                Console.WriteLine("The factorail of {0} is {1}", number, result);

                string workProcess = printFactorial(number);
                workProcess += "=" + result;
                Console.WriteLine("The sequence of numbers is {0}", workProcess);
            }
            else
            {
                Console.WriteLine("Please input a number which is more than 1.");
            }
        }

        public static int TryFactorial(int number)
        {
            if (number > 1)
            {
                return number * TryFactorial(number - 1);
            }

            return 1;
        }

        public static string printFactorial(int number)
        {
            if (number <= 1)
            {
                return "1";
            }
            else
            {
                return number.ToString() + "*" + printFactorial(number - 1);
            }
        }
    }
}
