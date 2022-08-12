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

        private readonly ITrackerRepository _repository;
        private readonly IAuthenticate _authenticator;

        private Program()
        {
            _repository = new JSONTrackerRepository();
            _authenticator = new Authenticator();
            CreateAuthentifySuperviser();
        }

        private void CreateAuthentifySuperviser()
        {
            var view = new AuthenticateView();
            var authSuperviser = new AuthenticateSuperviser(view, _repository, _authenticator);
            view.ShowDialog();
            authSuperviser.UserAuthentified += OnUserAuthentified;
        }

        private void OnUserAuthentified(object sender, string code)
        {
            CreateMainSuperviser(code);
        }

        private void CreateMainSuperviser(string code)
        {
            var view = new MainView();
            var mainSuperviser = new MainSuperviser(view, _repository);
            view.Start();
        }
    }
}
