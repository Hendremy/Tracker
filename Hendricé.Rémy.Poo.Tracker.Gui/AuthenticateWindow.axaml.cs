using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Text.RegularExpressions;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class AuthenticateWindow : Window, IAuthenticateView
    {
        private TextBox _code;
        private TextBox _password;
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
            _code = this.FindControl<TextBox>("Code");
            _password = this.FindControl<TextBox>("Password");
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
            this.Close();
        }

        public void ShowInternalError(string message)
        {
            var errWin = new ErrorWindow();
            errWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            errWin.ShowError(this, message);
        }

        public void ShowLoginError(string message)
        {
            ShowPasswordError(message);
        }

        private void Quit_Click(object sender, RoutedEventArgs args)
        {
            QuitRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Authenticate_Click(object? sender, RoutedEventArgs args)
        {
            ResetErrorMessages();
            if (CheckCredentialsValidity()) 
            {
                AuthenticateEventArgs authArgs = new AuthenticateEventArgs(_code.Text, _password.Text);
                AuthenticationTried?.Invoke(this, authArgs);
            }
        }

        private void ShowCodeError(string message)
        {
            _codeError.Text = message;
            _codeError.Opacity = 1;
        }

        private void ShowPasswordError(string message)
        {
            _passwordError.Text = message;
            _passwordError.Opacity = 1;
        }

        private void ResetErrorMessages()
        {
            _codeError.Opacity = 0;
            _passwordError.Opacity = 0;
        }

        private bool CheckCredentialsValidity()
        {
            bool valid = true;
            if (!MatchesCodePattern(_code.Text))
            {
                valid = false;
                ShowCodeError("Code invalide");
            }
            if (string.IsNullOrWhiteSpace(_password.Text))
            {
                valid = false;
                ShowPasswordError("Le mot de passe doit compter au moins un symbole non-blanc");
            }

            return valid;
        }

        private bool MatchesCodePattern(string code)
        {
            Regex reg = new("^[A-z]\\d{3}$");
            return code != null && reg.IsMatch(code);
        }
    }
}
