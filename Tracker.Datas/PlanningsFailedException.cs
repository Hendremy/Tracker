using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public class PlanningsFailedException : Exception
    {
        public PlanningsFailedException(string message) : base(message)
        {

        }
    }
}
