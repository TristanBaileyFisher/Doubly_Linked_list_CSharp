using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace C2_Algorithms
{
    internal class DoublyLinkedList
    {

        public Node Head { get; set; }
        public Node Tail { get; set; }
        public Node Current { get; set; }
        public int Counter { get; set; }

        public DoublyLinkedList()
        {
            Head = null;
            Tail = null;
            Counter = 0;
            Current = null;
        }

        public int CountNodes()
        {
            int count = 0;
            Node current = Head;

            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }




        public void Insert()
        {
            Console.Clear();
            Console.WriteLine("Where would you like to insert your word? please select either option 1 or 2:");
            Console.WriteLine("1. Add to Front");
            Console.WriteLine("2. Add to End");
            Console.WriteLine("3. Add after word");

            string UserChoice = Console.ReadLine();

            if (UserChoice == "1")
            {
                Console.WriteLine("Please enter the new word to be inserted at the Front: ");
                string newWord = Console.ReadLine();
                Node newNode = new Node(newWord, newWord.Length);
                FrontInsert(newNode);
                int pos = Peek(newNode); // Call Peek without an argument to get the position of the newly inserted node.

                Console.WriteLine("===============================================");
                Console.WriteLine("Index Number |    Word    |    Length");
                Console.WriteLine("===============================================");
                Console.WriteLine($"{pos,-13} | {newWord,-10} | {newWord.Length}");
            }
            else if (UserChoice == "2")
            {
                Console.WriteLine("Please enter the new word to be inserted at the End: ");
                string newWord = Console.ReadLine();
                Node newNode = new Node(newWord, newWord.Length);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                EndInsert(newNode);
                
                int pos = Peek(newNode); // Call Peek without an argument to get the position of the newly inserted node.

                Console.WriteLine("===============================================");
                Console.WriteLine("Index Number |    Word    |    Length");
                Console.WriteLine("===============================================");
                Console.WriteLine($"{pos,-13} | {newWord,-10} | {newWord.Length}");
                sw.Stop();
                TimeSpan timespan = sw.Elapsed;
                Console.WriteLine(@"- Time taken to insert " + newWord);
                Console.WriteLine("Time: " + timespan.ToString(@"mm\:ss\.ffffff") + " seconds\n");
            }
            else if (UserChoice == "3")
            {
                int Count = CountNodes();
                Console.WriteLine($"There are {Count} Nodes to Select");
                Console.WriteLine("Please input a number for your new word to be placed after: ");
                int UserPlace = int.Parse(Console.ReadLine());




                Console.WriteLine($"Please enter the new word to be inserted after Node {UserPlace}: ");
                string newWord = Console.ReadLine();
                Node newNode = new Node(newWord, newWord.Length);

                
                Node targetNode = Head;
                for (int i = 1; i < UserPlace; i++)
                {
                    targetNode = targetNode.Next;
                }
                Stopwatch sw = new Stopwatch();
                sw.Start();
                bool inserted = InsertAfter(newNode, targetNode);
                int pos = Peek(newNode); // Call Peek without an argument to get the position of the newly inserted node.
                if (inserted)
                {
                    Console.WriteLine("===============================================");
                    Console.WriteLine("Index Number |    Word    |    Length");
                    Console.WriteLine("===============================================");
                    Console.WriteLine($"{pos,-13} | {newWord,-10} | {newWord.Length}");
                }
                sw.Stop();

                TimeSpan timespan = sw.Elapsed;
                // Print the elapsed time in minutes:seconds and as seconds
                Console.WriteLine(@"- Time taken to insert " + newWord + " after: " + UserPlace);
                Console.WriteLine("Time: " + timespan.ToString(@"mm\:ss\.ffffff") + " seconds\n");

            }

            //return to menu
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
            Program.Menu(this);
        }

        private int Peek(Node nodeToFind)
        {
            int pos = 0;
            Node current = Head;

            while (current != null)
            {
                if (current == nodeToFind)
                {
                    return pos + 1; // Position is 1-based
                }
                pos++;
                current = current.Next;
            }

            return -1; // Node not found
        }




        #region FRONT AND END INSERTS
        public void EndInsert(Node node)
        {


            if (Head == null)
            {
                Head = node;
                Tail = node;
                Current = node;
            }
            else
            {
                Tail.Next = node;
                node.Prev = Tail;

                Tail = node;
                Current = node;
            }
            Counter++;
        }

        public void FrontInsert(Node node)
        {
            if (Head == null)
            {
                Head = node;
                Tail = node;
                Current = node;
            }
            else
            {
                node.Next = Head;
                Head.Prev = node;


                Head = node;
                Current = node;
            }
            Counter++;
        }


        private bool InsertAfter(Node node, Node targetNode)
        {
            bool inserted = false;
            if (Head == null)
            {
                return inserted;
            }
            Current = Head;
            while (Current != null && !inserted)
            {
                if (Current.Word == targetNode.Word)
                {
                    if (Current == Tail)
                    {
                        EndInsert(node);
                    }
                    else
                    {
                        node.Next = Current.Next;
                        node.Prev = Current;
                        node.Next.Prev = node;
                        Current.Next = node;
                        Current = node;
                    }
                    inserted = true;
                    Counter++;
                }
                else
                {
                    Current = Current.Next;
                }
            }
            return inserted;
        }
        #endregion


        public void Print()
        {
            Console.WriteLine("Which way do you want the list to be printed? (Forward/backward  F/B)");
            string Userdirect = Console.ReadLine().ToLower();
            if (Userdirect == "f")
            {
                Node Current = Head;
                int pos = 0;
                Console.WriteLine("===================================================");
                Console.WriteLine("Index Number    |    Word    |    Length");
                Console.WriteLine("===================================================");

                while (Current != null)
                {
                    pos++;
                    Console.WriteLine($"{pos,-15} | {Current.Word,-10} | {Current.Length}");
                    Current = Current.Next;
                    
                }
                Console.WriteLine();
            }
            else if(Userdirect == "b")
            {
                Node Current = Tail;
                int pos = 0;
                Console.WriteLine("===================================================");
                Console.WriteLine("Index Number    |    Word    |    Length");
                Console.WriteLine("===================================================");

                while (Current != null)
                {
                    pos++;
                    Console.WriteLine($"{pos,-15} | {Current.Word,-10} | {Current.Length}");
                    Current = Current.Prev;

                }
                Console.WriteLine();
            }
            //return to menu
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
            Program.Menu(this);

        }
        public void Delete()
        {
            Console.Clear();
            Console.WriteLine("Please select What/Where you would like to delete from: ");
            Console.WriteLine("\n1. Front");
            Console.WriteLine("2. End");
            Console.WriteLine("3. Word");
            string UserDelete = Console.ReadLine().ToLower();
            if (UserDelete == "1")
            {
                DeleteAtFront();
            }
            else if (UserDelete == "2")
            {
                DeleteAtEnd();
            }
            else if (UserDelete == "3")
            {
                DeleteNode();
            }
            else
            {
                Console.WriteLine("Sorry, this is not an option, please press Y to try again, or any other key to return to Menu");
                string UserError = Console.ReadLine().ToLower();
                if (UserError == "y")
                {
                    Delete();
                }
                else
                {
                    Program.Menu(this);
                }
            }
            Console.WriteLine("Do you wish to Delete something else? y/n");
            string Userchoice = Console.ReadLine().ToLower();
            if(Userchoice == "y")
            {
                Delete();
            }
            else
            {
               
                Program.Menu(this);
            }
        }

        private Node DeleteAtFront()
        {
            if (Head == null)
            {
                return null;
            }
            else
            {
                Node nodeToRemove = new Node();
                nodeToRemove = Head;

                //reassign head to next node in list
                Head = Head.Next;
                Head.Prev = null;
                Current = Head;
                Counter--;

                Console.WriteLine($"The word at the front '{nodeToRemove.Word}' has been removed.\n The new word at the front is '{Head.Word}'");

                return nodeToRemove;

            }
        }
        private Node DeleteAtEnd()
        {
            if (Head == null)
            {//list is empty
                return null;
            }
            else
            {
                Node nodeToRemove = new Node();
                nodeToRemove = Tail;

                //reassign Tail to previous node in list
                Tail = Tail.Prev;
                Tail.Next = null;
                Current = Tail;
                Counter--;

                Console.WriteLine($"The word at the End '{nodeToRemove.Word}' has been removed.\n The new word at the front is '{Tail.Word}'");

                return nodeToRemove;
            }
        }


        //Remove a specific Node
        private Node DeleteNode()
        {

            Console.WriteLine("Enter the Word you wish to Delete: ");
            string nodeToDelete = Console.ReadLine().ToLower();

            

            Node nodeToRemove = null;
            if (Head == null)
            {//list is empty
                nodeToRemove = null;
            }
            else if (Head.Word == nodeToDelete)
            {
                nodeToRemove = Head;
                DeleteAtFront();
            }
            else if (Tail.Word == nodeToDelete  )
            {
                nodeToRemove = Tail;
                DeleteAtEnd();
            }
            else
            {//node in middle traverse through list
                Current = Head;

                while (Current != null)
                {
                    if (Current.Word == nodeToDelete)
                    {
                        // Found node, use the previous node and next node to remove the current node from the list
                        nodeToRemove = Current;
                        if (Current.Prev != null)
                            Current.Prev.Next = Current.Next;
                        else
                            Head = Current.Next;

                        if (Current.Next != null)
                            Current.Next.Prev = Current.Prev;

                        if (Current == Tail)
                            Tail = Current.Prev;

                        Counter--;
                        break;
                    }
                    Current = Current.Next;
                }

                
                if (nodeToRemove == null)
                {
                    Console.WriteLine($"{nodeToDelete} was not found in the list");
                }
                else
                {
                    Console.WriteLine($"{nodeToDelete} has been removed");
                }
               
            }

            return nodeToRemove;
        }


        public int Find()
        {
            Console.Clear();
            Console.WriteLine("Enter the Word you wish to Find: ");
            string nodeToFind = Console.ReadLine().ToLower();
            int pos = 0;

            if (Head == null)
            {
                Console.WriteLine($"The list is empty. The word '{nodeToFind}' could not be found.");
            }
            else
            {
                Current = Head;
                bool found = false;
                while (Current != null && !found)
                {
                    if (Current.Word == nodeToFind)
                    {
                        found = true;
                    }
                    else
                    {
                        Current = Current.Next;
                    }
                    pos++;
                }

                if (found)
                {
                    Console.WriteLine($"The word '{nodeToFind}' was found at position {pos}");
                }
                else
                {
                    Console.WriteLine($"Sorry, the word '{nodeToFind}' could not be found.");
                }
            }

            Console.WriteLine("Do you wish to find another word? y/n");
            string userChoice = Console.ReadLine().ToLower();
            if (userChoice == "y")
            {
                Find();
            }
            else
            {
                Program.Menu(this);
            }

            return pos;
        }

    }

}
