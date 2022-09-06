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
        private ITrackerServices _services;
        private PresenterCreator _superviserCreator;
        private MainPresenter _mainSuperviser;


        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            _services = new TrackerServiceProvider("../../../../../json", "users.json", "plannings");
            _superviserCreator = new PresenterCreator(_services);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = CreateMainView();
                desktop.MainWindow = mainWindow;
                desktop.MainWindow.Opened += MainWindow_Opened;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private MainWindow CreateMainView()
        {
            var mainWindow = new MainWindow();
            _mainSuperviser = _superviserCreator.CreateMainPresenter(mainWindow);
            CreateTabViews(mainWindow, _mainSuperviser);
            return mainWindow;
        }

        private void CreateTabViews(MainWindow mainWindow, MainPresenter mainSuperviser)
        {
            CreateJobListView(mainWindow, mainSuperviser);
            CreateGanttView(mainWindow, mainSuperviser);
        }

        private void CreateJobListView(MainWindow mainWindow, MainPresenter mainSuperviser)
        {
            var jobsView = new JobListView();
            mainWindow.AddJobsView(jobsView);
            var jobsSuperviser = _superviserCreator.CreateJobListPresenter(jobsView);
            mainSuperviser.JobListPresenter = jobsSuperviser;
        }

        private void CreateGanttView(MainWindow mainWindow, MainPresenter mainSuperviser)
        {
            var ganttView = new GanttView();
            mainWindow.AddGanttView(ganttView);
            var ganttSuperviser = _superviserCreator.CreateGanttPresenter(ganttView);
            mainSuperviser.GanttSuperviser = ganttSuperviser;
        }

        private void CreateAuthenticateWindow(AuthenticateWindow view)
        {
            var authSuperviser = _superviserCreator.CreateAuthenticatePresenter(view);
            authSuperviser.UserAuthentified += _mainSuperviser.OnUserAuthentified;
            authSuperviser.AboutToQuit += Superviser_AboutToQuit;
        }

        private void MainWindow_Opened(object? sender, EventArgs e)
        {
            var authenticateWindow = new AuthenticateWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SystemDecorations = SystemDecorations.BorderOnly
            };

            CreateAuthenticateWindow(authenticateWindow);

            authenticateWindow.ShowDialog(sender as Window);
        }

        private void Superviser_AboutToQuit(object? sender, EventArgs args)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown(0);
            }
        }
    }
}
