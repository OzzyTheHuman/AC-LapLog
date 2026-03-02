namespace LapLog.Models;

public class Track
{
    // TODO: Folder path is not utilised
    public string Name { get; }
    public string FolderPath { get; }

    public Track(string name)
    {
        Name = name;
    }
}