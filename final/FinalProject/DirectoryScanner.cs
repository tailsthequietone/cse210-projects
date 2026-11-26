using System;
using System.Collections.Generic;
using System.IO;

public class DirectoryScanner
{
    private readonly HashSet<string> _audioExtensions;

    public DirectoryScanner()
{
    _audioExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".mp3", ".flac", ".wav", ".m4a", ".ogg", ".opus", ".wma", ".aac"
    };
}
    public List<FileItem> ScanDirectory(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            throw new DirectoryNotFoundException($"Folder not found: {folderPath}");
        }

        List<FileItem> foundFiles = new List<FileItem>();
        ScanRecursive(folderPath, foundFiles);
        return foundFiles;
    }

    private void ScanRecursive(string currentPath, List<FileItem> foundFiles)
    {
        try
        {
            foreach (string filePath in Directory.GetFiles(currentPath))
            {
                foundFiles.Add(CreateFileItem(filePath));
            }

            foreach (string subDir in Directory.GetDirectories(currentPath))
            {
                ScanRecursive(subDir, foundFiles);
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Skip folders the OS restricts access to
        }
        catch (IOException)
        {
            // Skip locked or inaccessible paths
        }
    }

    private FileItem CreateFileItem(string filePath)
    {
        string extension = Path.GetExtension(filePath);

        if (_audioExtensions.Contains(extension))
        {
            return new MusicFile(filePath);
        }

        return new FileItem(filePath);
    }
}