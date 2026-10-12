using System;
using System.IO;

class Journal
{
    public List<JournalEntry> _entries = new List<JournalEntry>();

    public void AddEntry(JournalEntry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nNo entries in the journal.\n");
            return;
        }

        Console.WriteLine("\n=== Journal Entries ===");
        foreach (JournalEntry entry in _entries)
        {
            entry.DisplayEntry();
        }
    }

    public void WriteToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {          
            foreach (JournalEntry entry in _entries)
            {
                outputFile.WriteLine(entry.ToString());
            }
        }
        Console.WriteLine($"\nJournal successfully saved to {filename}.\n");
    }

    public void ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"\nFile '{filename}' not found.\n");
            return;
        }

        _entries.Clear();

        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split("#");

            if (parts.Length >= 3)
            {
                string date = parts[0];
                string prompt = parts[1];
                string response = parts[2];
                string mood = parts.Length > 3 ? parts[3] : "N/A";

                JournalEntry entry = new JournalEntry(date, prompt, response, mood);  
                _entries.Add(entry);
            }
        }
        Console.WriteLine($"\nJournal successfully loaded from {filename}.\n");
    }
}