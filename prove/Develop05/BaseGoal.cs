using System;

class BaseGoal
{
    private string _name;
    private string _description;

    private int _points;
    public BaseGoal()
    {
        GetGoalName();
        GetGoalDescription();
        GetGoalPoints();

    }

    private void GetGoalName()
    {
        Console.Write("Please enter the name of your goal: ");
        _name = Console.ReadLine();
    }

        private void GetGoalDescription()
    {
        Console.Write("Please enter the description of your goal: ");
        _description = Console.ReadLine();
    }
            private void GetGoalPoints()
    {
        Console.Write("Please enter the number of points for your goal: ");
        _points = int.Parse(Console.ReadLine());
    }

    public string GetConsoleString()
    {
        return $"Goal name: {_name}. Goal description is: {_description}. Goal points is: {_points}";
    }
}