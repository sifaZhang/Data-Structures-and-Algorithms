using System;

namespace HelloWorld
{
    class MyLinkedList //create a class for linked list, contains two
    {
        public int value;
        public MyLinkedList next;
        public MyLinkedList(int value)
        {
            this.value = value;
            this.next = null;
        }
    }

    class LinkedList //create a linked list itself and contains a head.
    {
        private MyLinkedList head;
        private MyLinkedList tail;
        public LinkedList()
        {
            head = null;
            tail = null;
        }
        public void AddNode(int value) //add a new node to the list.
        {
            MyLinkedList newNode = new MyLinkedList(value);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.next = newNode;
                tail = newNode;
            }
        }

        public void DeleteNode(int value)
        {
            MyLinkedList currentNode = head;
            MyLinkedList previousNode = head;
            while (currentNode != null)
            {
                if (currentNode.value == value)
                {
                    if (currentNode == head)
                    {
                        head = currentNode.next;
                        if (currentNode.next == null)
                        {
                            tail = null;
                        }
                    }
                    else
                    {
                        previousNode.next = currentNode.next;
                    }

                    return;
                }
                else
                {
                    previousNode = currentNode;
                    currentNode = currentNode.next;
                }
            }
        }

        public void PrintLinkedlist()//print the linked list
        {
            MyLinkedList current = head;
            while (current != null)
            {
                Console.Write(current.value + ",");
                current = current.next;
            }
        }
    }

    internal class TestLinkedList
    {
        public static void Run()
        {
            Console.WriteLine("Please input the array(seperated by space):");
            string strData = Console.ReadLine() ?? string.Empty;
            string[] strArray = strData.Split(' ');
            LinkedList myLinkList = new LinkedList();
            for (int i = 0; i < strArray.Length; i++)
            {
                myLinkList.AddNode(int.Parse(strArray[i]));
            }

            Console.WriteLine("The linklist is :");
            myLinkList.PrintLinkedlist();

            Console.WriteLine("\r\nPlease input the node that you want to delete:");
            strData = Console.ReadLine() ?? string.Empty;
            int data = int.Parse(strData);
            myLinkList.DeleteNode(data);

            Console.WriteLine("The new linklist is :");
            myLinkList.PrintLinkedlist();
        }
    }


}