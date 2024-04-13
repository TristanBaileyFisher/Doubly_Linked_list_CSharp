using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C2_Algorithms
{
    internal class Node
    {
        public string Word { get; set; }
        public int Length { get; set; }

        public Node Next { get; set; }

        public Node Prev { get; set; }

        public Node()
        {
            Word = null;
            Length = 0;
            Next = null;
            Prev = null;
        }

        public Node(string word, int length)
        {
            this.Word = word;
            this.Length = length;
            Next = null;
            Prev = null;
        }
        internal class DoublyLinkedList
        {
            public Node Head { get; set; }
            public Node Tail { get; set; }

            public DoublyLinkedList()
            {
                Head = null;
                Tail = null;
            }

            public void RemoveDuplicates()
            {
                HashSet<string> uniqueWords = new HashSet<string>();
                Node current = Head;

                while (current != null)
                {
                    if (uniqueWords.Contains(current.Word))
                    {
                        Node nextNode = current.Next;
                        RemoveNode(current);
                        current = nextNode;
                    }
                    else
                    {
                        uniqueWords.Add(current.Word);
                        current = current.Next;
                    }
                }
            }
            private void RemoveNode(Node node)
            {
                if (node.Prev != null)
                    node.Prev.Next = node.Next;
                else
                    Head = node.Next;

                if (node.Next != null)
                    node.Next.Prev = node.Prev;

                if (node == Tail)
                    Tail = node.Prev;
            }


            

        }
    }
}
