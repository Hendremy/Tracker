using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private ITrackerRepository _repository;
        private IAuthenticate _authenticator;

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _repository = new JSONTrackerRepository();
                _authenticator = new Authenticator();

                var mainWindow = new MainWindow();
                desktop.MainWindow = mainWindow;
                CreateMainSuperviser(mainWindow);
                desktop.MainWindow.Opened += MainWindow_Opened;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void CreateAuthenticateSuperviser(AuthenticateWindow view)
        {
            var authSuperviser = new AuthenticateSuperviser(view, _repository, _authenticator);
            authSuperviser.UserAuthentified += OnUserAuthentified;
        }

        private void MainWindow_Opened(object? sender, System.EventArgs e)
        {
            var authenticateWindow = new AuthenticateWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SystemDecorations = SystemDecorations.BorderOnly
            };

            CreateAuthenticateSuperviser(authenticateWindow);

            authenticateWindow.ShowDialog(sender as Window);
        }

        private void OnUserAuthentified(object sender, string code)
        {

        }

        private void CreateMainSuperviser(MainWindow mainwindow)
        {
            var view = new MainWindow();
            var mainSuperviser = new MainSuperviser(view, _repository);
        }
    }
}
