//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/08/27

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal class Stack
    {
        private int top;
        private int[] storage;

        /// <summary>
        /// 10 element by default
        /// </summary>
        public Stack()
        {
            storage = new int[10];
            top = -1;
        }

        /// <summary>
        /// set the size of the stack
        /// </summary>
        /// <param name="size"></param>
        public Stack(int size)
        {
            storage = new int[size];
            top = -1;
        }

        /// <summary>
        /// add a new data
        /// </summary>
        /// <param name="value"></param>
        public void Push(int value)
        {
            if (top + 1 >= storage.Length)
            {
                int[] storageTemp = new int[storage.Length * 2];
                Array.Copy(storage, storageTemp, storage.Length);

                storage = storageTemp;
            }

            storage[++top] = value;
        }

        /// <summary>
        /// remove a data
        /// </summary>
        /// <param name="value"></param>
        /// <returns>the stack is not empty</returns>
        public bool Pop(out int value)
        {
            value = -1;
            if (top >= 0)
            {
                value = storage[top--];
                return true;
            }

            return false;
        }

        /// <summary>
        /// fetch the top data
        /// </summary>
        /// <param name="value"></param>
        /// <returns>has data in the stack</returns>
        public bool Peek(out int value)
        {
            value = -1;
            if (top >= 0)
            {
                value = storage[top];
                return true;
            }

            return false;
        }

        /// <summary>
        /// clear all data
        /// </summary>
        public void Clear()
        {
            top = -1;
        }

        /// <summary>
        /// determine whether it is full
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            return top + 1 == storage.Length;
        }

        /// <summary>
        /// determine whether it is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return top == -1;
        }

        /// <summary>
        /// get the current numnber of the stack
        /// </summary>
        /// <returns></returns>
        public int CurrentSize()
        {
            return top + 1;
        }

        /// <summary>
        /// get the capacity
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return storage.Length;
        }

        /// <summary>
        /// find a data, return the index if the data exist in the stack
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns>the data exist</returns>
        public bool Find(int value, out int index)
        {
            for (int i = top; i >= 0; i--)
            {
                if (storage[i] == value)
                {
                    index = top + 1 - i; 
                    return true;
                }
            }

            index = - 1;
            return false;
        }

        /// <summary>
        /// to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (IsEmpty()) return "[Empty Stack]";
            return "[" + string.Join(", ", storage.Take(top + 1)) + " ←Top]";
        }
    }
}
