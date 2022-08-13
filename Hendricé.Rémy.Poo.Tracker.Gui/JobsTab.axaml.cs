using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class JobsTab : UserControl, IJobsView
    {
        public JobsTab()
        {
            InitializeComponent();
        }

        public event EventHandler<SortParams> SortRequested;
        public event EventHandler<FilterParams> FilterRequested;
        public event EventHandler QuitRequested;

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void ShowConflicts(IEnumerable<JobConflict> conflicts)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<Job> jobs)
        {
            throw new NotImplementedException();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
