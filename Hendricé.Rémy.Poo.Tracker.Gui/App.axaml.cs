using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Datas;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;

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
        private MainSuperviser _mainSuperviser;

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _repository = new JSONTrackerRepository("../../../../../json", "users.json", "plannings");
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
            authSuperviser.UserAuthentified += _mainSuperviser.OnUserAuthentified;
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

        private void CreateMainSuperviser(MainWindow mainwindow)
        {
            var view = new MainWindow();
            _mainSuperviser = new MainSuperviser(view, _repository, initSortHandler(), initFilterHandler(), new JobConflictDetector());
        }

        private void Superviser_AboutToQuit(object sender, EventArgs args)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown(0);
            }
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
