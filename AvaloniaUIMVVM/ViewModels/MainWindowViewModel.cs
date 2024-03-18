using ReactiveUI;
using System;
using System.Reactive;
using Avalonia.Threading;

namespace AvaloniaUIMVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Olá, Mundo!";
        private string _message;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);

        }

        public delegate void OkClickedEventHandler(object sender, EventArgs e);
        public event OkClickedEventHandler? OkClicked;
        public delegate void ExitClickedEventHandler(object sender, EventArgs e);
        public event ExitClickedEventHandler? ExitClicked;

        public ReactiveCommand<Unit, Unit> OkCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        private DispatcherTimer _timer;

        public MainWindowViewModel()
        {
            OkCommand = ReactiveCommand.Create(OnOkClicked);
            ExitCommand = ReactiveCommand.Create(OnExitClicked);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, e) => Message = null;
        }

        private void OnOkClicked()
        {
            // Lógica do comando OK 
            Message = "Botão Ok clicado!";
            _timer.Start();
            Console.WriteLine("Botão OK Clicado!");
            OkClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnExitClicked()
        {
            // Lógica do comando Exit 
            Console.WriteLine("fechando aplicação");
            System.Environment.Exit(0);
            ExitClicked?.Invoke(this, EventArgs.Empty);
            
            
        }
    }
}

