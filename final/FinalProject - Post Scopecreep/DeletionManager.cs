using System;

public class DeletionManager
{
    private int _filesDeletedCount;
    private long _bytesFreed;

    public DeletionManager()
    {
        _filesDeletedCount = 0;
        _bytesFreed = 0;
    }

    public int GetFilesDeletedCount() => _filesDeletedCount;

    public long GetBytesFreed() => _bytesFreed;

    public string GetFormattedSpaceFreed()
    {
        if (_bytesFreed >= 1024 * 1024)
        {
            return $"{Math.Round(_bytesFreed / (1024.0 * 1024.0), 2)} MB";
        }
        return $"{Math.Round(_bytesFreed / 1024.0, 2)} KB";
    }

    public bool DeleteFileFromGroup(DuplicateGroup group, FileItem file, out string statusMessage)
    {
        if (group.GetFileCount() <= 1)
        {
            statusMessage = "Safety block: Cannot delete the last remaining copy in this group.";
            return false;
        }

        try
        {
            long fileSize = file.GetFileSize();
            file.Delete();
            group.RemoveFile(file);

            _filesDeletedCount++;
            _bytesFreed += fileSize;

            statusMessage = $"Deleted: {file.GetFileName()}";
            return true;
        }
        catch (Exception ex)
        {
            statusMessage = $"Failed to delete {file.GetFileName()}: {ex.Message}";
            return false;
        }
    }
}