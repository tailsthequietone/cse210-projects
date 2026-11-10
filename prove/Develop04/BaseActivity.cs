using System;
//don't forget is base class
public class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public string Name => _name;
    public int Duration => _duration;

    public Activity()
    {
        _name = "Mindfulness Activity";
        _description = string.Empty;
        _duration = 0;
    }

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"\nWelcome to the {_name}.\n");
        Console.WriteLine(_description);
        Console.Write("\nHow long, in seconds, would you like for your session? ");
        _duration = ReadPositiveInteger();
        Console.WriteLine("\nGet ready...");
        ShowSpinner(3);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(2);
        Console.WriteLine($"\nYou have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(3);
    }

    public void ShowSpinner(int seconds)
    {
        Console.CursorVisible = false;
        string[] symbols = { "/", "-", "\\", "|" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int index = 0;
        while (DateTime.Now < endTime)
        {
            Console.Write(symbols[index % symbols.Length]);
            Thread.Sleep(250);
            Console.Write("\b \b");
            index++;
        }
        Console.CursorVisible = true;
    }

    public void ShowCountDown(int seconds)
    {
        Console.CursorVisible = false;
        for (int i = seconds; i >= 1; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
        Console.CursorVisible = true;
    }

    // Compatibility names for the methods included in the starter file.
    public void RunCountDownMessage(string message, int seconds)
    {
        Console.CursorVisible = false;
        for (int i = seconds; i >= 1; i--)
        {
            Console.Clear();
            Console.Write($"{message} - {i}");
            Thread.Sleep(1000);
        }
        Console.CursorVisible = true;
    }

    public void DisplaySpinnerMessage(string message, int seconds)
    {
        Console.Write($"{message} ");
        ShowSpinner(seconds);
    }

    protected static int ReadPositiveInteger()
    {
        int value;

        while (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            Console.Write("Please enter a positive whole number: ");
        }
        return value;
    }
}

// Keeps the supplied starter type usable while the required design uses Activity.
public class BaseActivity : Activity
{
    public BaseActivity() : base() { }
}



