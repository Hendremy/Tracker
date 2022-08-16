using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class PresenterCreator : IProvideJobPresenter
    {
        private ITrackerServices _services;

        public PresenterCreator(ITrackerServices services)
        {
            _services = services;
        }

        public MainPresenter CreateMainPresenter(IMainView view)
        {
            return new MainPresenter(view, _services.GetTrackerRepository());
        }

        public AuthenticatePresenter CreateAuthenticatePresenter(IAuthenticateView view)
        {
            return new AuthenticatePresenter(view, _services.GetTrackerRepository(), _services.GetAuthenticator());
        }

        public JobListPresenter CreateJobListPresenter(IJobListView view)
        {
            return new JobListPresenter(view, _services.GetSortHandler(), _services.GetFilterHandler(), _services.GetConflictDetector(), this);
        }

        public GanttPresenter CreateGanttPresenter(IGanttView view)
        {
            return new GanttPresenter(view, _services.GetGanttItemsCreator());
        }

        public ReportPresenter CreateReportPresenter(IReportView view)
        {
            return new ReportPresenter(view);
        }

        public JobPresenter CreateJobPresenter(IJobView view, Job job)
        {
            return new JobPresenter(view, job);
        }
    }
}
