namespace LapLog.Models;

public class Car
{
    // TODO: Folder path is not utilised
    public string Name { get; }
    public string FolderPath { get; }

    public Car(string name)
    {
        Name = name;
    }
}