//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/09/19

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    class Node
    {
        public int Value;
        public Node? Left, Right;

        public Node(int item)
        {
            Value = item;
            Left = Right = null;
        }
    }

    internal class BinarySearchTree
    {
        public Node? Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }

        /// <summary>
        /// clear all data
        /// </summary>
        public void Clear()
        {
            Root = null;
        }

        /// <summary>
        /// insert a new data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Insert(int value)
        {
            Root = InsertRec(Root, value);

            return true;
        }

        /// <summary>
        /// insert a new data recursively
        /// </summary>
        /// <param name="root"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Node InsertRec(Node? root, int value)
        {
            if (root == null)
            {
                root = new Node(value);
                return root;
            }

            if (value < root.Value)
                root.Left = InsertRec(root.Left, value);
            else if (value > root.Value)
                root.Right = InsertRec(root.Right, value);

            return root;
        }

        /// <summary>
        /// search a data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node? Search(int value)
        {
            return SearchRec(Root, value);
        }

        /// <summary>
        /// search a data recursively
        /// </summary>
        /// <param name="root"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Node? SearchRec(Node? root, int value)
        {
            if (root == null)
                return null;
            if (root.Value == value)
                return root;

            return value < root.Value ? SearchRec(root.Left, value) : SearchRec(root.Right, value);
        }

        /// <summary>
        /// in-order traversal
        /// </summary>
        /// <returns></returns>
        public string InOrder()
        {
            return InOrderRec(Root);
        }

        /// <summary>
        /// in-order traversal recursively
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private string InOrderRec(Node? root)
        {
            string output = "";

            if (root != null)
            {
                output += InOrderRec(root.Left);
                output += root.Value + " ";
                output += InOrderRec(root.Right);
            }

            return output;
        }

        /// <summary>
        /// pre-order traversal
        /// </summary>
        /// <returns></returns>
        public string PreOrder()
        {
            return PreOrderRec(Root);
        }

        /// <summary>
        /// pre-order traversal recursively
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private string PreOrderRec(Node? root)
        {
            string output = "";
            if (root != null)
            {
                output += root.Value + " ";
                output += PreOrderRec(root.Left);
                output += PreOrderRec(root.Right);
            }
            return output;
        }

        /// <summary>
        /// get the smallest data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int Smallest()
        {
            if (Root == null)
                throw new InvalidOperationException("The tree is empty.");

            Node current = Root;
            while (current.Left != null)
            {
                current = current.Left;
            }

            return current.Value;
        }

        /// <summary>
        /// get the largest data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int Largest()
        {
            if (Root == null)
                throw new InvalidOperationException("The tree is empty.");

            Node current = Root;
            while (current.Right != null)
            {
                current = current.Right;
            }

            return current.Value;
        }

        /// <summary>
        /// get the height of the tree
        /// </summary>
        /// <returns></returns>
        public int Height()
        {
            return HeightRec(Root);
        }

        /// <summary>
        /// get the height of the tree recursively
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private int HeightRec(Node? root)
        {
            if (root == null)
                return -1;

            int leftHeight = HeightRec(root.Left);
            int rightHeight = HeightRec(root.Right);

            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }
}
