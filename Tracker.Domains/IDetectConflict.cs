using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public interface IDetectConflict
    {
        public IEnumerable<JobConflict> DetectConflicts(IEnumerable<Job> jobs);
    }
}
