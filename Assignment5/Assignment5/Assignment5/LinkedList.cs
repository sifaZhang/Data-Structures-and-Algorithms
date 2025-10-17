//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/10/17

using Assignment5;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Assignment_5
{
    /// <summary>
    /// create a class for linked list, contains two elements
    /// </summary>
    class MyLinkedList<T>
    {
        public T value;
        public MyLinkedList<T>? next;

        public MyLinkedList(T value)
        {
            this.value = value;
            this.next = null;
        }
    }

    /// <summary>
    /// create a linked list contains a head and a tail.
    /// </summary>
    class LinkedList<T>
    {
        private MyLinkedList<T>? head;
        private MyLinkedList<T>? tail;

        public LinkedList()
        {
            head = null;
            tail = null;
        }

        public bool FindNode(T value)
        {
            MyLinkedList<T>? node = head;
            while (node != null)
            {
                if (EqualityComparer<T>.Default.Equals(node.value, value))
                {
                    return true;
                }
                node = node.next;
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
            MyLinkedList<T>? current = head;
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
        public bool GetHeadNode(out T value)
        {
            value = default;
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
        public void AddNode(T value) 
        {
            MyLinkedList<T> newNode = new MyLinkedList<T>(value);
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
        public bool RemoveFirstNode(out T vaule)
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
                vaule = default;
                return false;
            }
        }

        /// <summary>
        /// delete a node
        /// </summary>
        /// <param name="value"></param>
        public bool DeleteNode(T value)
        {
            MyLinkedList<T>? currentNode = head;
            MyLinkedList<T>? previousNode = head;
            while (currentNode != null && previousNode != null)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.value, value))
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
            MyLinkedList<T>? current = head;
            while (current != null)
            {
                linkedList += current.value + ",";
                current = current.next;
            }

            return linkedList;
        }

        /// <summary>
        /// to string
        /// </summary>
        /// <returns></returns>
        public T GetValueAt(int index)
        {
            if (index < 0 || index >= Size())
            {
                return default;
            }

            MyLinkedList<T>? current = head;
            int count = 0;
            while (current != null)
            {
                if (count == index)
                {
                    return current.value;
                }
                count++;
                current = current.next;
            }

            return default;
        }
    }
}