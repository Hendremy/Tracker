using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public class MainView : CliView, IMainView
    {
        public event EventHandler<CancelEventArgs> QuitRequested;
        public event EventHandler QuitForced;

        public void AskForceQuit(string message)
        {
            ShowInternalError(message);
            string answer = AskString("Des erreurs sont survenues en essayant de quitter l'application\n Entrez \"QUIT\" pour forcer l'arrêt");
            if (answer.Equals("QUIT"))
            {
                QuitForced?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ShowInternalError(string message)
        {
            WriteInternalError(message);
        }
    }
}
