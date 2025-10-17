//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/10/17

using Assignment5;
using System;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Assignment_5
{
    internal class Program
    {
        private static MaxHeap maxHeap = new MaxHeap();     // save the date that user input
        private static bool testMode = false;               //user mode or test mode
        private static bool waiting = false;
        private static Dictionary<int, MyQueue> keyValuePairs = new Dictionary<int, MyQueue>();    //use for test mode

        /// <summary>
        /// simulate input thread
        /// </summary>
        static void SimulateInput()
        {
            int current = 1;
            var sim = new InputSimulator();
            while (true)
            {
                // work in test mode and console is waiting input
                if (testMode && waiting)
                {
                    //let console to output firstly
                    Thread.Sleep(50);

                    //get current queue
                    if (keyValuePairs.ContainsKey(current))
                    {
                        MyQueue testQueue = keyValuePairs[current];
                        string value = "";
                        //pop current data
                        if (testQueue.Dequeue(out value))
                        {
                            // -1 represent quit
                            if (value == "-1")
                            {
                                sim.Keyboard.TextEntry("q");
                            }
                            else
                            {
                                sim.Keyboard.TextEntry(value.ToString());
                            }

                            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                            Thread.Sleep(100);  //let mian thread to output data to console

                            //if the stack is empty, move to next stack.
                            if (testQueue.IsEmpty())
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

                        maxHeap.Clear();
                        Console.WriteLine("The tree is cleared.");
                        Console.WriteLine("============Performance Testing end============");
                    }
                }
                else
                {
                    // do not block 
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
            //start stimulate thread
            Thread simulatorThread = new Thread(SimulateInput);
            simulatorThread.Start();

            while (true)
            {
                // It prompts the user to select a number corresponding to a specific algorithm or functionality.
                Console.WriteLine("Please select the number:");
                Console.WriteLine("[1]: Add");
                Console.WriteLine("[2]: Remove");
                Console.WriteLine("[3]: ReheapUp");
                Console.WriteLine("[4]: ReheapDown");
                Console.WriteLine("[5]: Testing");
                Console.WriteLine("[q]: Quit");

                // Read user input and convert it to an integer.
                // Ensure that the input is valid (between 1 and 5).
                int iChoice = 0;
                if (ChoiceOrQuit(out iChoice))
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
                        TryAdd();
                        break;
                    case 2:
                        TryRemove();
                        break;
                    case 3:
                        TryReheapUp();
                        break;
                    case 4:
                        TryReheapDown();
                        break;
                    case 5:
                        PerformanceTesting();
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

        /// <summary>
        /// add new data and reheap up
        /// </summary>
        public static void TryAdd()
        {
            while (true)
            {
                Console.WriteLine("Please input numbers(1-1000) seperating by space for adding or [q] to quit to the upper menu:");

                //get a new data or quit
                if (ChoiceOrQuit(out List<int> numbers))
                {
                    Console.WriteLine("\r\n");
                    return;
                }
                else if (numbers.Count == 0)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }
                else
                {
                    bool breakFlag = false;
                    foreach (int iChoice in numbers)
                    {
                        if (iChoice < 1 || iChoice > 1000)
                        {
                            Console.WriteLine("The input ({0}) is out of range [1, 1000].", iChoice);
                            breakFlag = true;
                            continue;
                        }
                    }

                    if (breakFlag) continue;

                    maxHeap.Add(numbers);
                    maxHeap.DrawHeap();
                }
            }
        }

        /// <summary>
        /// remove the root and reheap up
        /// </summary>
        public static void TryRemove()
        {
            maxHeap.Remove();
            maxHeap.DrawHeap();

            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// reheap up and print the tree
        /// </summary>
        public static void TryReheapUp()
        {
            while (true)
            {
                Console.WriteLine("Please input a number(1-1000) for adding or [q] to quit to the upper menu:");

                //get a new data or quit
                if (ChoiceOrQuit(out List<int> numbers))
                {
                    Console.WriteLine("\r\n");
                    return;
                }
                else if (numbers.Count == 0)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }
                else
                {
                    foreach (int iChoice in numbers)
                    {
                        if (iChoice < 1 || iChoice > 1000)
                        {
                            Console.WriteLine("The input ({0}) is out of range [1, 1000].", iChoice);
                            continue;
                        }
                    }

                    numbers.RemoveRange(1, numbers.Count - 1);
                    maxHeap.Add(numbers);
                    maxHeap.DrawHeap();
                }
            }
        }

        /// <summary>
        /// reheap down and print the tree  
        /// </summary>
        public static void TryReheapDown()
        {
            maxHeap.Remove();
            maxHeap.DrawHeap();

            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// test from 1 to 4 process
        /// </summary>
        public static void PerformanceTesting()
        {
            //Console.Clear();
            Console.WriteLine("============Performance Testing start============");
            Console.WriteLine("-----------Testing 1 start-----------");

            //construct the stimulate data
            Random random = new Random();
            int number = random.Next(5, 10);
            MyQueue queueAdd = new MyQueue();
            queueAdd.Enqueue("1");
            string data = "";
            for (int i = 0; i < number; i++)
            {
                int randomNumber = random.Next(1, 1001);
                data += randomNumber.ToString();    //data
                data += " ";                        //space
            }
            queueAdd.Enqueue(data);
            queueAdd.Enqueue("-1"); // quit

            MyQueue queueRemove= new MyQueue();
            queueRemove.Enqueue("2");


            MyQueue queueHerapUp = new MyQueue();
            queueHerapUp.Enqueue("3");
            queueHerapUp.Enqueue(random.Next(1, 1001).ToString());
            queueHerapUp.Enqueue("-1"); // quit

            MyQueue TryReheapDown = new MyQueue();
            TryReheapDown.Enqueue("4");

            //manage in a dictionary
            keyValuePairs.Clear();
            keyValuePairs.Add(1, queueAdd);
            keyValuePairs.Add(2, queueRemove);
            keyValuePairs.Add(3, queueHerapUp);
            keyValuePairs.Add(4, TryReheapDown);

            maxHeap.Clear();
            Console.WriteLine("The heap is cleared.");
            testMode = true;
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
            waiting = true;
            string choiceTemp = Console.ReadLine() ?? string.Empty;
            waiting = false;

            //if the input is not a number
            if (string.IsNullOrEmpty(choiceTemp))
            {
                return false;
            }

            // quit
            if (choiceTemp?.ToLower() == "q")
            {
                return true;
            }

            //if the input is not a number
            if (!int.TryParse(choiceTemp, out int number))
            {
                return false;
            }

            choice = int.Parse(choiceTemp);
            return false;
        }

        /// <summary>
        /// get the user input
        /// </summary>
        /// <param name="choice"></param>
        /// <returns></returns>
        public static bool ChoiceOrQuit(out List<int> numbers)
        {
            numbers = new List<int>();

            // Read user input from the console.
            waiting = true;
            string choiceTemp = Console.ReadLine() ?? string.Empty;
            waiting = false;

            //if the input is not a number
            if (string.IsNullOrEmpty(choiceTemp))
            {
                return false;
            }

            // quit
            if (choiceTemp?.ToLower() == "q")
            {
                return true;
            }

            string[] parts = choiceTemp.Split(' ');
            foreach (string part in parts)
            {
                if (string.IsNullOrWhiteSpace(part)) continue; 
                if (int.TryParse(part, out int number))
                {
                    numbers.Add(number);
                }
                else
                {
                    numbers.Clear();
                    return false;
                }
            }

            return false;
        }
    }
}
