
abstract class BaseGoal
{
    private string _name;
    private string _description;
    private int _points;
    private bool _status;
    private string _goalType;

    public BaseGoal(string name , string description, int points, bool status, string goalType)
    {
        _name = name;
        _description = description;
        _points = points;
        _status = status;
        _goalType = goalType;
    }
    public BaseGoal()
    {
        SetGoalName();
        SetGoalDescription();
        SetGoalPoints();
        _status = false;
        _goalType = base.ToString();

    }

    private void SetGoalName()
    {
        Console.Write("Please enter the name of your goal: ");
        _name = Console.ReadLine();
    }

        private void SetGoalDescription()
    {
        Console.Write("Please enter the description of your goal: ");
        _description = Console.ReadLine();
    }
            private void SetGoalPoints()
    {
        Console.Write("Please enter the number of points for your goal: ");
        _points = int.Parse(Console.ReadLine());
    }

    public virtual string GetConsoleString()
    {   string completeString = " ";
        if(_status)
        completeString = "X";

        return $"[] - ({_goalType}) Goal name: {_name}. Goal description is: {_description}. Goal points is: {_points}";
    }

    public int MarkComplete()
    {
        _status = true;
        return _points;
    }

    public abstract int RecordEvent();
}