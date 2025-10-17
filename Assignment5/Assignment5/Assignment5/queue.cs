//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/10/17

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{
    internal class MyQueue
    {
        private LinkedList<string> myList;

        /// <summary>
        /// constructor
        /// </summary>
        public MyQueue()
        {
            myList = new LinkedList<string>();
        }

        /// <summary>
        /// add a new data
        /// </summary>
        /// <param name="value"></param>
        public void Enqueue(string value)
        {
            myList.AddNode(value);
        }

        /// <summary>
        /// remove a data
        /// </summary>
        /// <param name="value"></param>
        public bool Dequeue(out string vaule)
        {
            return myList.RemoveFirstNode(out vaule);
        }

        /// <summary>
        /// fetch the top data
        /// </summary>
        /// <param name="value"></param>
        /// <returns>has data in the stack</returns>
        public bool Peek(out string value)
        {
            return myList.GetHeadNode(out value);
        }

        /// <summary>
        /// clear all data
        /// </summary>
        public void Clear()
        {
            myList.Clear();
        }

        
        /// <summary>
        /// determine whether it is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return myList.Size() == 0;
        }


        /// <summary>
        /// find a data, return the index if the data exist in the stack
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns>the data exist</returns>
        public bool Contains(string value)
        {
            return myList.FindNode(value);
        }

        /// <summary>
        /// to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (myList.Size() == 0) return "[Empty Queue]";
            string linkedList = myList.ToString();
            return "[" + "Head→ " + linkedList + " ←Tail]";
        }

        /// <summary>
        /// find the value of the queue at the index
        /// </summary>
        /// <returns>out of size: -1</returns>
        public string GetValueAt(int index)
        {
            return myList.GetValueAt(index);
        }
    }
}
