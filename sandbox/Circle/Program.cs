class Program
{
    static void Main()
    {
        Console.WriteLine("Hello Circle");

        Circle myCircle = new Circle(100);

        double area =myCircle.GetArea();

        Console.WriteLine(area);

    }

}