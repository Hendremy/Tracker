using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public class SuperviserCreator
    {
        private ITrackerRepository _repository;
        public SuperviserCreator()
        {
            _repository = new JSONTrackerRepository("../../../../../json", "users.json", "plannings");
        }

        public MainSuperviser CreateMainSuperviser(IMainView view)
        {
            return new MainSuperviser(view);
        }

        public AuthenticateSuperviser CreateAuthenticateSuperviser(IAuthenticateView view)
        {
            return new AuthenticateSuperviser(view, _repository, GetAuthenticator());
        }

        public JobsSuperviser CreateJobsSuperviser(IJobsView view)
        {
            return new JobsSuperviser(view, _repository, GetSortHandler(), GetFilterHandler(), GetConflictDetector());
        }

        public GanttSuperviser CreateGanttSuperviser(IGanttView view)
        {
            return new GanttSuperviser(view);
        }

        public ReportSuperviser CreateReportSuperviser(IReportView view)
        {
            return new ReportSuperviser(view);
        }

        private Authenticator GetAuthenticator()
        {
            return new Authenticator();
        }

        private SortHandler GetSortHandler()
        {
            var planningsort = new PlanningSort(new StatusSort(new BaseSort()));
            return new SortHandler(planningsort, new SortParams());
        }

        private FilterHandler GetFilterHandler()
        {
            var planning = new PlanningFilter(new StatusFilter(new BaseFilter()));
            return new FilterHandler(planning, new FilterParams());
        }

        private ConflictDetector GetConflictDetector()
        {
            return new ConflictDetector();
        }
    }
}
