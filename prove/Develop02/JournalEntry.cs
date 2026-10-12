using System;

class JournalEntry
{
    public string _date;
    public string _prompt;
    public string _response;
    public string _mood; // Extra feature: Mood tracking

    public JournalEntry()
    {
    }

    public JournalEntry(string date, string prompt, string response, string mood)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
        _mood = mood;
    }

    public void DisplayEntry()
    {
        Console.WriteLine($"Date: {_date} - Mood: {_mood}/5");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");
        Console.WriteLine("----------------------------------------");
    }

    public override string ToString()
    {
        // Save using '#' as separator
        return $"{_date}#{_prompt}#{_response}#{_mood}";
    }
}