using System;
using System.Security.Cryptography.X509Certificates;

namespace Assignment_3
{
    /// <summary>
    /// create a class for linked list, contains two elements
    /// </summary>
    class MyLinkedList
    {
        public int value;
        public MyLinkedList? next;
        public MyLinkedList(int value)
        {
            this.value = value;
            this.next = null;
        }
    }

    /// <summary>
    /// create a linked list contains a head and a tail.
    /// </summary>
    class LinkedList
    {
        private MyLinkedList? head;
        private MyLinkedList? tail;

        public LinkedList()
        {
            head = null;
            tail = null;
        }

        public bool FindNode(int value)
        {
            MyLinkedList? node = head;
            while (node != null)
            {
                if (node.value == value)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// get the total number
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            int count = 0;
            MyLinkedList? current = head;
            while (current != null)
            {
                count++;
                current = current.next;
            }

            return count;
        }

        /// <summary>
        /// delete all data
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
        }

        /// <summary>
        /// get the first node
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetHeadNode(out int value)
        {
            value = 0;
            if ( head == null)
            {
                return false;
            }
            else
            {
                value = head.value;
                return true;
            }
        }

        /// <summary>
        /// add a new node to the list.
        /// </summary>
        /// <param name="value"></param>
        public void AddNode(int value) 
        {
            MyLinkedList newNode = new MyLinkedList(value);
            if (head == null || tail == null)
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

        /// <summary>
        /// delete the first node
        /// </summary>
        /// <returns></returns>
        public bool RemoveFirstNode(out int vaule)
        {
            if (head != null)
            {
                vaule = head.value;
                head = head.next;
                if (head == null)
                {
                    tail = null;
                }

                return true;
            }
            else
            {
                vaule = 0;
                return false;
            }
        }

        /// <summary>
        /// delete a node
        /// </summary>
        /// <param name="value"></param>
        public bool DeleteNode(int value)
        {
            MyLinkedList? currentNode = head;
            MyLinkedList? previousNode = head;
            while (currentNode != null && previousNode != null)
            {
                if (currentNode.value == value)
                {
                    if (currentNode == head)
                    {
                        head = currentNode.next;
                        if (head == null)
                        {
                            tail = null;
                        }
                    }
                    else
                    {
                        previousNode.next = currentNode.next;
                    }

                    return true;
                }
                else
                {
                    previousNode = currentNode;
                    currentNode = currentNode.next;
                }
            }

            return false;
        }

        /// <summary>
        /// print the linked list
        /// </summary>
        public override string ToString()
        {
            string linkedList = string.Empty;
            MyLinkedList? current = head;
            while (current != null)
            {
                linkedList += current.value + ",";
                current = current.next;
            }

            return linkedList;
        }
    }
}