using System;

class Program
{
    static void Main(string[] args)
    {
        string FirstName;
        string LastName;

        Console.Write("Please enter your first name: ");
        FirstName = Console.ReadLine();

        Console.Write("Please enter your last name: ");
        LastName = Console.ReadLine();

        Console.WriteLine($"Your name is: {LastName}, {FirstName} {LastName}");
    }
}