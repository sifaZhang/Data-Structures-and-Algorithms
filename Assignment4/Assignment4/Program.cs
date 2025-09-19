//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/09/19

using Assignment4;
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


namespace Assignment_4
{
    internal class Program
    {
        private static BinarySearchTree binarySearchTree = new BinarySearchTree();   // save the date that user input
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
                if (testMode && waiting)
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

                        binarySearchTree.Clear();
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
                Console.WriteLine("[2]: Find");
                Console.WriteLine("[3]: Sort");
                Console.WriteLine("[4]: The smallest and largest value");
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
                        TryFind();
                        break;
                    case 3:
                        TrySort();
                        break;
                    case 4:
                        TryPeek();
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
        /// perform the push process
        /// </summary>
        public static void TryAdd()
        {
            while (true)
            {
                Console.WriteLine("Please input a number for adding or [q] to quit to the upper menu:");

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
                    Node? findNode = binarySearchTree.Search(iChoice);
                    if (findNode != null)
                    {
                        Console.WriteLine("The node ({0}) is already in the tree.", iChoice);
                        continue;
                    }
                    else
                    {
                        binarySearchTree.Insert(iChoice);
                        string sorted = binarySearchTree.PreOrder();
                        if (sorted == "")
                        {
                            Console.WriteLine("The tree is empty.");
                        }
                        else
                        {
                            Console.WriteLine("PreOrder: " + sorted);
                        }

                        binarySearchTree.DrawTree();
                    }
                }
            }
        }

        /// <summary>
        /// perform the pop process
        /// </summary>
        public static void TryFind()
        {
            while (true)
            {
                Console.WriteLine("Please input a number for finding or [q] to quit to the upper menu:");

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
                    binarySearchTree.DrawTree();
                    Node? findNode = binarySearchTree.Search(iChoice);
                    if (findNode == null)
                    {
                        Console.WriteLine("The node ({0}) is not found.", iChoice);
                    }
                    else
                    {
                        Console.WriteLine("The node ({0}) is found. Its left: {1}, Its right: {2}.", 
                            iChoice, findNode.Left == null ? "null" : findNode.Left.Value.ToString(),
                            findNode.Right == null ? "null" : findNode.Right.Value.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// perform the peek process
        /// </summary>
        public static void TrySort()
        {
            string sorted = binarySearchTree.InOrder();
            if(sorted == "")
            {
                Console.WriteLine("The tree is empty.");
            }
            else
            {
                binarySearchTree.DrawTree();
                Console.WriteLine("InOrder: " + sorted);
            }

            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// performe the clear process
        /// </summary>
        public static void TryPeek()
        {
            if (binarySearchTree.Height() == -1 )
            {
                Console.WriteLine("The tree is empty.");
            }
            else
            {
                binarySearchTree.DrawTree();
                int smallest = binarySearchTree.Smallest();
                int largest = binarySearchTree.Largest();

                Console.WriteLine("The smallest value is {0}, the largest value is {1}", smallest, largest);
            }

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
            queueAdd.Enqueue(1);
            for (int i = 0; i < number; i++)
            {
                int randomNumber = random.Next(0, 100);
                queueAdd.Enqueue(randomNumber);    //data
                if (i == 0)
                {    
                    queueAdd.Enqueue(randomNumber);    //duplicate data
                }
            }
            queueAdd.Enqueue(-1); // quit

            MyQueue queueFind = new MyQueue();
            queueFind.Enqueue(2);
            for (int i = 0; i < number; i++)
            {
                if(i % 2 == 0)
                {
                    queueFind.Enqueue(random.Next(0, 100)); //random data
                }
                else
                {
                    queueFind.Enqueue(queueAdd.GetValueAt(i));  //found
                }
            }
            queueFind.Enqueue(-1); // quit

            MyQueue queueSort = new MyQueue();
            queueSort.Enqueue(3);

            MyQueue queuePeek = new MyQueue();
            queuePeek.Enqueue(4);

            //manage in a dictionary
            keyValuePairs.Clear();
            keyValuePairs.Add(1, queueAdd);
            keyValuePairs.Add(2, queueFind);
            keyValuePairs.Add(3, queueSort);
            keyValuePairs.Add(4, queuePeek);

            binarySearchTree.Clear();
            Console.WriteLine("The tree is cleared.");
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
