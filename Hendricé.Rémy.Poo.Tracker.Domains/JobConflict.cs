using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class JobConflict
    {
        private readonly Job _jobA;
        private readonly Job _jobB;
        private IList<DateTime> _conflictDates;

        public JobConflict(Job jobA, Job jobB, IEnumerable<DateTime> conflictDates)
        {
            _jobA = jobA;
            _jobB = jobB;
            _conflictDates = new List<DateTime>(conflictDates);
        }

        public string JobAPlanning => _jobA.Planning;
        public string JobAName => _jobA.Name;

        public string JobBPlanning => _jobB.Planning;
        public string JobBName => _jobB.Name;

        public IEnumerable<DateTime> ConflictDates => new List<DateTime>(_conflictDates);

    }
}
