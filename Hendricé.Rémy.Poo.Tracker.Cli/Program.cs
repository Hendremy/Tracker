using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        private Program()
        {
            var repository = new JSONTrackerRepository("../../../../../json", "users.json", "plannings");
            var authenticator = new Authenticator();
            var mainSuperviser = CreateJobsSuperviser(repository);
            CreateAuthentifySuperviser(mainSuperviser, repository, authenticator);
        }

        private void CreateAuthentifySuperviser(JobsSuperviser mainSuperviser, ITrackerRepository repo, IAuthenticate auth)
        {
            var view = new AuthenticateView();
            var authSuperviser = new AuthenticateSuperviser(view, repo, auth);
            view.ShowDialog();
            authSuperviser.UserAuthentified += mainSuperviser.OnUserAuthentified;
        }

        private JobsSuperviser CreateJobsSuperviser(ITrackerRepository repo)
        {
            var view = new MainView();
            var sorter = initSortHandler();
            var filter = initFilterHandler();
            var conflictDetector = new JobConflictDetector();
            var mainSuperviser = new JobsSuperviser(view, repo, sorter, filter, conflictDetector);
            return mainSuperviser;
        }

        private SortHandler initSortHandler()
        {
            var startdate = new BaseSort();
            var status = new StatusSort(startdate);
            var planningsort = new PlanningSort(status);
            return new SortHandler(planningsort, new SortParams());
        }

        private FilterHandler initFilterHandler()
        {
            var date = new BaseFilter();
            var status = new StatusFilter(date);
            var planning = new PlanningFilter(status);
            return new FilterHandler(planning, new FilterParams());
        }
    }
}
