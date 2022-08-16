using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class JobListSuperviser
    {
        private readonly IJobListView _view;
        private readonly ISortHandler _sortHandler;
        private readonly IFilterHandler _filterHandler;
        private readonly IDetectConflict _conflictDetector;
        private readonly IProvideJobSuperviser _superviserProvider;

        private ISet<Job> _userJobs;
        private ObservableCollection<Job> _observableJobs;//TODO: P-e pas besoin d'en faire une collection observable

        public JobListSuperviser(IJobListView view, 
            ISortHandler sortHandler, IFilterHandler filterHandler, 
            IDetectConflict conflictDetector, IProvideJobSuperviser superviserProvider)
        {
            _view = view;
            _sortHandler = sortHandler;
            _filterHandler = filterHandler;
            _conflictDetector = conflictDetector;
            _superviserProvider = superviserProvider;
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.QuitRequested += OnQuitRequested;
            _view.SortRequested += OnSortRequested;
            _view.FilterRequested += OnFilterRequested;
            _view.JobViewCreated += OnJobViewCreated;
        }

        public void SetJobs(IList<Job> jobs, ObservableCollection<Job> observableJobs)
        {
            _userJobs = new HashSet<Job>(jobs);
            SubscribeToJobsPropertyChanged(_userJobs);
            _observableJobs = observableJobs;
            _view.ShowConflicts(_conflictDetector.DetectConflicts(_userJobs));
            _view.Update(_userJobs);
        }

        private void SubscribeToJobsPropertyChanged(ICollection<Job> jobs)
        {
            foreach (var job in jobs)
            {
                job.PropertyChanged += OnJobPropertyChanged;
            }
        }

        public void OnQuitRequested(object sender, EventArgs args)
        {
            CloseView();
        }

        public void OnSortRequested(object sender, SortParams args)
        {
            _sortHandler.Params = args;
            SortAndFilterJobs();
        }

        public void OnFilterRequested(object sender, FilterParams args)
        {
            _filterHandler.Params = args;
            SortAndFilterJobs();
        }

        private void OnJobPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SortAndFilterJobs();
        }

        private void SortAndFilterJobs()
        {
            IEnumerable<Job> jobsCopy = new HashSet<Job>(_userJobs);
            jobsCopy = _filterHandler.Handle(jobsCopy);
            jobsCopy = _sortHandler.Handle(jobsCopy);
            RefreshObservableJobs(jobsCopy);
            _view.Update(jobsCopy);
        }

        private void RefreshObservableJobs(IEnumerable<Job> jobsToAdd)
        {
            _observableJobs.Clear();
            foreach(Job job in jobsToAdd)
            {
                _observableJobs.Add(job);
            }
        }

        private void OnJobViewCreated(object sender, JobViewCreatedEventArgs args)
        {
            var jobSuperviser = _superviserProvider.CreateJobSuperviser(args.View, args.Job);
        }

        private void CloseView()
        {
            _view.Close();
        }
    }
}
