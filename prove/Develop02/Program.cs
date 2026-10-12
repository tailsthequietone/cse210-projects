using System;
using System.ComponentModel;

class Program
{
    static void Main(string[] args)
    {
        Menu myMenu = new Menu();

        int responce = 0;
        JournalEntry newEntry = new JournalEntry();

        while(responce != 5)
        {
            responce = myMenu.ProcessMenu();
            switch(responce)
            {
                case 1:
                    Console.WriteLine("Create");
                    newEntry.CreateJournalEntry();
                    break;
                case 2:
                    Console.WriteLine("Display");
                    break;
                case 3:
                    Console.WriteLine("Save");
                    break;
                case 4:
                    Console.WriteLine("Write");
                    break;
            }
        }
    }
}