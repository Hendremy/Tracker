using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class Job
    {
        private readonly string _name;
        private readonly string _description;
        private readonly Planning _planning;
        private readonly TimeReport _timeReport;

        public Job(string name, string description, Planning planning, TimeReport timeReport)
        {
            _name = name;
            _description = description;
            _planning = planning;
            _timeReport = timeReport;
        }

        public string Name => _name;

        public string Description => _description;

        public string Planning => _planning.Name;

        public TimeReport TimeReport => _timeReport;

        public JobStatus GetStatus() => _timeReport.GetStatus();

        public double GetDelay() => _timeReport.GetDelay();
    }
}
