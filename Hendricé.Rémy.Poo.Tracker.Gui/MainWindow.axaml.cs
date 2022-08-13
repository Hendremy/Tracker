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

        public MainWindow()
        {
            InitializeComponent();
            LocateControls();
        }

        public void InitJobsTab(Presentations.JobsSuperviser superviser)
        {
            ;
        }

        private void LocateControls()
        {
            _tabs = this.FindControl<TabControl>("Tabs");
        }


    }
}
