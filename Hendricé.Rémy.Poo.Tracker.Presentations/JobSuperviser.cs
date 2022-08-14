using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class JobSuperviser
    {
        private IJobView _view;
        private Job _job;

        public JobSuperviser(IJobView view, Job job)
        {
            _view = view;
            _job = job;
            _view.InitView(job);
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.StartJobRequested += OnStartClicked;
            _view.EndJobRequested += OnEndClicked;
        }

        private void OnStartClicked(object sender, EventArgs args)
        {
            _job.Start();
        }

        private void OnEndClicked(object sender, EventArgs args)
        {
            _job.End();
        }

    }
}
