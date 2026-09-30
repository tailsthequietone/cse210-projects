using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        
        int userNumber = -1;
        do
        {
            Console.Write("Enter a number (0 to quit): ");
            
            string userResponse = Console.ReadLine();
            userNumber = int.Parse(userResponse);
            
            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }
        }while (userNumber != 0);

        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        Console.WriteLine($"The sum is: {sum}");

        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        
        int largest_Number = numbers[0];
        foreach (int number in numbers)
        {
            if (number > largest_Number)
            {
                largest_Number = number;
            }
        }
        Console.WriteLine($"The largest Number is: {largest_Number}");

        int smallest_Number = numbers[0];
        foreach (int number in numbers)
        {
            if (number < smallest_Number)
            {
                smallest_Number = number;
            }
        }
        Console.WriteLine($"The smallest Number is: {smallest_Number}");

        
    }
}