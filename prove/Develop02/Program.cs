using System;

class Program
{
    static void Main(string[] args)
    {
        Menu myMenu = new Menu();
        Journal myJournal = new Journal();
        Random randomGenerator = new Random();

        List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is something new I learned today?"
        };

        int response = 0;

        while (response != 5)
        {
            response = myMenu.ProcessMenu();

            switch (response)
            {
                case 1:
                    int promptIndex = randomGenerator.Next(prompts.Count);
                    string selectedPrompt = prompts[promptIndex];

                    Console.WriteLine($"\nPrompt: {selectedPrompt}");
                    Console.Write("> ");
                    string userResponse = Console.ReadLine();

                    Console.Write("Rate your mood today (1-5): ");
                    string moodRating = Console.ReadLine();

                    string currentDate = DateTime.Now.ToShortDateString();

                    JournalEntry newEntry = new JournalEntry(currentDate, selectedPrompt, userResponse, moodRating);
                    myJournal.AddEntry(newEntry);
                    Console.WriteLine("Entry recorded!\n");
                    break;

                case 2:
                    myJournal.DisplayAll();
                    break;

                case 3:
                    Console.Write("What is the filename to save to? ");
                    string saveFilename = Console.ReadLine();
                    myJournal.WriteToFile(saveFilename);
                    break;

                case 4:
                    Console.Write("What is the filename to load from? ");
                    string loadFilename = Console.ReadLine();
                    myJournal.ReadFromFile(loadFilename);
                    break;

                case 5:
                    Console.WriteLine("\nGoodbye!");
                    break;
            }
        }
    }
}