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

        private readonly ITrackerServices _services;
        private readonly SuperviserCreator _superviserCreator;
        private MainSuperviser _mainSuperviser;

        private Program()
        {
            _services = new TrackerServiceProvider("../../../../../json", "users.json", "plannings");
            _superviserCreator = new SuperviserCreator(_services);
            CreateMainView();
            CreateAuthenticateView();
        }

        private void CreateMainView()
        {
            var view = new MainView();
            _mainSuperviser = _superviserCreator.CreateMainSuperviser(view);
            CreateTabViews(_mainSuperviser);
        }

        private void CreateTabViews(MainSuperviser mainSuperviser)
        {
            CreateJobListView(mainSuperviser);
        }

        private void CreateJobListView(MainSuperviser mainSuperviser)
        {
            var jobsView = new JobListView();
            var jobsSuperviser = _superviserCreator.CreateJobListSuperviser(jobsView);
            mainSuperviser.JobListSuperviser = jobsSuperviser;
        }

        private void CreateAuthenticateView()
        {
            var view = new AuthenticateView();
            var authSuperviser = _superviserCreator.CreateAuthenticateSuperviser(view);
            view.ShowDialog();
            authSuperviser.UserAuthentified += _mainSuperviser.OnUserAuthentified;
        }
    }
}
