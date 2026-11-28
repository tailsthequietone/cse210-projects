using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class AlbumOrganizer
{
    private static readonly HashSet<string> ReservedWindowsNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "CON", "PRN", "AUX", "NUL",
        "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
        "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
    };

    private static readonly HashSet<char> InvalidChars = new HashSet<char>(Path.GetInvalidFileNameChars());

    private int _songsMovedCount;
    private int _foldersCreatedCount;

    public AlbumOrganizer()
    {
        _songsMovedCount = 0;
        _foldersCreatedCount = 0;
    }

    public int GetSongsMovedCount() => _songsMovedCount;

    public int GetFoldersCreatedCount() => _foldersCreatedCount;

    public string SanitizeFolderName(string albumName)
    {
        if (string.IsNullOrWhiteSpace(albumName))
        {
            return "Unnamed Album";
        }

        StringBuilder builder = new StringBuilder();

        foreach (char c in albumName.Trim())
        {
            if (c == ':')
            {
                builder.Append(" - ");
            }
            else if (InvalidChars.Contains(c))
            {
                builder.Append('_');
            }
            else
            {
                builder.Append(c);
            }
        }

        // Collapse multiple consecutive spaces into a single space
        string collapsed = CollapseSpaces(builder.ToString());

        // Windows does not allow folder names ending with spaces or periods
        string sanitized = collapsed.Trim().TrimEnd('.').Trim();

        if (string.IsNullOrWhiteSpace(sanitized))
        {
            return "Unnamed Album";
        }

        // Windows blocks reserved names even when followed by a dot (e.g., "AUX. Live")
        string baseSegment = sanitized.Split('.')[0].Trim();
        if (ReservedWindowsNames.Contains(baseSegment))
        {
            sanitized = $"Album - {sanitized}";
        }

        // Cap length to help prevent Windows MAX_PATH issues
        if (sanitized.Length > 100)
        {
            sanitized = sanitized.Substring(0, 100).TrimEnd(' ', '.');
        }

        return string.IsNullOrWhiteSpace(sanitized) ? "Unnamed Album" : sanitized;
    }

    public Dictionary<string, List<MusicFile>> BuildAlbumPlan(
        List<FileItem> scannedFiles,
        string rootFolder,
        out int untaggedCount,
        out int alreadySortedCount)
    {
        Dictionary<string, List<MusicFile>> plan = new Dictionary<string, List<MusicFile>>(StringComparer.OrdinalIgnoreCase);
        untaggedCount = 0;
        alreadySortedCount = 0;

        string normalizedRoot = Path.GetFullPath(rootFolder);

        foreach (FileItem item in scannedFiles)
        {
            if (!(item is MusicFile musicFile))
            {
                continue;
            }

            // Skip files that were already deleted earlier in the session
            if (!File.Exists(musicFile.GetFilePath()))
            {
                continue;
            }

            if (!musicFile.HasAlbum())
            {
                untaggedCount++;
                continue;
            }

            string folderName = SanitizeFolderName(musicFile.GetAlbum());
            string targetDir = Path.GetFullPath(Path.Combine(normalizedRoot, folderName));
            string currentDir = Path.GetFullPath(Path.GetDirectoryName(musicFile.GetFilePath()));

            if (string.Equals(currentDir, targetDir, StringComparison.OrdinalIgnoreCase))
            {
                alreadySortedCount++;
                continue;
            }

            if (!plan.ContainsKey(folderName))
            {
                plan[folderName] = new List<MusicFile>();
            }

            plan[folderName].Add(musicFile);
        }

        return plan;
    }

    public List<string> ExecuteAlbumPlan(Dictionary<string, List<MusicFile>> plan, string rootFolder)
    {
        List<string> results = new List<string>();
        string normalizedRoot = Path.GetFullPath(rootFolder);

        foreach (KeyValuePair<string, List<MusicFile>> entry in plan)
        {
            string folderName = entry.Key;
            string targetDir = Path.Combine(normalizedRoot, folderName);

            try
            {
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                    _foldersCreatedCount++;
                    results.Add($"Created folder: {folderName}");
                }

                foreach (MusicFile song in entry.Value)
                {
                    if (!File.Exists(song.GetFilePath()))
                    {
                        continue;
                    }

                    string destinationPath = Path.Combine(targetDir, song.GetFileName());

                    // Safety check: avoid overwriting an existing file with the same name
                    if (File.Exists(destinationPath))
                    {
                        results.Add($"Skipped \"{song.GetFileName()}\" (a file with that name already exists in \"{folderName}\").");
                        continue;
                    }

                    song.MoveTo(destinationPath);
                    _songsMovedCount++;
                    results.Add($"Moved \"{song.GetFileName()}\" -> {folderName}\\");
                }
            }
            catch (Exception ex)
            {
                results.Add($"Error organizing album \"{folderName}\": {ex.Message}");
            }
        }

        return results;
    }

    private string CollapseSpaces(string input)
    {
        StringBuilder sb = new StringBuilder();
        bool previousWasSpace = false;

        foreach (char c in input)
        {
            if (char.IsWhiteSpace(c))
            {
                if (!previousWasSpace)
                {
                    sb.Append(' ');
                    previousWasSpace = true;
                }
            }
            else
            {
                sb.Append(c);
                previousWasSpace = false;
            }
        }

        return sb.ToString();
    }
}