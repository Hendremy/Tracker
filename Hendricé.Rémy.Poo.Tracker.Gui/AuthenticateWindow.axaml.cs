using Avalonia;
using Avalonia.Controls;
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
    }
}
