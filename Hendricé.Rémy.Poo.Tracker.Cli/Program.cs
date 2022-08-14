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

        //TODO: utiliser superviser creator pour créer les superviser
        private Program()
        {
            var repository = new JSONTrackerRepository("../../../../../json", "users.json", "plannings");
            var authenticator = new Authenticator();
            var mainSuperviser = CreateJobsSuperviser(repository);
            CreateAuthentifySuperviser(mainSuperviser, repository, authenticator);
        }

        private void CreateAuthentifySuperviser(JobListSuperviser mainSuperviser, ITrackerRepository repo, IAuthenticate auth)
        {
            var view = new AuthenticateView();
            var authSuperviser = new AuthenticateSuperviser(view, repo, auth);
            view.ShowDialog();
            authSuperviser.UserAuthentified += mainSuperviser.OnUserAuthentified;
        }

        private JobListSuperviser CreateJobsSuperviser(ITrackerRepository repo)
        {
            var view = new MainView();
            var sorter = initSortHandler();
            var filter = initFilterHandler();
            var conflictDetector = new ConflictDetector();
            var mainSuperviser = new JobListSuperviser(view, sorter, filter, conflictDetector, null);
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
