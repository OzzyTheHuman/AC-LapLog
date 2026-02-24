namespace LapLog.Models;

public class LapTime
{
    public string TrackName { get; set; }
    public string CarName { get; set; }
    public TimeSpan Time { get; set; }

    public LapTime(string trackName, string carName, TimeSpan time)
    {
        TrackName = trackName;
        CarName = carName;
        Time = time;
    }
}