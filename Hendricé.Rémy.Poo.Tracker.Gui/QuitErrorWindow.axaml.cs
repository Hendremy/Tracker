using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class QuitErrorWindow : Window
    {
        private TextBlock _error;
        public event EventHandler QuitForced;

        public QuitErrorWindow()
        {
            InitializeComponent();
            LocateControls();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void LocateControls()
        {
            _error = this.FindControl<TextBlock>("Error");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void ShowError(Window owner, string message)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _error.Text = message;
            this.ShowDialog(owner);
        }

        private void Cancel(object? sender, RoutedEventArgs args)
        {
            this.Close();
        }

        private void ForceQuit(object? sender, RoutedEventArgs args)
        {
            QuitForced?.Invoke(this, args);
        }
    }
}
