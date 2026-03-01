using LapLog.Models;

namespace LapLog.ViewModels;

public class LapTimeViewModel : ViewModelBase
{
    private readonly LapTime _lapTime;

    public string TrackName => _lapTime.TrackName;
    public string CarName => _lapTime.CarName;
    public string Time => _lapTime.Time.ToString(@"mm\:ss\.fff");
    public string Date => _lapTime.Date.ToString("dd/MM/yy");
    public LapTimeViewModel(LapTime lapTime)
    {
        _lapTime = lapTime;
    }
}