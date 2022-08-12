using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public interface IMainView
    {
        public event EventHandler<string> SortRequested;
        public event EventHandler<string> FilterRequested;

        public void ShowUserJobs();
    }
}
