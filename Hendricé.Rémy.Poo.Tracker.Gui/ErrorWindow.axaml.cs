using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class ErrorWindow : Window
    {
        public ErrorWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void ShowError(Window owner, string message)
        {
            var messageBlock = this.FindControl<TextBlock>("ErrorMessage");
            messageBlock.Text = message;
            this.ShowDialog(owner);
        }

        private void Error_Seen(object? sender, RoutedEventArgs args)
        {
            this.Close();
        }
    }
}
