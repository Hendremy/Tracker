using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public interface IMainView
    {

        public event EventHandler<CancelEventArgs> QuitRequested;
        public event EventHandler QuitForced;

        public void ShowInternalError(string message);

        public void AskForceQuit(string message);
    }
}
