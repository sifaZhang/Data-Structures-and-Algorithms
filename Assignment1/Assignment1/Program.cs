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
    internal class Program
    {
        static void Main(string[] args)
        {
            // This is the main entry point for the program.

            while(true)
            {
                Console.Clear();
                // It prompts the user to select a number corresponding to a specific algorithm or functionality.
                Console.WriteLine("Please select the number:");
                Console.WriteLine("1: Linear Search");
                Console.WriteLine("2: Binary Search");
                Console.WriteLine("3: Bubble Sort");
                Console.WriteLine("4: Selection Sort");
                Console.WriteLine("5: Performance Testing");

                // Read user input and convert it to an integer.
                // Ensure that the input is valid (between 1 and 5).
                int iChoice = PublicFunction.ReadANumber();
                while (iChoice > 5 || iChoice < 1)
                {
                    Console.WriteLine("Invalid input! Please input the number from 1 do 5!");
                    iChoice = PublicFunction.ReadANumber();
                }

                // Based on the user's choice, call the corresponding method to run the selected functionality.
                switch (iChoice)
                {
                    case 1:
                        LinearSearch.Run();
                        break;
                    case 2:
                        BinarySearch.Run();
                        break;
                    case 3:
                        BubbleSort.Run();
                        break;
                    case 4:
                        SelectionSort.Run();
                        break;
                    case 5:
                        PerformanceTesting.Run();
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }

                Console.WriteLine("Press any key to continue or 'q' to quit.");
                // Wait for the user to press a key.
                string input = Console.ReadLine();
                // If the user presses 'q', exit the loop and terminate the program.
                if (input?.ToLower() == "q")
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    return; // Exit the program.
                }
            }
        }
    }
}
