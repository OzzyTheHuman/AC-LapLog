using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
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
    private IEnumerable<LapTimeViewModel> _allLapTimes;
    private string _selectedTrack;
    public string SelectedTrack
    {
        get => _selectedTrack;
        set
        {
            if (SetField(ref _selectedTrack, value))
            {
                ApplyFilters();
            }
        }
    }
    
    private string _selectedCar;
    public string SelectedCar
    {
        get => _selectedCar;
        set
        {
            if (SetField(ref _selectedCar, value))
            {
                ApplyFilters();
            }
        }
    }

    private bool _sortByTime;
    public bool SortByTime
    {
        get => _sortByTime;
        set
        {
            if (SetField(ref _sortByTime, value))
            {
                ApplyFilters();
            }
        }
    }

    public IEnumerable<string> AvailableTracks
    {
        get
        {
            if (_allLapTimes == null)
            {
                return new List<string>();
            }

            var tracks = _allLapTimes.Select(lap => lap.TrackName).ToList();

            var distinctTracks = tracks.Distinct().ToList();
            distinctTracks.Sort();
            distinctTracks.Insert(0, "All Tracks");

            return distinctTracks;
        }
    }
    
    public IEnumerable<string> AvailableCars
    {
        get
        {
            if (_allLapTimes == null)
            {
                return new List<string>();
            }

            var cars = _allLapTimes.Select(car => car.CarName).ToList();

            var distinctCars = cars.Distinct().ToList();
            distinctCars.Sort();
            distinctCars.Insert(0, "All Cars");

            return distinctCars;
        }
    }
    
    public LapTimeListingViewModel(ITelemetryProvider telemetryProvider)
    {
        _laptimes = new ObservableCollection<LapTimeViewModel>();
        _telemetryProvider = telemetryProvider;

        LoadData();
        ApplyFilters();
    }

    private async void LoadData()
    {
        // Working on raw data, without formatting and parsing. Thats why we are using model, not viewmodel
        // TODO: Its not real MVVM, this should be in xaml
        try
        {
            IEnumerable<LapTime> rawData = await _telemetryProvider.GetAllLapTimes();

            var loadedViewModels = new List<LapTimeViewModel>();
            _laptimes.Clear();

            foreach (var lap in rawData)
            {
                loadedViewModels.Add(new LapTimeViewModel(lap));
            }

            _allLapTimes = loadedViewModels;
            
            OnPropertyChanged(nameof(AvailableTracks));
            OnPropertyChanged(nameof(AvailableCars));
            
            SelectedTrack = "All Tracks";
            SelectedCar = "All Cars";
        }
        catch (System.IO.FileNotFoundException)
        {
            MessageBox.Show(
                "An error occurred while accessing the personalbest.ini file.\nExpected file path: Documents\\Assetto Corsa\\personalbest.ini",
                "Error",
                MessageBoxButton.OK, MessageBoxImage.Error
            );
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            MessageBox.Show(
                "An error occurred while accessing the Assetto Corsa directory.\nExpected file path: Documents\\Assetto Corsa\\personalbest.ini",
                "Error",
                MessageBoxButton.OK, MessageBoxImage.Error
            );
        }
    }

    private void ApplyFilters()
    {
        if (_allLapTimes == null) return;
        _laptimes.Clear();
        
        IEnumerable<LapTimeViewModel> results = _allLapTimes;
        
        if (!string.IsNullOrEmpty(SelectedTrack) && SelectedTrack != "All Tracks")
        {
            results = results.Where(lap => lap.TrackName == SelectedTrack);
        }

        if (!string.IsNullOrEmpty(SelectedCar) && SelectedCar != "All Cars")
        {
            results = results.Where(lap => lap.CarName == SelectedCar);
        }

        if (SortByTime)
        {
            results = results.OrderBy(lap => lap.Time);
        }
        else
        {
            results = results.OrderByDescending(lap => DateTime.ParseExact(lap.Date, "dd.MM.yy", CultureInfo.InvariantCulture));
        }
        
        foreach (var lap in results)
        {
            _laptimes.Add(lap);
        }
    }
}