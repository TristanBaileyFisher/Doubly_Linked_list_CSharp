using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace C2_Algorithms
{
    internal class Program
    {


        static void Main(string[] args)
        {

            DoublyLinkedList myList = new DoublyLinkedList();
            FindFiles(myList);
            Menu(myList);
        }


        static public void Menu(DoublyLinkedList list)
        {


            Console.Clear();
            Console.WriteLine("===================================================");
            Console.WriteLine("                    Main Menu");
            Console.WriteLine("===================================================");
            Console.WriteLine("1. Insert");
            Console.WriteLine("2. Delete");
            Console.WriteLine("3. Find");
            Console.WriteLine("4. Print");
            Console.WriteLine("5. New");
            Console.WriteLine("6. Exit");
            Console.WriteLine("---------------------------------------------------");
            Console.Write("Please type the option of your choice: ");
            string UserChoice = Console.ReadLine();

            if (UserChoice == "1")
            {
                list.Insert();
            }
            else if (UserChoice == "2")
            {
                list.Delete();
            }
            else if (UserChoice == "3")
            {
                list.Find();
            }
            else if (UserChoice == "4")
            {
                list.Print();
            }
            else if (UserChoice == "5")
            {
                
                Wipe(list);
                FindFiles(list);
                Menu(list);
            }
            else if (UserChoice == "6")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Write("Sorry that is not a valid option, please try again...");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Menu(list);
            }

        }
        static void Wipe(DoublyLinkedList list)
        {
            Console.WriteLine("Are you sure you wish to wipe the entire directory? y/n");
            string userAnswer = Console.ReadLine();
            if (userAnswer == "y")
            {
                int beforeWipeCount = list.CountNodes();
                list.Head = null;
                list.Tail = null;
                int WipeCount = list.CountNodes();

                Console.WriteLine("Doubly Linked List has been Cleared.");
                Console.WriteLine($"Before wipe: {beforeWipeCount}");
                Console.WriteLine($"After wipe: {WipeCount}");


            }

            else
            {
                Menu(list);
            }
        }


        private Node Head;
     
      
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

        #region FIND FILES
        static void FindFiles(DoublyLinkedList list)
        {
            Console.Clear();
            //turn folder destination into vairables
            string path = @"C:\Users\trist\OneDrive\Desktop\C2_Algorithms\dictionary";

            //Turn folder chosen and txt files into floating variables
            string[] txtFiles = Directory.GetFiles(path, "*.txt");
            //select a file text
            Console.WriteLine("Select a .txt file to open: ");
            //for each txt file in ordered an integer of 1 will be given to each file
            for (int i = 0; i < txtFiles.Length; i++)
            {//loops adding on integers until all files have processed
                Console.WriteLine($"{i + 1}. {Path.GetFileName(txtFiles[i])}");
            }
            int selectedFileIndex;                      //is set to chosen integer that the user provides
            string userFile = Console.ReadLine();       //read the user input
            if (int.TryParse(userFile, out selectedFileIndex) && selectedFileIndex >= 1 && selectedFileIndex <= txtFiles.Length)         //turns user input to integer(parse) 
                                                                                                                                         //if integer is = or more than 1 and = or less than the txtfile max integer
            {
                string selectedFilePath = txtFiles[selectedFileIndex - 1]; //will contain the users chosen file path
                Console.WriteLine($"Now Loading: {selectedFilePath}");
                string[] fileContents = System.IO.File.ReadAllLines(selectedFilePath); // read all lines in the chosen file
                ProcessData(fileContents, list); // send to process the data for the dictionary
            }
            else
            {
                Console.WriteLine(selectedFileIndex + " is not a valid option, would you like to try again? (yes/no)");
                string userAnswer = Console.ReadLine().ToLower();
                if (userAnswer == "yes")
                {
                    FindFiles(list);
                }
                else
                {
                    System.Environment.Exit(0);
                }
            }

            // ... (previous code)

  

        }
            
    
        #endregion

        #region PROCESS DATA


        static void ProcessData(string[] fileContents, DoublyLinkedList list)
        {
            HashSet<string> uniqueWords = new HashSet<string>();

            foreach (string word in fileContents)
            {
                if (word.Contains("#"))
                {
                    // Skip words with '#'
                    continue;
                }

                if (!uniqueWords.Contains(word))
                {
                    // Create a new Node with the word and its length
                    Node newNode = new Node(word, word.Length);

                    // Insert the new Node into your linked list
                    list.EndInsert(newNode); // You can choose 'EndInsert' or 'FrontInsert' based on your requirements

                    // Add the word to the HashSet to track uniqueness
                    uniqueWords.Add(word);
                }
            }
        }
    }
}




        #endregion


