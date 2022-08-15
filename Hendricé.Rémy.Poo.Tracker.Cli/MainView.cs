using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public class MainView : CliView, IMainView
    {
        public void ShowInternalError(string message)
        {
            WriteInternalError(message);
        }
    }
}
