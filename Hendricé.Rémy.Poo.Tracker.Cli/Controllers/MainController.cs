using Hendricé.Rémy.Poo.Tracker.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli.Controllers
{
    public class MainController : ITrack
    {
        private readonly ITrackerRepository _repository;
        public MainController(ITrackerRepository repository)
        {
            _repository = repository;
        }

        public IAuthentify AuthentifyController => new AuthentifyController(this);
        public ITrackerRepository Repository => _repository;
    }
}
