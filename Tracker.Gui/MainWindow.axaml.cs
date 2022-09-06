using Avalonia.Controls;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class MainWindow : Window, IMainView
    {
        private TabControl _tabs;
        private TabItem _jobsTab;
        private TabItem _ganttTab;

        private QuitErrorWindow? _errorWindow;

        public event EventHandler<CancelEventArgs> QuitRequested;
        public event EventHandler QuitForced;

        public MainWindow()
        {
            InitializeComponent();
            LocateControls();
            SubscribeToWindowEvents();
        }

        private void LocateControls()
        {
            _tabs = this.FindControl<TabControl>("Tabs");
            _jobsTab = this.FindControl<TabItem>("Jobs");
            _ganttTab = this.FindControl<TabItem>("Gantt");
        }

        private void SubscribeToWindowEvents()
        {
            this.Closing += OnQuitRequested;
        } 

        public void AddJobsView(JobListView jobsTab)
        {
            _jobsTab.Content = jobsTab;
        }

        public void AddGanttView(GanttView ganttView)
        {
            _ganttTab.Content = ganttView;
        }


        public void ShowInternalError(string message)
        {
            var errWin = new ErrorWindow();
            errWin.ShowError(this, message);
        }

        private void OnQuitRequested(object sender, CancelEventArgs args)
        {
            this.Closing -= OnQuitRequested;
            QuitRequested?.Invoke(sender, args);
        }

        public void AskForceQuit(string message)
        {
            _errorWindow = new QuitErrorWindow();
            _errorWindow.ShowError(this, message);
            _errorWindow.QuitForced += OnQuitForced;
            _errorWindow.Closed += OnErrorWindowClosed;
        }

        private void OnErrorWindowClosed(object? sender, EventArgs args)
        {
            SubscribeToWindowEvents();
        }

        private void OnQuitForced(object? sender, EventArgs args)
        {
            QuitForced?.Invoke(this, args);
        }

        public void CloseView()
        {
            this.Close();
        }
    }
}
