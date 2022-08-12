using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    internal interface IPlanningRepository
    {
        IEnumerable<Job> getUserJobs(string code);
    }
}
