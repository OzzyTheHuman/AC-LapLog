using LapLog.Models;

namespace LapLog.ViewModels;

public class BestLapsLocalVM : ViewModelBase
{
    private readonly LapTime _lapTime;

    public string TrackName => _lapTime.Track.Name;
    public string CarName => _lapTime.Car.Name;
    public string Time => _lapTime.Time.ToString(@"mm\:ss\.fff");
    public string Date => _lapTime.Date.ToString("dd/MM/yy");
    public BestLapsLocalVM(LapTime lapTime)
    {
        _lapTime = lapTime;
    }
}