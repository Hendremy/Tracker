using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    class AuthenticateView : CliView, IAuthenticateView
    {
        public event EventHandler<AuthenticateEventArgs> AuthenticationTried;
        public event EventHandler QuitRequested;

        private const string QUIT_CMD = "q";
        private bool _stop = false;
        public AuthenticateView()
        {
            Welcome();
            _thread = new Thread(new ThreadStart(Loop));
        }

        private void Welcome()
        {
            WriteLine("IN-B2-UE11-C# : Tracker"
                + "\n=======================" 
                + "\nAuthentifiez-vous pour afficher vos tâches !");
        }

        public void ShowDialog()
        {
            StartThread();
        }

        private void Loop()
        {
            while (!_stop)
            {
                string code = AskCode();
                if (code.Equals(QUIT_CMD)) break;
                string password = AskPassword();
                AuthenticateEventArgs args = new AuthenticateEventArgs(code, password);
                AuthenticationTried?.Invoke(this, args);
            }
            QuitRequested.Invoke(this, EventArgs.Empty);
        }

        private string AskCode()
        {
            string question = $"Votre code : (Encodez {QUIT_CMD} pour quitter l'application)";
            string code = AskString(question);
            while (!MatchesCodePattern(code) && !code.Equals("q"))
            {
                WriteUserError("Code invalide");
                code = AskString(question);
            }
            return code;
        }

        private bool MatchesCodePattern(string code)
        {
            Regex reg = new("^[A-z]\\d{3}$");
            return code != null && reg.IsMatch(code);
        }

        private string AskPassword()
        {
            string question = "Votre mot de passe :";
            string password = AskString(question);
            while (string.IsNullOrWhiteSpace(password))
            {
                WriteUserError("Le mot de passe doit compter au moins un symbole non-blanc");
                password = AskString(question);
            }
            return password;
        }

        public void ShowLoginError(string message)
        {
            WriteUserError(message);
        }

        public void ShowInternalError(string message)
        {
            WriteInternalError(message);
        }

        public void CloseView()
        {
            _stop = true;
        }
    }
}
