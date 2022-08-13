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
        private TextBox _userCode;
        private TextBox _userPassword;
        private TextBlock _codeError;
        private TextBlock _passwordError;

        public AuthenticateWindow()
        {
            InitializeComponent();
            LocateControls();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void LocateControls()
        {
            _userCode = this.FindControl<TextBox>("Code");
            _userPassword = this.FindControl<TextBox>("Password");
            _codeError = this.FindControl<TextBlock>("CodeError");
            _passwordError = this.FindControl<TextBlock>("PasswordError");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
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

        private void Quit_Click(object sender, RoutedEventArgs args)
        {
            QuitRequested(this, EventArgs.Empty);
        }

        private void Authenticate_Click(object? sender, RoutedEventArgs args)
        {

        }
    }
}
