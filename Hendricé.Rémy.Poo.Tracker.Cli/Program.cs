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

        private AuthenticateSuperviser authSuperviser;
        private event EventHandler<User> UserAuthentified;

        private Program()
        {
            ITrackerRepository repository = new JSONTrackerRepository();
            IAuthenticate authenticator = new Authenticator();
            CreateAuthentifySuperviser(repository, authenticator);
        }

        private void CreateAuthentifySuperviser(ITrackerRepository repository, IAuthenticate authenticator)
        {
            AuthenticateView view = new AuthenticateView();
            authSuperviser = new AuthenticateSuperviser(view, repository, authenticator);
            view.ShowDialog();
        }

        private void CreateMainSuperviser(ITrackerRepository repository)
        {
            IMainView view = new MainView();
            MainSuperviser mainSuperviser = new MainSuperviser(view, repository);
        }
    }
}
