using System.Runtime.InteropServices.JavaScript;

namespace LapLog.Models;

public class LapTime
{
    public Track Track { get; set; }
    public Car Car { get; set; }
    public TimeSpan Time { get; set; }
    public DateTime Date { get; set; }

    public LapTime(Track track, Car car, TimeSpan time, DateTime date)
    {
        Track = track;
        Car = car;
        Time = time;
        Date = date;
    }
}