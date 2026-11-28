using System;
using System.Collections.Generic;
using System.IO;

public class UserInterface
{
    public void DisplayWelcome()
    {
        Console.WriteLine("=========================================");
        Console.WriteLine("     Duplicate File & Music Finder       ");
        Console.WriteLine("=========================================");
    }

    public int DisplayMainMenu()
    {
        Console.WriteLine("\nMain Menu:");
        Console.WriteLine("  1. Scan a folder for duplicates & music");
        Console.WriteLine("  2. View flagged duplicate groups");
        Console.WriteLine("  3. Review and delete duplicates");
        Console.WriteLine("  4. Sort music files into album folders");
        Console.WriteLine("  5. Suggest and delete empty sub-folders");
        Console.WriteLine("  6. View session summary");
        Console.WriteLine("  7. Quit");
        Console.Write("\nSelect an option (1-7): ");

        string input = Console.ReadLine()?.Trim() ?? "";
        return int.TryParse(input, out int choice) ? choice : -1;
    }

    public string PromptForFolderPath()
    {
        Console.Write("\nEnter the full folder path to scan: ");
        string path = Console.ReadLine()?.Trim() ?? "";
        return path.Trim('"');
    }

    public void DisplayScanSummary(int totalFiles, int duplicateGroupCount, int emptySubfolderCount)
    {
        Console.WriteLine($"\nScan complete! Checked {totalFiles} file(s).");
        Console.WriteLine($"Found {duplicateGroupCount} duplicate group(s).");
        if (emptySubfolderCount > 0)
        {
            Console.WriteLine($"Also detected {emptySubfolderCount} empty sub-folder(s) (see Option 5 to clean up).");
        }
    }

    public void DisplayDuplicateGroups(List<DuplicateGroup> groups)
    {
        if (groups.Count == 0)
        {
            Console.WriteLine("\nNo duplicate groups to display. Try scanning a folder first.");
            return;
        }

        Console.WriteLine();
        for (int i = 0; i < groups.Count; i++)
        {
            List<FileItem> files = groups[i].GetFiles();
            if (files.Count <= 1) continue;

            Console.WriteLine($"--- Group {i + 1} ({files.Count} files) ---");
            for (int j = 0; j < files.Count; j++)
            {
                Console.WriteLine($"  [{j + 1}] {files[j].GetDisplayDetails()}");
            }
            Console.WriteLine();
        }
    }

    public void RunDeletionWorkflow(
        List<DuplicateGroup> groups,
        DeletionManager deletionManager,
        EmptyFolderCleaner folderCleaner)
    {
        if (groups.Count == 0)
        {
            Console.WriteLine("\nNo duplicate groups available. Scan a folder first.");
            return;
        }

        for (int i = 0; i < groups.Count; i++)
        {
            DuplicateGroup group = groups[i];

            while (group.GetFileCount() > 1)
            {
                List<FileItem> files = group.GetFiles();
                Console.WriteLine($"\n--- Reviewing Group {i + 1} of {groups.Count} ---");
                for (int j = 0; j < files.Count; j++)
                {
                    Console.WriteLine($"  [{j + 1}] {files[j].GetDisplayDetails()}");
                }

                Console.Write("\nEnter the number of the file to DELETE, 's' to skip group, or 'q' to return to menu: ");
                string input = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (input == "q") return;
                if (input == "s") break;

                if (int.TryParse(input, out int index) && index >= 1 && index <= files.Count)
                {
                    FileItem selectedFile = files[index - 1];
                    Console.Write($"Are you sure you want to permanently delete \"{selectedFile.GetFileName()}\"? (y/n): ");
                    string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";

                    if (confirm == "y")
                    {
                        string parentFolder = Path.GetDirectoryName(selectedFile.GetFilePath());
                        bool deleted = deletionManager.DeleteFileFromGroup(group, selectedFile, out string message);
                        Console.WriteLine(message);

                        if (deleted && !string.IsNullOrEmpty(parentFolder))
                        {
                            PromptIfParentFolderEmpty(parentFolder, folderCleaner);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Deletion canceled.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            }
        }

        groups.RemoveAll(g => g.GetFileCount() <= 1);
        Console.WriteLine("\nFinished reviewing duplicate groups.");
    }

    public void RunAlbumOrganization(List<FileItem> scannedFiles, string rootFolder, AlbumOrganizer albumOrganizer)
    {
        if (string.IsNullOrEmpty(rootFolder) || scannedFiles.Count == 0)
        {
            Console.WriteLine("\nPlease scan a folder first (Option 1).");
            return;
        }

        Dictionary<string, List<MusicFile>> plan = albumOrganizer.BuildAlbumPlan(
            scannedFiles,
            rootFolder,
            out int untaggedCount,
            out int alreadySortedCount);

        if (plan.Count == 0)
        {
            Console.WriteLine("\nNo music files need to be moved into album folders.");
            if (alreadySortedCount > 0)
            {
                Console.WriteLine($"  - {alreadySortedCount} song(s) are already in their album folder.");
            }
            if (untaggedCount > 0)
            {
                Console.WriteLine($"  - {untaggedCount} music file(s) were skipped because they have no album metadata.");
            }
            return;
        }

        Console.WriteLine("\n--- Proposed Album Folder Organization ---");
        int totalToMove = 0;

        foreach (KeyValuePair<string, List<MusicFile>> entry in plan)
        {
            string folderName = entry.Key;
            List<MusicFile> songs = entry.Value;
            totalToMove += songs.Count;

            string originalAlbum = songs[0].GetAlbum();
            string renameNote = string.Equals(originalAlbum, folderName, StringComparison.Ordinal)
                ? ""
                : $" (sanitized from \"{originalAlbum}\")";

            Console.WriteLine($"\nFolder: {folderName}\\{renameNote} — {songs.Count} song(s)");
            foreach (MusicFile song in songs)
            {
                Console.WriteLine($"  - {song.GetFileName()}");
            }
        }

        if (alreadySortedCount > 0)
        {
            Console.WriteLine($"\nNote: {alreadySortedCount} song(s) are already in their matching album folder.");
        }
        if (untaggedCount > 0)
        {
            Console.WriteLine($"Note: {untaggedCount} music file(s) have no album tag and will stay where they are.");
        }

        Console.Write($"\nMove {totalToMove} song(s) into {plan.Count} album folder(s) inside \"{rootFolder}\"? (y/n): ");
        string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";

        if (confirm == "y")
        {
            List<string> results = albumOrganizer.ExecuteAlbumPlan(plan, rootFolder);
            Console.WriteLine();
            foreach (string line in results)
            {
                Console.WriteLine($"  {line}");
            }
            Console.WriteLine("\nAlbum organization complete! Tip: Run Option 5 if you want to remove any newly emptied sub-folders.");
        }
        else
        {
            Console.WriteLine("Album organization canceled.");
        }
    }

    private void PromptIfParentFolderEmpty(string folderPath, EmptyFolderCleaner folderCleaner)
    {
        string currentFolder = folderPath;

        while (!string.IsNullOrEmpty(currentFolder) && folderCleaner.IsEmptySubfolder(currentFolder))
        {
            Console.WriteLine($"\nThe sub-folder \"{currentFolder}\" is now empty.");
            Console.Write("Would you like to delete this empty sub-folder? (y/n): ");
            string choice = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (choice == "y")
            {
                bool removed = folderCleaner.DeleteEmptyFolder(currentFolder, out string msg);
                Console.WriteLine(msg);
                if (!removed) break;

                currentFolder = Path.GetDirectoryName(currentFolder);
            }
            else
            {
                break;
            }
        }
    }

    public void RunEmptyFolderCleanup(EmptyFolderCleaner folderCleaner)
    {
        if (!folderCleaner.HasRootPath())
        {
            Console.WriteLine("\nPlease scan a folder first (Option 1).");
            return;
        }

        List<string> emptyFolders = folderCleaner.FindEmptySubfolders();

        if (emptyFolders.Count == 0)
        {
            Console.WriteLine("\nNo empty sub-folders found.");
            return;
        }

        Console.WriteLine($"\nFound {emptyFolders.Count} empty sub-folder(s):");
        for (int i = 0; i < emptyFolders.Count; i++)
        {
            Console.WriteLine($"  [{i + 1}] {emptyFolders[i]}");
        }

        foreach (string folder in emptyFolders)
        {
            if (!folderCleaner.IsEmptySubfolder(folder)) continue;

            Console.Write($"\nDelete empty sub-folder \"{folder}\"? (y/n/q to stop): ");
            string input = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (input == "q") break;

            if (input == "y")
            {
                bool deleted = folderCleaner.DeleteEmptyFolder(folder, out string msg);
                Console.WriteLine(msg);

                if (deleted)
                {
                    string parent = Path.GetDirectoryName(folder);
                    if (!string.IsNullOrEmpty(parent))
                    {
                        PromptIfParentFolderEmpty(parent, folderCleaner);
                    }
                }
            }
        }
    }

    public void DisplaySessionStats(
        DeletionManager deletionManager,
        EmptyFolderCleaner folderCleaner,
        AlbumOrganizer albumOrganizer)
    {
        Console.WriteLine("\n--- Session Summary ---");
        Console.WriteLine($"Files deleted:         {deletionManager.GetFilesDeletedCount()}");
        Console.WriteLine($"Space freed:           {deletionManager.GetFormattedSpaceFreed()}");
        Console.WriteLine($"Empty folders deleted: {folderCleaner.GetFoldersDeletedCount()}");
        Console.WriteLine($"Album folders created: {albumOrganizer.GetFoldersCreatedCount()}");
        Console.WriteLine($"Songs sorted by album: {albumOrganizer.GetSongsMovedCount()}");
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine($"\n{message}");
    }
}