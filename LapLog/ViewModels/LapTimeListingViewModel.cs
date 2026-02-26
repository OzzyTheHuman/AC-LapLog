using System.Collections.ObjectModel;
using System.Windows.Input;
using LapLog.Models;

namespace LapLog.ViewModels;

public class LapTimeListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<LapTimeViewModel> _laptimes;
    // This is what we are Binding to in LapTimeListingView. 
    // {Binding LapTimes} is pointing to this.
    public IEnumerable<LapTimeViewModel> LapTimes => _laptimes;

    public LapTimeListingViewModel()
    {
        _laptimes = new ObservableCollection<LapTimeViewModel>();
        
        _laptimes.Add(new LapTimeViewModel(new LapTime("Akina", "Toyota AE86", new TimeSpan(0, 4, 30))));
        _laptimes.Add(new LapTimeViewModel(new LapTime("Akagi", "Mazda RX7", new TimeSpan(0, 4, 37))));
        _laptimes.Add(new LapTimeViewModel(new LapTime("Nordschleife", "Nissan Silvia S15", new TimeSpan(9, 50, 680))));
    }
}