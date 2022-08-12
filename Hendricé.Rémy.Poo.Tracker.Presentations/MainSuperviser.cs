using Hendricé.Rémy.Poo.Tracker.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class MainSuperviser
    {
        private readonly IMainView _view;
        private readonly ITrackerRepository _repository;

        public MainSuperviser(IMainView view, ITrackerRepository repository)
        {
            _repository = repository;
            _view = view;
        }

        public ITrackerRepository Repository => _repository;
    }
}
