//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/10/17

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    class Node
    {
        public int Value;
        public Node? Left, Right;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="item"></param>
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

        /// <summary>
        /// constructor
        /// </summary>
        public PrintNode()
        {
            level = -1;
            occupied = false;
            data = null;
        }
    }

    internal class MaxHeap
    {
        Node? Root { get; set; }
        List<Node> nodeList;

        /// <summary>
        /// constructor
        /// </summary>
        public MaxHeap()
        {
            Root = null;
            nodeList = new List<Node>();
        }

        /// <summary>
        /// clear all data
        /// </summary>
        public void Clear()
        {
            Root = null;
            nodeList.Clear();
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

        /// <summary>
        /// print the heap as an array
        /// </summary>
        public void PrintHeap()
        {
            if (Root == null)
            {
                Console.WriteLine("The heap is empty.");
                return;
            }

            Console.Write("Current array: [");
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (i == nodeList.Count - 1)
                    Console.Write(nodeList[i].Value);
                else
                    Console.Write(nodeList[i].Value + ", ");
            }
            Console.WriteLine("]");
        }

        /// <summary>
        /// draw the heap
        /// </summary>
        public void DrawHeap()
        {
            PrintHeap();

            List<List<Node?>> treeData = new List<List<Node?>>();
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
                }

                //print the tree
                for (int k = 0; k <= heigth; k++)
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
                                line += "    ";
                            }
                            else
                            {
                                int data = listPrintNode[i].data.Value;
                                line += data.ToString("D4");
                            }
                        }
                        else
                        {
                            line += "    ";
                        }

                        //construnct next level line space
                        if (i + 1 < listPrintNode.Count
                            && listPrintNode[i + 1].level == k
                            && listPrintNode[i + 1].data != null
                            && listPrintNode[i + 1].data.Left != null)
                        {
                            lineSpace += "  /";
                        }
                        else if (i > 0
                            && listPrintNode[i - 1].level == k
                            && listPrintNode[i - 1].data != null
                            && listPrintNode[i - 1].data.Right != null)
                        {
                            lineSpace += "  \\  ";
                        }
                        else
                        {
                            lineSpace += "    ";
                        }
                    }

                    Console.WriteLine(line);
                    if (k != heigth) Console.WriteLine(lineSpace);   //no need to print space line for the last level
                }
            }
        }


        /// <summary>
        /// return the size of the heap
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return nodeList.Count;
        }


        /// <summary>
        /// find the last node which has no left child
        /// </summary>
        /// <returns></returns>
        public int LastNoLeftNode()
        {
            if (Size() < 1)
                return -1;
            else
                return Size() / 2 - 1;
        }

        /// <summary>
        /// add an array of values to the heap
        /// </summary>
        /// <param name="array"></param>
        public void Add(List<int> array)
        {
            foreach (int value in array)
            {
                Add(value);
            }
        }

        /// <summary>
        /// add a value to the heap
        /// </summary>
        /// <param name="value"></param>
        public void Add(int value)
        {
            Node newNode = new Node(value);
            nodeList.Add(newNode);
            if (Root == null)
            {
                Root = newNode;
            }
            else
            {
                int newNodeIndex = nodeList.Count - 1;
                int parentIndex = (newNodeIndex - 1) / 2;
                Node parentNode = nodeList[parentIndex];
                if (parentNode.Left == null)
                {
                    parentNode.Left = newNode;
                }
                else
                {
                    parentNode.Right = newNode;
                }
            }

            ReheapUp();
        }

        /// <summary>
        /// remove the root node from the heap
        /// </summary>
        public void Remove()
        {
            if (Root == null) return;
            if (Size() == 1)
            {
                Root = null;
                nodeList.RemoveAt(0);
                return;
            }

            int lastIndex = nodeList.Count - 1;
            Node lastNode = nodeList[lastIndex];
            int parentIndex = (lastIndex - 1) / 2;
            Node parentNode = nodeList[parentIndex];
            if (parentNode.Left == lastNode)
            {
                parentNode.Left = null;
            }
            else
            {
                parentNode.Right = null;
            }

            lastNode.Left = Root.Left;
            lastNode.Right = Root.Right;
            Root = lastNode;
            nodeList.RemoveAt(lastIndex);
            if (nodeList.Count > 0)
                nodeList[0] = Root;

            ReheapDown();
        }

        /// <summary>
        /// reheap up from the last node
        /// </summary>
        public void ReheapUp()
        {
            if (Root == null) return;
            int lastIndex = nodeList.Count - 1;
            Node lastNode = nodeList[lastIndex];
            ReheapUp(lastNode);
        }

        /// <summary>
        /// reheap up recursively
        /// </summary>
        /// <param name="node"></param>
        private void ReheapUp(Node node)
        {
            if (node == Root) return;

            int nodeIndex = nodeList.IndexOf(node);
            int parentIndex = (nodeIndex - 1) / 2;
            Node parentNode = nodeList[parentIndex];
            if (node.Value > parentNode.Value)
            {
                //swap node and parent
                int temp = node.Value;
                node.Value = parentNode.Value;
                parentNode.Value = temp;
                ReheapUp(parentNode);
            }
        }

        /// <summary>
        /// reheap down from the root node
        /// </summary>
        public void ReheapDown()
        {
            if (Root == null) return;
            ReheapDown(Root);
        }

        /// <summary>
        /// reheap down recursively
        /// </summary>
        /// <param name="node"></param>
        private void ReheapDown(Node node)
        {
            if (node.Left == null && node.Right == null) return;
            if (node.Left == null)
            {
                if (node.Value < node.Right.Value)
                {
                    //swap node and right
                    int temp = node.Value;
                    node.Value = node.Right.Value;
                    node.Right.Value = temp;
                    ReheapDown(node.Right);
                }
            }
            else if (node.Right == null)
            {
                if (node.Value < node.Left.Value)
                {
                    //swap node and left
                    int temp = node.Value;
                    node.Value = node.Left.Value;
                    node.Left.Value = temp;
                    ReheapDown(node.Left);
                }
            }
            else
            {
                if (node.Left.Value < node.Right.Value)
                {
                    if (node.Value < node.Right.Value)
                    {
                        //swap node and right
                        int temp = node.Value;
                        node.Value = node.Right.Value;
                        node.Right.Value = temp;
                        ReheapDown(node.Right);
                    }
                }
                else
                {
                    if (node.Value < node.Left.Value)
                    {
                        //swap node and left
                        int temp = node.Value;
                        node.Value = node.Left.Value;
                        node.Left.Value = temp;
                        ReheapDown(node.Left);
                    }
                }
            }
        }
    }
}
