using System.Collections.Generic;

public class DuplicateDetector
{
    public List<DuplicateGroup> FindDuplicates(List<FileItem> files)
    {
        Dictionary<string, List<FileItem>> groupedFiles = new Dictionary<string, List<FileItem>>();

        foreach (FileItem file in files)
        {
            string key = file.GetUniqueIdentifier();

            if (!groupedFiles.ContainsKey(key))
            {
                groupedFiles[key] = new List<FileItem>();
            }

            groupedFiles[key].Add(file);
        }

        List<DuplicateGroup> duplicates = new List<DuplicateGroup>();

        foreach (KeyValuePair<string, List<FileItem>> entry in groupedFiles)
        {
            if (entry.Value.Count > 1)
            {
                duplicates.Add(new DuplicateGroup(entry.Key, entry.Value));
            }
        }

        return duplicates;
    }
}