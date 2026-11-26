using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class EmptyFolderCleaner
{
    private string _rootScanPath;
    private int _foldersDeletedCount;

    public EmptyFolderCleaner()
    {
        _rootScanPath = "";
        _foldersDeletedCount = 0;
    }

    public void SetRootScanPath(string folderPath)
    {
        _rootScanPath = NormalizePath(folderPath);
    }

    public bool HasRootPath() => !string.IsNullOrEmpty(_rootScanPath);

    public int GetFoldersDeletedCount() => _foldersDeletedCount;

    public bool IsEmptySubfolder(string folderPath)
    {
        if (string.IsNullOrWhiteSpace(folderPath) || !HasRootPath())
        {
            return false;
        }

        string normalizedFolder = NormalizePath(folderPath);

        // Safety check: must strictly be a sub-folder INSIDE the root scan path (never the root itself)
        string rootPrefix = _rootScanPath + Path.DirectorySeparatorChar;
        if (!normalizedFolder.StartsWith(rootPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (!Directory.Exists(normalizedFolder))
        {
            return false;
        }

        try
        {
            // Empty only if it contains no files AND no sub-folders
            return !Directory.EnumerateFileSystemEntries(normalizedFolder).Any();
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<string> FindEmptySubfolders()
    {
        List<string> emptyFolders = new List<string>();

        if (!HasRootPath() || !Directory.Exists(_rootScanPath))
        {
            return emptyFolders;
        }

        FindEmptyRecursive(_rootScanPath, emptyFolders);
        return emptyFolders;
    }

    private void FindEmptyRecursive(string currentPath, List<string> emptyFolders)
    {
        try
        {
            foreach (string subDir in Directory.GetDirectories(currentPath))
            {
                if (IsEmptySubfolder(subDir))
                {
                    emptyFolders.Add(subDir);
                }
                else
                {
                    FindEmptyRecursive(subDir, emptyFolders);
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Skip restricted folders
        }
        catch (IOException)
        {
            // Skip inaccessible folders
        }
    }

    public bool DeleteEmptyFolder(string folderPath, out string statusMessage)
    {
        if (!IsEmptySubfolder(folderPath))
        {
            statusMessage = "Safety block: Folder is either not empty or is outside the scanned sub-folders.";
            return false;
        }

        try
        {
            // Passing 'false' guarantees Windows/OS refuses to delete if anything is inside
            Directory.Delete(folderPath, false);
            _foldersDeletedCount++;
            statusMessage = $"Deleted empty sub-folder: {folderPath}";
            return true;
        }
        catch (Exception ex)
        {
            statusMessage = $"Failed to delete folder: {ex.Message}";
            return false;
        }
    }

    private string NormalizePath(string path)
    {
        return Path.GetFullPath(path)
            .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
    }
}