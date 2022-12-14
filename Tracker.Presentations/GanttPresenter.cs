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
    public class GanttPresenter
    {
        private readonly IGanttView _view;
        private readonly ICreateGanttItems _ganttCreator;

        private ObservableCollection<Job> _jobs;

        public GanttPresenter(IGanttView view, ICreateGanttItems ganttCreator)
        {
            _view = view;
            _ganttCreator = ganttCreator;
        }

        public void SetObservableJobs(ObservableCollection<Job> jobs)
        {
            _jobs = jobs;
            _jobs.CollectionChanged += OnJobCollectionChanged;
            UpdateItems();
        }

        private void OnJobCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            UpdateItems();
        }

        private void UpdateItems()
        {
            IList<GanttJob> ganttItems = _ganttCreator.CreateGanttJobs(_jobs);
            _view.UpdateItems(ganttItems);
        }
    }
}
