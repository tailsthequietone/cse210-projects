using System;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

class Program
{
    static void Main(string[] args)
    {
        int X = 10;
        int Y = 20;
        int Z = 30;

        if (X == 10 || Y == 21 && Z == 30)
        {
            Console.WriteLine("X is 10");
            Console.WriteLine("Y is fun");
        }
        else if (X == 20)
        {
            Console.WriteLine("Were are in hte else if.");
        }
        else
        {
            Console.WriteLine("z is not much fun");
        }
    }
}