using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment assignment = new Assignment("Samuel Bennett", "Multiplication");
        string summary = assignment.GetSummary();
        Console.WriteLine(summary);

        MathAssignment mathassignment = new MathAssignment("Section 7.3", "Problems 8-19", "Roberto Rodriguez", "Fractions");
        string summary2 = assignment.GetSummary();
        string homeworklist = mathassignment.GetHomeworkList();
        Console.WriteLine(summary2);
        Console.WriteLine(homeworklist);
        
        WritingAssignment writingassignment = new WritingAssignment("The Causes of World War II", "Mary Waters", "European History");
        Console.WriteLine(writingassignment.GetSummary());
        Console.WriteLine(writingassignment.GetWritingInformation());


    }
}