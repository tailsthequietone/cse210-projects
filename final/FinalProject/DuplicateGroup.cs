using System.Collections.Generic;

public class DuplicateGroup
{
    private string _identifier;
    private List<FileItem> _files;

    public DuplicateGroup(string identifier, List<FileItem> files)
    {
        _identifier = identifier;
        _files = new List<FileItem>(files);
    }

    public string GetIdentifier() => _identifier;

    public List<FileItem> GetFiles() => new List<FileItem>(_files);

    public int GetFileCount() => _files.Count;

    public bool RemoveFile(FileItem file)
    {
        return _files.Remove(file);
    }
}