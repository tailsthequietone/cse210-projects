using System;
using System.IO;
using System.Security.Cryptography;

public class FileItem
{
    private string _filePath;
    private string _fileName;
    private long _fileSize;
    private string _fileHash;

    public FileItem(string filePath)
    {
        FileInfo info = new FileInfo(filePath);
        _filePath = info.FullName;
        _fileName = info.Name;
        _fileSize = info.Length;
        _fileHash = ComputeFileHash(_filePath);
    }

    public string GetFilePath() => _filePath;
    public string GetFileName() => _fileName;
    public long GetFileSize() => _fileSize;

    private string ComputeFileHash(string filePath)
    {
        try
        {
            using FileStream stream = File.OpenRead(filePath);
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(stream);
            return Convert.ToHexString(hashBytes);
        }
        catch (Exception)
        {
            // Fallback so unreadable files never falsely match each other
            return $"UNREADABLE|{_filePath}";
        }
    }

    // Default duplicate key: exact byte size + SHA-256 content hash
    public virtual string GetUniqueIdentifier()
    {
        return $"HASH|{_fileSize}|{_fileHash}";
    }

    public virtual string GetDisplayDetails()
    {
        double sizeInKb = Math.Round(_fileSize / 1024.0, 2);
        return $"{_fileName} ({sizeInKb} KB) — {_filePath}";
    }

    public void Delete()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }
    }
}