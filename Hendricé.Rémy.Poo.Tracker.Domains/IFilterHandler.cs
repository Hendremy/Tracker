using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public interface IFilterHandler
    {
        public FilterParams Params { set; }

        public IEnumerable<Job> Handle(IEnumerable<Job> jobs);
    }
}
