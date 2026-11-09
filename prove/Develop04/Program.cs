using System;

public class Program
{
    private static void DisplayScripture()
{
    string[] scriptures =
    {
        "Joshua 1:9 - Be strong and of a good courage: for unto this people shalt thou divide for an inheritance the land, which I sware unto their fathers to give them.",
        "Philippians 4:13 - I can do all things through Christ which strengtheneth me.",
        "Proverbs 3:5 - Trust in the Lord with all thine heart; and lean not unto thine own understanding."
    };

    Random random = new Random();

    Console.WriteLine("\nBefore you go, here is a scripture:");
    Console.WriteLine(scriptures[random.Next(scriptures.Length)]);
    Console.WriteLine("\nPress Enter to exit.");
    Console.ReadLine();
}
    public static void Main(string[] args)
    {
        // The program follows the same terminal flow as the demonstration:
        // each activity returns to the menu after its completion message.
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine() ?? string.Empty;

            if (choice == "1")
            {
                new BreathingActivity().Run();
            }
            else if (choice == "2")
            {
                new ReflectionActivity().Run();
            }
            else if (choice == "3")
            {
                new ListingActivity().Run();
            }
            else if (choice == "4")
            {
                DisplayScripture();
                running = false;
            }
            else
            {
                Console.WriteLine("\nThat is not a valid option.");
                Console.WriteLine("Press Enter to try again.");
                Console.ReadLine();
            }
        }
    }
}
