using System.Runtime.CompilerServices;

class Scripture
{
    List<Word> _words;
    // Add scripture reference

    public Scripture(string bookName, int chapter, int verse, string text)
    {
        _words = ConvertToWords(text);
    }
    public Scripture(string bookName, int chapter, int startVerse, int endVerse, string text)
    {
        
    }  
    public void ShowScripture()
    {
        foreach(Word word in _words)
        {
            word.Display();
        }
    }
    //ConvertToWords will convert the paragraph that is in the text parameter to a list of words. 
    // This list of words is returned
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