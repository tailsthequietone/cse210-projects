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
        UserInterface ui = new UserInterface();

        List<DuplicateGroup> currentDuplicates = new List<DuplicateGroup>();
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
                        List<FileItem> scannedFiles = scanner.ScanDirectory(folderPath);
                        currentDuplicates = detector.FindDuplicates(scannedFiles);
                        ui.DisplayScanSummary(scannedFiles.Count, currentDuplicates.Count);
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
                    ui.RunDeletionWorkflow(currentDuplicates, deletionManager);
                    break;

                case 4:
                    ui.DisplayDeletionStats(deletionManager);
                    break;

                case 5:
                    ui.DisplayDeletionStats(deletionManager);
                    ui.DisplayMessage("Goodbye!");
                    running = false;
                    break;

                default:
                    ui.DisplayMessage("Invalid option. Please enter a number between 1 and 5.");
                    break;
            }
        }
    }
}