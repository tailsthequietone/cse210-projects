using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeMessage();

        string userName = PromptUserName();
        int userNumber = PromptUserNumber();

        int squaredNumber = SquareNumber(userNumber);

        int birthYear;
        PromptUserBirthYear(out birthYear);


        DisplayResult(userName, squaredNumber, birthYear);
    }

    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string userName = Console.ReadLine();
        return userName;
    }

    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int userNumber = int.Parse(Console.ReadLine());
        return userNumber;
    }
    
    static void PromptUserBirthYear(out int userbirthYear)
    {
        Console.Write($"Please enter the year you were born: ");
        userbirthYear = int.Parse(Console.ReadLine());

    }

    static int SquareNumber(int userNumber)
    {
        int square = userNumber * userNumber;
        return square;
    }

    static void DisplayResult(string name, int square, int birthYear)
    {
        int currentYear = DateTime.Now.Year;
        Console.WriteLine($"{name}, the square of your number is {square}.");
        Console.WriteLine($"{name}, you will turn {currentYear - birthYear} years old this year.");
    }
}