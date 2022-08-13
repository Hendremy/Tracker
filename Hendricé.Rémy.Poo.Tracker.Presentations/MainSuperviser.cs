using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class MainSuperviser
    {
        private readonly IMainView _view;
        private readonly ITrackerRepository _repository;
        private IEnumerable<Job> _userJobs;
        private ObservableCollection<Job> _observableJobs;
        private ISortHandler _sortHandler;
        private IFilterHandler _filterHandler;

        public MainSuperviser(IMainView view, ITrackerRepository repository, 
            ISortHandler sortHandler, IFilterHandler filterHandler)
        {
            _repository = repository;
            _view = view;
            _sortHandler = sortHandler;
            _filterHandler = filterHandler;
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
            _observableJobs = new ObservableCollection<Job>(_userJobs);
            _view.SubscribeToJobs(_observableJobs);
        }

        public void OnQuitRequested(object sender, EventArgs args)
        {
            CloseView();
        }

        public void OnSortRequested(object sender, SortParams args)
        {
            _sortHandler.Params = args;
            UpdateJobs();
        }

        public void OnFilterRequested(object sender, FilterParams args)
        {
            _filterHandler.Params = args;
            UpdateJobs();
        }

        private void UpdateJobs()
        {
            IEnumerable<Job> jobsCopy = new HashSet<Job>(_userJobs);
            jobsCopy = _filterHandler.Handle(jobsCopy);
            jobsCopy = _sortHandler.Handle(jobsCopy);
            _observableJobs.Intersect(jobsCopy);
        }

        private void CloseView()
        {
            _view.Close();
        }
    }
}
