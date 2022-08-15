using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class GanttSuperviser
    {
        private readonly IGanttView _view;

        private ObservableCollection<Job> _jobs;

        public GanttSuperviser(IGanttView view)
        {
            _view = view;
        }

        public void SetObservableJobs(ObservableCollection<Job> jobs)
        {
            _jobs = jobs;
            _jobs.CollectionChanged += OnJobCollectionChanged;
        }

        private void OnJobCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            _view.UpdateItems(_jobs);
        }
    }
}
