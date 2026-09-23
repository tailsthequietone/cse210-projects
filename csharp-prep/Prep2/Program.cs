using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter your grade: ");
        string input = Console.ReadLine();
        int percentage = int.Parse(input);

        string letter = "";

        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else if (percentage < 60)
        {
            letter = "F";
        }

        Console.Write($"Your grade is: {letter}");


        if (percentage >= 93 || percentage < 60)
        {
            Console.WriteLine("!");
        }
        else if (percentage % 10 >= 7 && percentage > 93)
        {
            Console.WriteLine("+!");
        }
        else if (percentage % 10 < 3 && percentage <= 60)
        {
            Console.WriteLine("-!");
        }
        else if (percentage % 10 < 3 && percentage > 60)
        {
            Console.WriteLine("-!");
        }



        if (percentage >= 70)
        {
            Console.WriteLine("You passed, great work!");
        }
        else if (percentage < 70)
        {
            Console.WriteLine("You failed, but don't be discouraged!");
        }



    }
}