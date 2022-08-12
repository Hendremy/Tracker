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

        public MainView()
        {

        }

        public event EventHandler<string> SortRequested;
        public event EventHandler<string> FilterRequested;

        public void ShowUserJobs()
        {
            throw new NotImplementedException();
        }
    }
}
