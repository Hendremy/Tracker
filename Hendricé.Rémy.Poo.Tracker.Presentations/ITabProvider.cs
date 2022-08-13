using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public interface ITabProvider
    {
        IJobsView GetJobsView();

        //GanttSuperviser GetGanttSuperviser();

        //RapportSuperviser GetRapportSuperviser();
    }
}
