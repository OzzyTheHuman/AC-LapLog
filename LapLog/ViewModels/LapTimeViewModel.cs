using LapLog.Models;

namespace LapLog.ViewModels;

public class LapTimeViewModel : ViewModelBase
{
    private readonly LapTime _lapTime;

    public string TrackName => _lapTime.TrackName;
    public string CarName => _lapTime.CarName;
    public string Time => _lapTime.Time.ToString("g");

    public LapTimeViewModel(LapTime lapTime)
    {
        _lapTime = lapTime;
    }
}