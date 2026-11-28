using System;

public class MusicFile : FileItem
{
    private string _title;
    private string _artist;
    private string _album;
    private int _durationSeconds;

    public MusicFile(string filePath) : base(filePath)
    {
        _title = "";
        _artist = "";
        _album = "";
        _durationSeconds = 0;

        LoadMetadata(filePath);
    }

    public string GetAlbum() => _album;

    public bool HasAlbum() => !string.IsNullOrWhiteSpace(_album);

    private void LoadMetadata(string filePath)
    {
        try
        {
            using var tagFile = TagLib.File.Create(filePath);
            _title = tagFile.Tag.Title?.Trim() ?? "";

            if (tagFile.Tag.Performers.Length > 0)
            {
                _artist = tagFile.Tag.Performers[0].Trim();
            }
            else if (tagFile.Tag.AlbumArtists.Length > 0)
            {
                _artist = tagFile.Tag.AlbumArtists[0].Trim();
            }

            _album = tagFile.Tag.Album?.Trim() ?? "";
            _durationSeconds = (int)Math.Round(tagFile.Properties.Duration.TotalSeconds);
        }
        catch (Exception)
        {
            // Leave metadata blank; GetUniqueIdentifier() will fall back to SHA-256 hash
        }
    }
    public override string GetUniqueIdentifier()
    {
        // Only match by metadata if both title and artist exist
        if (string.IsNullOrWhiteSpace(_title) || string.IsNullOrWhiteSpace(_artist))
        {
            return base.GetUniqueIdentifier();
        }

        return $"MUSIC|{_artist.ToLower()}|{_title.ToLower()}|{_durationSeconds}";
    }

    public override string GetDisplayDetails()
    {
        if (string.IsNullOrWhiteSpace(_title) && string.IsNullOrWhiteSpace(_artist))
        {
            return $"[Music - No Tags] {base.GetDisplayDetails()}";
        }

        TimeSpan length = TimeSpan.FromSeconds(_durationSeconds);
        return $"[Music] {_artist} - {_title} ({length:mm\\:ss}) — {GetFilePath()}";
    }
}