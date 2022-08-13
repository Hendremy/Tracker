using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class GanttSuperviser
    {
        private readonly IGanttView _view;

        public GanttSuperviser(IGanttView view)
        {
            _view = view;
        }

        public void OnUserAuthentified(object sender, string code)
        {

        }
    }
}
