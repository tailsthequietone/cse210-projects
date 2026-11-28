using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        DirectoryScanner scanner = new DirectoryScanner();
        DuplicateDetector detector = new DuplicateDetector();
        DeletionManager deletionManager = new DeletionManager();
        EmptyFolderCleaner folderCleaner = new EmptyFolderCleaner();
        AlbumOrganizer albumOrganizer = new AlbumOrganizer();
        UserInterface ui = new UserInterface();

        List<FileItem> scannedFiles = new List<FileItem>();
        List<DuplicateGroup> currentDuplicates = new List<DuplicateGroup>();
        string currentScanRoot = "";
        bool running = true;

        ui.DisplayWelcome();

        while (running)
        {
            int choice = ui.DisplayMainMenu();

            switch (choice)
            {
                case 1:
                    string folderPath = ui.PromptForFolderPath();
                    try
                    {
                        scannedFiles = scanner.ScanDirectory(folderPath);
                        currentDuplicates = detector.FindDuplicates(scannedFiles);
                        currentScanRoot = folderPath;
                        folderCleaner.SetRootScanPath(folderPath);
                        int emptyFolderCount = folderCleaner.FindEmptySubfolders().Count;

                        ui.DisplayScanSummary(scannedFiles.Count, currentDuplicates.Count, emptyFolderCount);
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        ui.DisplayMessage(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        ui.DisplayMessage($"An error occurred while scanning: {ex.Message}");
                    }
                    break;

                case 2:
                    ui.DisplayDuplicateGroups(currentDuplicates);
                    break;

                case 3:
                    ui.RunDeletionWorkflow(currentDuplicates, deletionManager, folderCleaner);
                    break;

                case 4:
                    ui.RunAlbumOrganization(scannedFiles, currentScanRoot, albumOrganizer);
                    break;

                case 5:
                    ui.RunEmptyFolderCleanup(folderCleaner);
                    break;

                case 6:
                    ui.DisplaySessionStats(deletionManager, folderCleaner, albumOrganizer);
                    break;

                case 7:
                    ui.DisplaySessionStats(deletionManager, folderCleaner, albumOrganizer);
                    ui.DisplayMessage("Goodbye!");
                    running = false;
                    break;

                default:
                    ui.DisplayMessage("Invalid option. Please enter a number between 1 and 7.");
                    break;
            }
        }
    }
}