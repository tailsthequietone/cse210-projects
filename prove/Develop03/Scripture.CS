using System;
using System.Collections.Generic;

class Scripture
{
    private List<Word> _words;
    private ScriptureReference _reference;
    private string _text;

    public Scripture(string name, int chapter, int verse, string text)
    {
        _reference = new ScriptureReference(name, chapter, verse);
        _text = text;
        _words = ConvertToWords(text);
    }

    public Scripture(string name, int chapter, int startVerse, int endVerse, string text)
    {
        _reference = new ScriptureReference(name, chapter, startVerse, endVerse);
        _text = text;
        _words = ConvertToWords(text);
    }  

    public Scripture(ScriptureReference reference, string text)
    {
        _reference = reference;
        _text = text;
        _words = ConvertToWords(text);
    }

    public bool HideSomeWords()
    {
        Random random = new Random();
        List<Word> visibleWords = new List<Word>();

        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                visibleWords.Add(word);
            }
        }

        if (visibleWords.Count == 0)
        {
            return false;
        }

        int countToHide = Math.Min(3, visibleWords.Count);
        for (int i = 0; i < countToHide; i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }

        return true;
    }

    public void ShowScripture()
    {
        _reference.DisplayScriptureReference();
        Console.WriteLine();
        foreach(Word word in _words)
        {
            word.DisplayWord();
        }
        Console.WriteLine();
    }

    public string GetScriptureReference()
    {
        return _reference.GetScriptureReference();
    }

    private int NumberOfHiddenWords()
    {
        int count = 0;
        foreach (Word word in _words)
        {
            if (word.IsHidden())
            {
                count++;
            }
        }
        return count;
    }

    public void Reset()
    {
        _words = ConvertToWords(_text);
    }

    private List<Word> ConvertToWords(string text)
    {
        List<Word> tempWords = new List<Word>();
        foreach(string word in text.Split(' '))
        {
            Word tempWord = new Word(word);
            tempWords.Add(tempWord);
        }
        return tempWords;
    }
}