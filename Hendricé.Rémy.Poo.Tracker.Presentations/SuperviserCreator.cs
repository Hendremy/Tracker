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
    public class SuperviserCreator : IProvideJobSuperviser
    {
        private ITrackerServices _services;

        public SuperviserCreator(ITrackerServices services)
        {
            _services = services;
        }

        public MainSuperviser CreateMainSuperviser(IMainView view)
        {
            return new MainSuperviser(view, _services.GetTrackerRepository());
        }

        public AuthenticateSuperviser CreateAuthenticateSuperviser(IAuthenticateView view)
        {
            return new AuthenticateSuperviser(view, _services.GetTrackerRepository(), _services.GetAuthenticator());
        }

        public JobListSuperviser CreateJobListSuperviser(IJobListView view)
        {
            return new JobListSuperviser(view, _services.GetSortHandler(), _services.GetFilterHandler(), _services.GetConflictDetector(), this);
        }

        public GanttSuperviser CreateGanttSuperviser(IGanttView view)
        {
            return new GanttSuperviser(view, _services.GetGanttItemsCreator());
        }

        public ReportSuperviser CreateReportSuperviser(IReportView view)
        {
            return new ReportSuperviser(view);
        }

        public JobSuperviser CreateJobSuperviser(IJobView view, Job job)
        {
            return new JobSuperviser(view, job);
        }
    }
}
