using System;

class Program
{
    static void Main(string[] args)
    {
        BaseActivity activity = new BaseActivity();
        activity.RunCountDownMessage("Get Ready", 5);

        DateTime endTime = DateTime.Now.AddSeconds(15);

        while (DateTime.Now < endTime)
        {
            activity.RunCountDownMessage("breath in...", 4);
            activity.RunCountDownMessage("breath out...", 6);
        }

        activity.DisplaySpinnerMessage("Well Done", 5);
    }
}