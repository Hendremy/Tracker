using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli.Controllers
{
    public class AuthentifyController : IAuthentify
    {
        private readonly ITrack _mainController;
        public AuthentifyController(ITrack mainController)
        {
            _mainController = mainController;
        }
    }
}
