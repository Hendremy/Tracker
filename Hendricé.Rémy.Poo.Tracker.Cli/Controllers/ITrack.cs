using Hendricé.Rémy.Poo.Tracker.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli.Controllers
{
    public interface ITrack
    {
        IAuthentify AuthentifyController { get;}

        ITrackerRepository Repository { get; }

    }
}
