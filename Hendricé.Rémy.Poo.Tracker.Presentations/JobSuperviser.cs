using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class JobSuperviser
    {
        private IJobView _view;

        public JobSuperviser(IJobView view)
        {
            _view = view;
        }
    }
}
