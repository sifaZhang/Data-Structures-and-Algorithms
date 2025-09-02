//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/09/03

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;


namespace Assignment_3
{
    internal class Program
    {
        private static MyQueue myQueue = new MyQueue();   // save the date that user input
        private static bool testMode = false;   //user mode or test mode
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
                if (testMode && waiting )
                {
                    //let console to output firstly
                    Thread.Sleep(50);

                    //get current queue
                    if (keyValuePairs.ContainsKey(current))
                    {
                        MyQueue testQueue = keyValuePairs[current];
                        int value = 0;
                        //pop current data
                        if (testQueue.Dequeue(out value))
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
                Console.WriteLine("[1]: Enqueue");
                Console.WriteLine("[2]: Dequeue");
                Console.WriteLine("[3]: Peek");
                Console.WriteLine("[4]: Clear");
                Console.WriteLine("[5]: ShowQueue");
                Console.WriteLine("[6]: Testing");
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
                    if (iChoice > 6 || iChoice < 1)
                    {
                        Console.WriteLine("Invalid input! Please input the number from 1 do 6!");
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
                        Console.WriteLine("The current queue is {0}", myQueue.ToString());
                        Console.WriteLine("\r\n");
                        break;
                    case 6:
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
                    myQueue.Enqueue(iChoice);
                    Console.WriteLine("The current queue is {0}", myQueue.ToString());
                }
            }
        }

        /// <summary>
        /// perform the pop process
        /// </summary>
        public static void TryPop()
        {
            int top = 0;
            if (myQueue.Dequeue(out top))
            {
                Console.WriteLine("The oldest item ({0}) has popped.", top);
                Console.WriteLine("The current queue is {0}.", myQueue.ToString());
            }
            else
            {
                Console.WriteLine("The queue is empty.");
            }

            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// perform the peek process
        /// </summary>
        public static void TryPeek()
        {
            int top = 0;
            if (myQueue.Peek(out top))
            {
                Console.WriteLine("The oldest item is ({0}).", top);
            }
            else
            {
                Console.WriteLine("The queue is empty.");
            }

            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// performe the clear process
        /// </summary>
        public static void TryClear()
        {
            myQueue.Clear();
            Console.WriteLine("The queue is cleared.");
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
            int number = random.Next(10, 15);
            MyQueue queuePush = new MyQueue();
            queuePush.Enqueue(1);  
            for (int i = 0; i < number; i++)
            {
                queuePush.Enqueue(random.Next(0, 100));    //data
            }
            
            queuePush.Enqueue(-1); // quit

            MyQueue queuePop = new MyQueue();
            for (int i = 0; i < number / 2; i++)
            {
                queuePop.Enqueue(2);   
            }

            MyQueue queuePeek = new MyQueue();
            queuePeek.Enqueue(3);  
            queuePeek.Enqueue(3);  

            MyQueue queueClear = new MyQueue();
            queueClear.Enqueue(4); 

            //manage in a dictionary
            keyValuePairs.Clear();
            keyValuePairs.Add(1, queuePush);
            keyValuePairs.Add(2, queuePop);
            keyValuePairs.Add(3, queuePeek);
            keyValuePairs.Add(4, queueClear);

            myQueue.Clear();
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

            choice = int.Parse(choiceTemp);
            return false;
        }
    }
}
