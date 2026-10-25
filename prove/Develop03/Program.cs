using System;

class Program
{
    // Exceeding Requirements Description:
    // Added an interactive restart option at the end of the memorization session 
    // allowing users to practice the scripture again without restarting the application.
    static void Main(string[] args)
    {
        Scripture scripture = new Scripture("Proverbs", 3, 5, 6, "Trust in the Lord with all thine heart and lean not unto thine own understanding");

        bool keepPlaying = true;
        while (keepPlaying)
        {
            scripture.Reset();
            
            while (true)
            {
                Console.Clear();
                scripture.ShowScripture();

                Console.WriteLine("\nPress enter to continue or type 'quit' to finish:");
                string input = Console.ReadLine();

                if (input != null && input.Trim().ToLower() == "quit")
                {
                    keepPlaying = false;
                    break;
                }

                bool hasMoreWords = scripture.HideSomeWords();
                if (!hasMoreWords)
                {
                    Console.Clear();
                    scripture.ShowScripture();
                    break;
                }
            }

            if (!keepPlaying) break;

            Console.WriteLine("\nAll words are hidden. Would you like to restart? (yes/no):");
            string restartChoice = Console.ReadLine();
            if (restartChoice == null || restartChoice.Trim().ToLower() != "yes")
            {
                keepPlaying = false;
            }
        }

        Console.WriteLine("Goodbye!");
    }
}