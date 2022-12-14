using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public interface IProvideJobPresenter
    {
        public JobPresenter CreateJobPresenter(IJobView view, Job job);
    }
}
