using System.Windows.Input;
using LapLog.Services;
using LapLog.Utilites;

namespace LapLog.ViewModels;

public class NavigationVM : ViewModelBase
{
    private object _currentView;
    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand GoToBestLapsLocalCommand { get; set; }
    public ICommand GoToBestLapsServerCommand { get; set; }
    public ICommand GoToAboutAndInfoCommand { get; set; }
    public ICommand GoToSendFeedbackCommand { get; set; }

    // Lazy<T> - ViewModels are created only when they are needed
    // when choosing different viewmodel, old one is still in memory (isnt deleted by GarbageCollector)
    private readonly Lazy<BestLapsLocalListingVM> _bestLapsLocalVM;
    private readonly Lazy<BestLapsServerVM> _bestLapsServerVM;
    private readonly Lazy<AboutAndInfoVM> _aboutAndInfoVM;
    private readonly Lazy<SendFeedbackVM> _SendFeedbackVM;

    public NavigationVM(ITelemetryProvider telemetryProvider)
    {
        // it does not create the target ViewModels yet
        _bestLapsLocalVM = new Lazy<BestLapsLocalListingVM>(() => new BestLapsLocalListingVM(telemetryProvider));
        _bestLapsServerVM = new Lazy<BestLapsServerVM>(() => new BestLapsServerVM());
        _aboutAndInfoVM = new Lazy<AboutAndInfoVM>(() => new AboutAndInfoVM());
        _SendFeedbackVM = new Lazy<SendFeedbackVM>(() => new SendFeedbackVM());

        // first creation of viewmodel is when reading .Value from Lazy
        GoToBestLapsLocalCommand = new RelayCommand(_ => CurrentView = _bestLapsLocalVM.Value);
        GoToBestLapsServerCommand = new RelayCommand(_ => CurrentView = _bestLapsServerVM.Value);
        GoToAboutAndInfoCommand = new RelayCommand(_ => CurrentView = _aboutAndInfoVM.Value);
        GoToSendFeedbackCommand = new RelayCommand(_ => CurrentView = _SendFeedbackVM.Value);
        
        // starting view
        CurrentView = _bestLapsLocalVM.Value;
    }
}