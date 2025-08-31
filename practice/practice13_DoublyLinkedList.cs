using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HelloWorld
{
    class DoublyLinkedListElement<T>
    {
        public DoublyLinkedListElement<T> Prev { get; set; }
        public DoublyLinkedListElement<T> Next { get; set; }
        public T Value { get; set; }

        public DoublyLinkedListElement(T value)
        {
            Value = value;
            Prev = this;
            Next = this;
        }
    }

    internal class DoublyLinkedList<T>
    {
        DoublyLinkedListElement<T>? head = null;

        private void Insert(T value)
        {
            var newNode = new DoublyLinkedListElement<T>(value);

            if (head != null)
            {
                var tail = head.Prev;
                tail.Next = newNode;
                newNode.Prev = tail;
                newNode.Next = head;
                head.Prev = newNode;
            }
            else
            {
                head = newNode;
            }
        }

        private bool Delete(T value)
        {
            DoublyLinkedListElement<T>? current = head;

            if (current == null) return false;

            do
            {
                if (object.Equals(current.Value, value))
                {
                    if (current.Next == current)
                    {
                        head = null;
                    }
                    else
                    {
                        current.Prev!.Next = current.Next;
                        current.Next!.Prev = current.Prev;

                        if (current == head)
                            head = current.Next;
                    }

                    return true;
                }

                current = current.Next;

            } while (current != head);

            return false;
        }

        private int Size()
        {
            int size = 0;
            DoublyLinkedListElement<T>? current = head;

            if (current == null) return size;

            do
            {
                size++;
                current = current.Next;

            } while (current != head);

            return size;
        }

        private void Print()
        {
            DoublyLinkedListElement<T>? current = head;

            if (current == null) return;

            do
            {
                Console.WriteLine(current.Value + ",");
                current = current.Next;
            } while (current != head);
        }
    }
}
