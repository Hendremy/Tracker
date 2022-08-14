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

        private readonly SuperviserCreator _superviserCreator = new SuperviserCreator();
        private MainSuperviser _mainSuperviser;

        //TODO: utiliser superviser creator pour créer les superviser
        private Program()
        {
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

        private MainView CreateMainView()
        {
            var mainView = new MainView();
            _mainSuperviser = _superviserCreator.CreateMainSuperviser(mainView);
            return mainView;
        }

        private void CreateJobListView(MainView mainWindow, MainSuperviser mainSuperviser)
        {
            var jobsView = new JobListView();
            mainWindow.AddJobsView(jobsView);
            var jobsSuperviser = _superviserCreator.CreateJobsSuperviser(jobsView);
            mainSuperviser.JobListSuperviser = jobsSuperviser;
        }

        private void CreateAuthenticateWindow(AuthenticateView view)
        {
            var authSuperviser = _superviserCreator.CreateAuthenticateSuperviser(view);
            authSuperviser.UserAuthentified += _mainSuperviser.OnUserAuthentified;
            authSuperviser.AboutToQuit += Superviser_AboutToQuit;
        }
    }
}
