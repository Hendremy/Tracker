using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class JobViewCreatedEventArgs
    {
        private IJobView _view;
        private Job _job;

        public JobViewCreatedEventArgs(IJobView view, Job job)
        {
            _view = view;
            _job = job;
        }

        public IJobView View => _view;
        public Job Job => _job;
    }
}
