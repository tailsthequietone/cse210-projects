using System;

abstract class Shape
{
    private double _color;

    public Shape(double color)
    {
        _color = color;
    }

    public double GetColor()
    {
        return _color;
    }

    public void SetColor(double color)
    {
        _color = color;
    }

    public abstract double GetArea();
}