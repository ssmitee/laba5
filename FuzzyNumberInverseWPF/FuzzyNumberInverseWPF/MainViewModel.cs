using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FuzzyNumberInverseWPF
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private double _a;
        private double _b;
        private string _inverseResult;

        public double A
        {
            get { return _a; }
            set
            {
                _a = value;
                OnPropertyChanged(nameof(A));
            }
        }

        public double B
        {
            get { return _b; }
            set
            {
                _b = value;
                OnPropertyChanged(nameof(B));
            }
        }

        public string InverseResult
        {
            get { return _inverseResult; }
            set
            {
                _inverseResult = value;
                OnPropertyChanged(nameof(InverseResult));
            }
        }

        public RelayCommand CalculateInverseCommand { get; private set; }

        public MainViewModel()
        {
            CalculateInverseCommand = new RelayCommand(CalculateInverse);
        }

        private void CalculateInverse(object parameter)
        {
            try
            {
                var fuzzyNumber = new FuzzyNumber(A, B);
                var inverse = FuzzyNumberOperations.Inverse(fuzzyNumber);
                InverseResult = inverse.ToString();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : System.Windows.Input.ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
