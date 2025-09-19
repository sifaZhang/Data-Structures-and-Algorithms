//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/09/19

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
    class PrintNode
    {
        public int level;
        public bool occupied;
        public Node? data;

        public PrintNode()
        {
            level = -1;
            occupied = false;
            data = null;
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

        private void PrintData(List<PrintNode> listPrintNode)
        {
            string printnodeStr = "";
            for (int i = 0; i < listPrintNode.Count; i++)
            {
                if (listPrintNode[i].occupied)
                {
                    printnodeStr += "(yes," + (listPrintNode[i].data == null ? "*" : listPrintNode[i].data.Value.ToString()) + "," + listPrintNode[i].level.ToString() + ")";
                }
                else
                {
                    printnodeStr += "(no,#," + listPrintNode[i].level.ToString() + ")";
                }

                printnodeStr += " ";
            }
            Console.WriteLine(printnodeStr);
        }

        public void DrawTree()
        {
            List<List<Node?>> treeData =  new List<List<Node?>>();
            List<Node?> currentNodes = new List<Node?>();
            currentNodes.Add(Root);
            treeData.Add(currentNodes);

            //find all nodes in each level
            int heigth = Height();
            int curentHeight = 0;
            while (curentHeight < heigth)
            {
                List<Node?> tempNodes = new List<Node?>();
                for (int i = 0; i < currentNodes.Count; i++)
                {
                    Node? node = currentNodes[i];
                    if (node == null)
                    {
                        tempNodes.Add(null);
                        tempNodes.Add(null);
                    }
                    else
                    {
                        tempNodes.Add(node.Left == null ? null : node.Left);
                        tempNodes.Add(node.Right == null ? null : node.Right);
                    }
                }

                treeData.Add(tempNodes);
                currentNodes = tempNodes;
                curentHeight++;
            }

            //fill the print node list
            if (treeData.Count > 0)
            {
                //fill the last level first
                List<PrintNode> listPrintNode = new List<PrintNode>();
                List<Node?> lastNodes = treeData[treeData.Count - 1];
                for (int j = 0; j < lastNodes.Count; j++)
                {
                    PrintNode printNode = new PrintNode();
                    printNode.level = treeData.Count - 1;
                    Node? node = lastNodes[j];
                    if (node != null)
                    {
                        printNode.data = node;
                    }

                    printNode.occupied = true;
                    listPrintNode.Add(printNode);

                    if (j != lastNodes.Count - 1) listPrintNode.Add(new PrintNode());
                }

                //fill other levels
                for (int j = treeData.Count - 2; j >= 0; j--)
                {
                    List<Node?> currentTreeNode = treeData[j];
                    int findIndex = 0;
                    int k = 0;
                    for (int i = 0; i < currentTreeNode.Count; i++)
                    {
                        Node? currentNode = currentTreeNode[i];
                        for (; k < listPrintNode.Count; k++)
                        {
                            PrintNode tempNode = listPrintNode[k];
                            if (!tempNode.occupied)
                            {
                                findIndex++;
                                if (findIndex % 2 == 1)
                                {
                                    tempNode.occupied = true;
                                    tempNode.level = j;
                                    tempNode.data = currentNode;
                                    break;
                                }
                            }
                        }
                    }

                    //PrintData(listPrintNode);
                }

                //PrintData(listPrintNode);

                //print the tree
                for ( int k = 0; k <= heigth; k++)
                {
                    string line = "Height " + k.ToString() + ": ";
                    string lineSpace = "          ";
                    for (int i = 0; i < listPrintNode.Count; i++)
                    {
                        //construct current level data line
                        if (listPrintNode[i].level == k)
                        {
                            if (listPrintNode[i].data == null)
                            {
                                line += "  ";
                            }
                            else
                            {
                                int data = listPrintNode[i].data.Value;
                                line += data.ToString("D2");
                            }
                        }
                        else
                        {
                            line += "  ";
                        }

                        //construnct next level line space
                        if (i + 1 < listPrintNode.Count 
                            && listPrintNode[i + 1].level == k
                            && listPrintNode[i + 1].data != null 
                            && listPrintNode[i + 1].data.Left != null)
                        {
                            lineSpace += " /";
                        }
                        else if (i > 0 
                            && listPrintNode[i - 1].level == k
                            && listPrintNode[i - 1].data != null
                            && listPrintNode[i - 1].data.Right != null)
                        {
                            lineSpace += "\\ ";
                        }
                        else
                        {
                            lineSpace += "  ";
                        }
                    }
                        
                    Console.WriteLine(line);
                    if(k != heigth) Console.WriteLine(lineSpace);   //no need to print space line for the last level
                }
            }
        }
    }
}
