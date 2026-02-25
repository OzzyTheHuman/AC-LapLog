using System.Collections.ObjectModel;
using System.Windows.Input;
using LapLog.Models;

namespace LapLog.ViewModel;

public class LapTimeListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<LapTimeViewModel> _laptimes;
    public IEnumerable<LapTimeViewModel> LapTimes => _laptimes;

    public LapTimeListingViewModel()
    {
        _laptimes = new ObservableCollection<LapTimeViewModel>();
        
        _laptimes.Add(new LapTimeViewModel(new LapTime("Akina", "Toyota AE86", new TimeSpan(0, 4, 30))));
        _laptimes.Add(new LapTimeViewModel(new LapTime("Akagi", "Mazda RX7", new TimeSpan(0, 4, 37))));
    }
}