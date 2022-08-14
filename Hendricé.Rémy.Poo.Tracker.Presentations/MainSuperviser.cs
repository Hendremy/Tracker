using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class MainSuperviser
    {
        private readonly IMainView _view;
        private readonly ITrackerRepository _repository;
        private IList<Job> _userJobs;

        private JobListSuperviser _jobListSuperviser;
        public JobListSuperviser JobListSuperviser
        {
            set => _jobListSuperviser = value;
        }
        private GanttSuperviser _ganttSuperviser;
        public GanttSuperviser GanttSuperviser
        {
            set => _ganttSuperviser = value;
        }
        private ReportSuperviser _reportSuperviser;
        public ReportSuperviser ReportSuperviser
        {
            set => _reportSuperviser = value;
        }
        public event EventHandler<string> UserAuthentified;

        public MainSuperviser(IMainView view, ITrackerRepository repository)
        {
            _view = view;
            _repository = repository;
        }

        public void OnUserAuthentified(object sender, string code)
        {
            string errorMessage;
            _userJobs = new List<Job>(_repository.GetUserJobs(code, out errorMessage));
            var observableJobs = new ObservableCollection<Job>(_userJobs);
            _jobListSuperviser.SetJobs(_userJobs, observableJobs);
            _ganttSuperviser.SetObservableJobs(observableJobs);
        }

    }
}
