using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class AuthenticateWindow : Window, IAuthenticateView
    {
        public AuthenticateWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public event EventHandler<AuthenticateEventArgs> AuthenticationTried;
        public event EventHandler QuitRequested;

        public void CloseView()
        {
            throw new NotImplementedException();
        }

        public void ShowInternalError(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowLoginError(string message)
        {
            throw new NotImplementedException();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Quit_Click(object sender, RoutedEventArgs args)
        {
            QuitRequested(this, EventArgs.Empty);
        }

        private void Authenticate_Click(object? sender, RoutedEventArgs args)
        {

        }
    }
}
