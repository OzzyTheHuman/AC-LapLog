using System.Collections.ObjectModel;
using System.Windows.Input;
using LapLog.Models;
using LapLog.Services;

namespace LapLog.ViewModels;

public class LapTimeListingViewModel : ViewModelBase
{
    // Data storage in memory, only this ViewModel can use methods like .Add(), .Remove()
    private readonly ObservableCollection<LapTimeViewModel> _laptimes;

    // This is what we are Binding to in LapTimeListingView. {Binding LapTimes} is pointing to this.
    // IEnumerable is the simplest type, this only allows to read, not write.
    public IEnumerable<LapTimeViewModel> LapTimes => _laptimes;
    
    private readonly ITelemetryProvider _telemetryProvider;

    public LapTimeListingViewModel(ITelemetryProvider telemetryProvider)
    {
        _laptimes = new ObservableCollection<LapTimeViewModel>();
        _telemetryProvider = telemetryProvider;

        LoadData();
    }

    private async void LoadData()
    {
        // Working on raw data, without formatting and parsing. Thats why we are using model, not viewmodel
        IEnumerable<LapTime> rawData = await _telemetryProvider.GetAllLapTimes();
        _laptimes.Clear();

        foreach (var lap in rawData)
        {
            _laptimes.Add(new LapTimeViewModel(lap));
        }
    }
}