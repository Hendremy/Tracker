using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public class AuthenticatePresenter
    {
        private readonly IAuthenticateView _view;
        private readonly ITrackerRepository _repository;
        private readonly IAuthenticate _authenticator;

        public event EventHandler<string> UserAuthentified;
        public event EventHandler AboutToQuit;

        public AuthenticatePresenter(IAuthenticateView view, ITrackerRepository repository, IAuthenticate authenticator)
        {
            _repository = repository;
            _authenticator = authenticator;
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
                var users = _repository.GetUsersCredentials();
                bool authOk = _authenticator.CredentialsAreValid(users, args.Code, args.Password);
                HandleAuthResult(authOk, args.Code);
            }
            catch (TrackerRepositoryException ex)
            {
                _view.ShowInternalError(ex.Message);
            }
        }

        private void HandleAuthResult(bool authOk, string code)
        {
            if (authOk)
            {
                UserAuthentified?.Invoke(this, code);
                CloseView();
            }
            else
            {
                _view.ShowLoginError("Code ou mot de passe incorrect");
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
