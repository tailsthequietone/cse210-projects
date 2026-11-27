using System;
using System.Collections.Generic;

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
        Console.WriteLine("  1. Scan a folder for duplicates");
        Console.WriteLine("  2. View flagged duplicate groups");
        Console.WriteLine("  3. Review and delete duplicates");
        Console.WriteLine("  4. View deletion summary");
        Console.WriteLine("  5. Quit");
        Console.Write("\nSelect an option (1-5): ");

        string input = Console.ReadLine()?.Trim() ?? "";
        return int.TryParse(input, out int choice) ? choice : -1;
    }

    public string PromptForFolderPath()
    {
        Console.Write("\nEnter the full folder path to scan: ");
        string path = Console.ReadLine()?.Trim() ?? "";
        return path.Trim('"');
    }

    public void DisplayScanSummary(int totalFiles, int duplicateGroupCount)
    {
        Console.WriteLine($"\nScan complete! Checked {totalFiles} file(s).");
        Console.WriteLine($"Found {duplicateGroupCount} duplicate group(s).");
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

    public void RunDeletionWorkflow(List<DuplicateGroup> groups, DeletionManager deletionManager)
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
                        deletionManager.DeleteFileFromGroup(group, selectedFile, out string message);
                        Console.WriteLine(message);
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

        // Clean up any groups that are now down to 1 remaining original file
        groups.RemoveAll(g => g.GetFileCount() <= 1);
        Console.WriteLine("\nFinished reviewing duplicate groups.");
    }

    public void DisplayDeletionStats(DeletionManager deletionManager)
    {
        Console.WriteLine("\n--- Session Deletion Summary ---");
        Console.WriteLine($"Files deleted: {deletionManager.GetFilesDeletedCount()}");
        Console.WriteLine($"Space freed:   {deletionManager.GetFormattedSpaceFreed()}");
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine($"\n{message}");
    }
}