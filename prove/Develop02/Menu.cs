using System;

class Menu
{
    public int ProcessMenu()
    {
        int response = 0;

        while (response < 1 || response > 5)
        {
            Console.WriteLine("Welcome to the Journaling Program.");
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            string input = Console.ReadLine();
            int.TryParse(input, out response);

            if (response < 1 || response > 5)
            {
                Console.WriteLine("\nInvalid choice. Please enter a number from 1 to 5.\n");
            }
        }
        return response;
    }
}