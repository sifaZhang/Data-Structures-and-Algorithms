using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HelloWorld
{
    class Node
    {
        public int key;
        public Node? left, right;
        public Node(int item)
        {
            key = item;
            left = right = null;
        }
    }

    class MyBinaryTree
    {
        public Node? root; // Root of Binary Tree
        public MyBinaryTree(int key) { root = new Node(key); }
        public MyBinaryTree() { root = null; }

        public Node? search(Node root, int key)
        {
            if (root == null || root.key == key)
                return root;

            if (root.key < key)
                return search(root.right, key);

            return search(root.left, key);
        }

        public void insert(int key)
        {
            root = insertRec(root, key);
        }

        public Node insertRec(Node root, int key)
        {
            if (root == null)
            {
                root = new Node(key);
                return root;
            }

            if (key < root.key) 
                root.left = insertRec(root.left, key);
            else if (key > root.key) 
                root.right = insertRec(root.right, key);

            return root;
        }

        public int Height(Node? node)
        {
            if (node == null)
                return 0;
            else
            {
                int lHeight = Height(node.left);
                int rHeight = Height(node.right);
                if (lHeight > rHeight)
                    return (lHeight + 1);
                else
                    return (rHeight + 1);
            }
        }
    }

    internal class BinaryTree
    {
        public static void Run()
        {
            MyBinaryTree tree = new MyBinaryTree();
            tree.insert(1);
            tree.insert(2);
            tree.insert(3);
            tree.insert(4);
            tree.insert(5);
            tree.insert(6);
            tree.insert(7);

            Node? left = tree.root;
            while (true)
            {
                if (left == null) break;

                if (left.left != null)
                {
                    left = left.left;
                }
                else
                {
                    break;
                }
            }

            Node? right = tree.root;
            while (true)
            {
                if (right == null) break;

                if (right.right != null)
                {
                    right = right.right;
                }
                else
                {
                    break;
                }
            }

            if(left != null)
                Console.WriteLine("The smallest value is: " + left.key);

            if(right != null)
                Console.WriteLine("The largest value is: " + right.key);

        }



    }
}
