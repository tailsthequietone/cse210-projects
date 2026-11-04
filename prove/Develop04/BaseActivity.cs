using System;
class BaseActivity
{
    public BaseActivity()
    {
        
    }

    public void RunCountDownMessage(string message, int seconds)
    {
        Console.CursorVisible = false;
        for (int i=seconds; i>= 1; i--)
        {
        Console.Clear();
        Console.Write($"{message} - {i}");
        Thread.Sleep(1000);
        }
        Console.CursorVisible = true;
    }

    public void DisplaySpinnerMessage(string message, int seconds)
    {
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        Console.CursorVisible = false;
        Console.Clear();
        Console.Write($"{message} ");
        while (DateTime.Now < endTime)
        {
            Console.Write("x");
            Thread.Sleep(400);
            Console.Write("\b");
            Console.Write("+");
            Thread.Sleep(400);
            Console.Write("\b");
        }
        Console.CursorVisible = true;
    }
}