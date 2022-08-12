using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public abstract class CliView
    {
        private readonly Arranger _arranger;

        public CliView()
        {
            _arranger = Arranger.GetInstance();
        }
    }
}
