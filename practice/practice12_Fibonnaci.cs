using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    internal class Fibonnaci
    {
        public static void Run()
        {
            Console.WriteLine("Please input a number:");
            string strData = Console.ReadLine() ?? string.Empty;
            while (strData != "q")
            {
                int number = 0;
                bool success = Int32.TryParse(strData, out number);
                if (success && number >= 0)
                {
                    Console.WriteLine($"Fibonacci({number}) = {Fibonacci(number)}");
                }
                else
                {
                    Console.WriteLine("Please input a number which is more than 0.");
                }

                Console.WriteLine("Please input a number:");
                strData = Console.ReadLine() ?? string.Empty;
            }
        }

        static int Fibonacci(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            else
            {
                return Fibonacci(n - 1) + Fibonacci(n - 2);
            }
        }
    }
}
