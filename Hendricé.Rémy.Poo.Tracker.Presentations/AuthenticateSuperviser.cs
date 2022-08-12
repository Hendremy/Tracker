using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class AuthenticateSuperviser
    {
        private readonly IAuthenticateView _view;
        private readonly ITrackerRepository _repository;
        private readonly IAuthenticate _authenticator;

        private event EventHandler<string> UserAuthentified;
        private event EventHandler AboutToQuit;

        public AuthenticateSuperviser(IAuthenticateView view, ITrackerRepository repository, IAuthenticate authenticator)
        {
            _repository = repository;
            _view = view;
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.AuthenticationTried += OnAunthenticate;
            _view.QuitRequested += OnQuitRequested;
        }

        private void OnAunthenticate(object sender, AuthenticateEventArgs args)
        {
            try
            {
                IEnumerable<User> users = _repository.GetUsers();
                string userCode = _authenticator.TryAuthentify(users, args.Code, args.Password);
                if(userCode != null)
                {
                    UserAuthentified?.Invoke(this, userCode);
                    CloseView();
                }
                else
                {
                    _view.ShowLoginError("Code ou mot de passe incorrect");
                }
            }
            catch (TrackerRepositoryException ex)
            {
                _view.ShowInternalError(ex.Message);
            }
        }

        private void CloseView()
        {
            _view.CloseView();
        }

        private void OnQuitRequested(object sender, EventArgs args)
        {
            AboutToQuit?.Invoke(this, EventArgs.Empty);
            CloseView();
        }
    }
}
