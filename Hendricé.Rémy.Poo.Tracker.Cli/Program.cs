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
        private readonly PresenterCreator _presenterCreator;
        private MainPresenter _mainPresenter;

        private Program()
        {
            _services = new TrackerServiceProvider("../../../../../json", "users.json", "plannings");
            _presenterCreator = new PresenterCreator(_services);
            CreateMainView();
            CreateAuthenticateView();
        }

        private void CreateMainView()
        {
            var view = new MainView();
            _mainPresenter = _presenterCreator.CreateMainPresenter(view);
            CreateTabViews(_mainPresenter);
        }

        private void CreateTabViews(MainPresenter mainSuperviser)
        {
            CreateJobListView(mainSuperviser);
        }

        private void CreateJobListView(MainPresenter mainSuperviser)
        {
            var jobsView = new JobListView();
            var jobsSuperviser = _presenterCreator.CreateJobListPresenter(jobsView);
            mainSuperviser.JobListPresenter = jobsSuperviser;
        }

        private void CreateAuthenticateView()
        {
            var view = new AuthenticateView();
            var authPresenter = _presenterCreator.CreateAuthenticatePresenter(view);
            view.ShowDialog();
            authPresenter.UserAuthentified += _mainPresenter.OnUserAuthentified;
        }
    }
}
