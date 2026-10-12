class Circle
{
    private double _radius;

    public Circle()
    {
        _radius = 0.0;
    }

    public Circle(double radius)
    {
        _radius = radius;
    }


    public double GetArea()
    {
        return _radius * _radius * 3.14159;
    }

        public double GetCircumference()
    {
        return 2 * 3.14159 * _radius;
    }

    private void SetRadius(double radius)
    {
        if (radius < 0.0)
        {
            _radius = 1.0;
            Console.WriteLine("Error, must have radius >= 0");
        }
        else
        {
            _radius = radius;
        }
    }
    public double GetRadius()
    {
        return _radius;
    }
}