using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== 1. Verifying Constructors ==/n");
        Fraction fDefault = new Fraction();
        Fraction fOneParam = new Fraction(6);
        Fraction fTwoParam = new Fraction(6, 7);

        Console.WriteLine($"Default constructor (1/1): {fDefault.GetFractionString()}");
        Console.WriteLine($"One-parameter constructor (6/1): {fOneParam.GetFractionString()}");
        Console.WriteLine($"Two-parameter constructor (6/7): {fTwoParam.GetFractionString()}");
        Console.WriteLine();

        Console.WriteLine("=== 2. Verifying Getters and Setters ===");
        Fraction fTest = new Fraction();
        Console.WriteLine($"Initial values: {fTest.GetFractionString()}");
        
        fTest.SetTop(3);
        fTest.SetBottom(4);
        Console.WriteLine($"After setters (Top: {fTest.GetTop()}, Bottom: {fTest.GetBottom()}): {fTest.GetFractionString()}");
        Console.WriteLine();

        Console.WriteLine("=== 3. Verifying Representations (Sample Output Match) ===");
        Fraction sample1 = new Fraction(1);
        Fraction sample2 = new Fraction(5);
        Fraction sample3 = new Fraction(3, 4);
        Fraction sample4 = new Fraction(1, 3);

        DisplayFraction(sample1);
        DisplayFraction(sample2);
        DisplayFraction(sample3);
        DisplayFraction(sample4);
        Console.WriteLine();

        Console.WriteLine("=== 4. Practice Using Fraction Class (Random Loop) ===");
        Fraction practiceFraction = new Fraction();
        Random random = new Random();

        // Loop of 20 random fractions
        for (int i = 1; i <= 20; i++)
        {
            int randomTop = random.Next(1, 50);
            int randomBottom = random.Next(1, 50);

            practiceFraction.SetTop(randomTop);
            practiceFraction.SetBottom(randomBottom);

            Console.WriteLine($"Fraction {i}: string: {practiceFraction.GetFractionString()} Number: {practiceFraction.GetDecimalValue()}");
        }
    }

    static void DisplayFraction(Fraction f)
    {
        Console.WriteLine(f.GetFractionString());
        Console.WriteLine(f.GetDecimalValue());
    }
}