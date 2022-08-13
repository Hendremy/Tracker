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

        public MainSuperviser(IMainView view, ITrackerRepository repository)
        {
            _repository = repository;
            _view = view;
        }

        public void OnUserAuthentified(object sender, string code)
        {
            _userJobs = _repository.GetUserJobs(code);
            _observableJobs = new ObservableCollection<Job>(_userJobs);
            _view.SubscribeToJobs(_observableJobs);
        }
    }
}
