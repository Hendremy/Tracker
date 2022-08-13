using Avalonia.Controls;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class MainWindow : Window, IMainView
    {
        private TabControl _tabs;
        private TabItem _jobsTab;
        private TabItem _ganttTab;
        private TabItem _reportTab;


        public MainWindow()
        {
            InitializeComponent();
            LocateControls();
        }

        public void AddJobsView(JobsView jobsTab)
        {
            _jobsTab.Content = jobsTab;
        }

        public void AddGanttView(GanttView ganttView)
        {
            _ganttTab.Content = ganttView;
        }

        public void AddReportView(ReportView reportView)
        {
            _reportTab.Content = reportView;
        }

        private void LocateControls()
        {
            _tabs = this.FindControl<TabControl>("Tabs");
            _jobsTab = this.FindControl<TabItem>("Jobs");
            _ganttTab = this.FindControl<TabItem>("Gantt");
            _reportTab = this.FindControl<TabItem>("Report");
        }


    }
}
