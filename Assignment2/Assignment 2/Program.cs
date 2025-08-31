//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/08/27

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;


namespace Assignment_2
{
    internal class Program
    {
        private static Stack myStack;   // save the date that user input
        private static ManualResetEvent inputReady = new ManualResetEvent(false);   //two threads sync event
        private static bool testMode = false;   //user mode or test mode
        private static Dictionary<int, Stack> keyValuePairs;    //use for test mode

        /// <summary>
        /// simulate input thread
        /// </summary>
        static void SimulateInput()
        {
            int current = 1;
            var sim = new InputSimulator();
            // wait for event
            while (inputReady.WaitOne(0)) 
            {
                // work in test mode
                if (testMode)
                {
                    //get current stack
                    if (keyValuePairs.ContainsKey(current))
                    {
                        Stack stack = keyValuePairs[current];
                        int value = 0;
                        //pop current data
                        if (stack.Pop(out value))
                        {
                            // -1 represent quit
                            if (value == -1)
                            {
                                sim.Keyboard.TextEntry("q");
                            }
                            else
                            {
                                sim.Keyboard.TextEntry(value.ToString());
                            }
                            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                            Thread.Sleep(100);

                            //if the stack is empty, move to next stack.
                            if (stack.IsEmpty())
                            {
                                Console.WriteLine("-----------Testing {0} end-----------", current++);
                                if (keyValuePairs.ContainsKey(current))
                                {
                                    Console.WriteLine("-----------Testing {0} start-----------", current);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: current = {0}", current++);
                        }
                    }
                    else
                    {
                        // quit test Mode
                        testMode = false;
                        current = 1;

                        Console.WriteLine("============Performance Testing end============");
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }


        /// <summary>
        /// the entry of the app
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // initialization
            myStack = new Stack();

            //start stimulate thread
            Thread simulatorThread = new Thread(SimulateInput);
            simulatorThread.Start();

            while (true)
            {
                // It prompts the user to select a number corresponding to a specific algorithm or functionality.
                Console.WriteLine("Please select the number:");
                Console.WriteLine("[1]: Push");
                Console.WriteLine("[2]: Pop");
                Console.WriteLine("[3]: Peek");
                Console.WriteLine("[4]: Clear");
                Console.WriteLine("[5]: Testing");
                Console.WriteLine("[q]: Quit");

                // Read user input and convert it to an integer.
                // Ensure that the input is valid (between 1 and 5).
                int iChoice = 0;
                if(ChoiceOrQuit(out iChoice))
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    return; // Exit the program.
                }
                else
                {
                    if (iChoice > 5 || iChoice < 1)
                    {
                        Console.WriteLine("Invalid input! Please input the number from 1 do 5!");
                        continue;
                    }
                }

                // Based on the user's choice, call the corresponding method to run the selected functionality.
                switch (iChoice)
                {
                    case 1:
                        TryPush();
                        break;
                    case 2:
                        TryPop();
                        break;
                    case 3:
                        TryPeek();
                        break;
                    case 4:
                        TryClear();
                        break;
                    case 5:
                        Console.Clear();
                        PerformanceTesting();
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

        /// <summary>
        /// perform the push process
        /// </summary>
        public static void TryPush()
        {
            while (true)
            {
                Console.WriteLine("Please input a number or [q] to quit to the upper menu:");

                //get a new data or quit
                int iChoice = 0;
                if (ChoiceOrQuit(out iChoice))
                {
                    Console.WriteLine("\r\n");
                    return;
                }
                else if (int.MaxValue == iChoice)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }
                else
                {
                    myStack.Push(iChoice);
                    Console.WriteLine("The current stack is {0}", myStack.ToString());
                }
            }
        }

        /// <summary>
        /// perform the pop process
        /// </summary>
        public static void TryPop()
        {
            int top = 0;
            if(myStack.Pop(out top))
            {
                Console.WriteLine("The top item ({0}) has popped.", top);
                Console.WriteLine("The current stack is {0}.", myStack.ToString());
            }
            else
            {
                Console.WriteLine("The stack is empty.");
            }

            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// perform the peek process
        /// </summary>
        public static void TryPeek()
        {
            int top = 0;
            if (myStack.Peek(out top))
            {
                Console.WriteLine("The top item is ({0}).", top);
            }
            else
            {
                Console.WriteLine("The stack is empty.");
            }

            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// performe the clear process
        /// </summary>
        public static void TryClear()
        {
            myStack.Clear();
            Console.WriteLine("The Stack is cleared.");
            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// test from 1 to 4 process
        /// </summary>
        public static void PerformanceTesting()
        {
            testMode = true;
            Console.WriteLine("============Performance Testing start============");
            Console.WriteLine("-----------Testing 1 start-----------");

            //construct the stimulate data
            Random random = new Random();
            int number = random.Next(5, 10);
            Stack stackPush = new Stack();
            stackPush.Push(-1); // quit
            for (int i = 0; i < number; i++)
            {
                stackPush.Push(random.Next(0, 100));    //data
            }
            stackPush.Push(1);  //selection

            Stack stackPop = new Stack();
            for (int i = 0; i < number / 2; i++)
            {
                stackPop.Push(2);   //selection
            }

            Stack stackPeek = new Stack();
            stackPeek.Push(3);  //selection

            Stack stackClear = new Stack();
            stackClear.Push(4); //selection

            //manage in a dictionary
            keyValuePairs = new Dictionary<int, Stack>();
            keyValuePairs.Add(1, stackPush);
            keyValuePairs.Add(2, stackPop);
            keyValuePairs.Add(3, stackPeek);
            keyValuePairs.Add(4, stackClear);
        }

        /// <summary>
        /// get the user input
        /// </summary>
        /// <param name="choice"></param>
        /// <returns></returns>
        public static bool ChoiceOrQuit(out int choice)
        {
            choice = int.MaxValue;

            // Read user input from the console.
            inputReady.Set(); // wait for inputing
            string choiceTemp = Console.ReadLine() ?? string.Empty;
            inputReady.Reset(); // end of inputing
            // quit
            if (choiceTemp?.ToLower() == "q")
            {
                return true;
            }

            //if the input is not a number
            if (choiceTemp == string.Empty
                || !int.TryParse(choiceTemp, out int number))
            {
                return false;
            }

            choice =  int.Parse(choiceTemp);
            return false;
        }
    }
}
