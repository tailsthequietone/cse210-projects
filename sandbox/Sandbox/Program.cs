using System;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

class Program
{

    static double addnumbers(double x, int y)
    {
        return x + y;
    }
    static void displaygreeting(string name)
    {
        Console.WriteLine($"Welcome {name}, pleased to meet you.");
    }
    static void Main(string[] args)
    {
        displaygreeting("Bob");
        double answer = (addnumbers(12.234, 10));
        Console.WriteLine(answer);
    //  bool done = false;

    //  do
    //     {
    //         Console.Write("Are we done (y/n): ");
    //         done = Console.ReadLine().ToLower() == "y";
    //     }while (! done);

    // for(int i = 0; i < 10; i++)
    //     {
    //         Console.WriteLine($"i -");
    //         Console.WriteLine("Hey Bob");
    //     }

        // for(double i = 1000; i >= 100.0; i-=5.98234)
        // {
        //     Console.WriteLine($"{i} -");
        //     Console.WriteLine("Hey Bob");
        // }

        // List<string> myFriends = new List<string> {"Bob", "Betty", "bubba"};

        // List<string> myFriends = ["Bob", "Betty", "bubba"];
        // List<string> names = new List<string>();
        // myFriends.Add("James");
        // myFriends.Add("Doug");

        // foreach(string name in myFriends)
        // {
        //     Console.WriteLine(name);
        // }
    }
}