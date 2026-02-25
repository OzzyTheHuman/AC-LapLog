using System.Configuration;
using System.Data;
using System.Windows;
using LapLog.Models;
using LapLog.Services;
using LapLog.ViewModel;
using LapLog.Views;

namespace LapLog;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly LapTime _lapTime;
    
    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow()
        {
            DataContext = new LapTimeListingViewModel()
        };
        
        MainWindow.Show();
        base.OnStartup(e);
    }
}