using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public interface IAuthenticateView
    {
        public event EventHandler<AuthenticateEventArgs> AuthenticationTried;
        public event EventHandler QuitRequested;
        public void ShowLoginError(string message);
        public void ShowInternalError(string message);
        public void CloseView();

    }
}
