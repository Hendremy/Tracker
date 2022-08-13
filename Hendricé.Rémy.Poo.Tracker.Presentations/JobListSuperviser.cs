using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class JobListSuperviser
    {
        private readonly IJobListView _view;
        private readonly ITrackerRepository _repository;
        private IEnumerable<Job> _userJobs;
        private ISortHandler _sortHandler;
        private IFilterHandler _filterHandler;
        private IDetectConflict _conflictDetector;

        public JobListSuperviser(IJobListView view, ITrackerRepository repository, 
            ISortHandler sortHandler, IFilterHandler filterHandler, IDetectConflict conflictDetector)
        {
            _repository = repository;
            _view = view;
            _sortHandler = sortHandler;
            _filterHandler = filterHandler;
            _conflictDetector = conflictDetector;
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.QuitRequested += OnQuitRequested;
            _view.SortRequested += OnSortRequested;
            _view.FilterRequested += OnFilterRequested;
        }

        public void OnUserAuthentified(object sender, string code)
        {
            _userJobs = _repository.GetUserJobs(code);
            _view.ShowConflicts(_conflictDetector.DetectConflicts(_userJobs));
            _view.Update(_userJobs);
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

        private void SortAndFilterJobs()
        {
            IEnumerable<Job> jobsCopy = new HashSet<Job>(_userJobs);
            jobsCopy = _filterHandler.Handle(jobsCopy);
            jobsCopy = _sortHandler.Handle(jobsCopy);
            _view.Update(jobsCopy);
        }

        private void CloseView()
        {
            _view.Close();
        }
    }
}
