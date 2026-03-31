using System.Windows.Input;

namespace LapLog.Utilites;

// Binding commands between the ViewModel and UI elements
public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute; 

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        // fail-fast, 
        // new RelayCommand(null) <- we throw exception immediately to avoid errors in runtime
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }
    
    /*
     * Every time the user does something in the window, call CanExecute method again,
     * because the situation may have changed and I need to lock or unlock the button!
     */
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
    
    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }
    
    public void Execute(object? parameter) => _execute(parameter);
}