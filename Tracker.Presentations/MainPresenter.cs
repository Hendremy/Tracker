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
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly ITrackerRepository _repository;
        private IList<Job> _userJobs;

        public event EventHandler AboutToQuit;

        private JobListPresenter _jobListSuperviser;
        public JobListPresenter JobListPresenter
        {
            set => _jobListSuperviser = value;
        }
        private GanttPresenter _ganttSuperviser;
        public GanttPresenter GanttSuperviser
        {
            set => _ganttSuperviser = value;
        }

        public event EventHandler<string> UserAuthentified;

        public MainPresenter(IMainView view, ITrackerRepository repository)
        {
            _view = view;
            _repository = repository;
            SubscribeToViewEvents(view);
        }

        private void SubscribeToViewEvents(IMainView view)
        {
            _view.QuitRequested += NotifyAboutToQuit;
            _view.QuitForced += OnForceQuit;
        }

        public void OnUserAuthentified(object sender, string code)
        {
            string errorMessage;
            _userJobs = new List<Job>(_repository.GetUserJobs(code, out errorMessage));
            if (!string.IsNullOrWhiteSpace(errorMessage)) _view.ShowInternalError(errorMessage);
            var observableJobs = new ObservableCollection<Job>(_userJobs);
            _jobListSuperviser.SetJobs(_userJobs, observableJobs);
            //Supervisers nullable car la CLI n'implémente pas ces superviseurs mais pourrait à l'avenir
            _ganttSuperviser?.SetObservableJobs(observableJobs);
        }

        public void NotifyAboutToQuit(object sender, CancelEventArgs args)
        {
            try
            {
                _repository.Dispose();
                _view.CloseView();
            }catch(TrackerRepositoryException ex)
            {
                args.Cancel = true;
                _view.AskForceQuit(ex.Message);
            }
        }

        private void OnForceQuit(object sender, EventArgs args)
        {
            _view.CloseView();
        }
    }
}
