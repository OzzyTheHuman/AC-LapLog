using System.Collections.ObjectModel;

namespace LapLog.ViewModel;

public class LapTimeListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<LapTimeViewModel> _laptimes;

    public LapTimeListingViewModel()
    {
        _laptimes = new ObservableCollection<LapTimeViewModel>();
    }

    public static LapTimeListingViewModel LoadViewModel()
    {
        LapTimeListingViewModel viewModel = new LapTimeListingViewModel();

        return viewModel;
    }
}