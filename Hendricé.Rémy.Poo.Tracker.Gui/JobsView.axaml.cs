using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class JobsView : UserControl, IJobsView
    {
        public JobsView()
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

        private void SortParameter_Selected(object? sender, SelectionChangedEventArgs args)
        {

        }

        private void FilterParameter_Selected(object? sender, SelectionChangedEventArgs args)
        {

        }

        private void FilterField_Changed(object? sender, RoutedEventArgs args)
        {

        }
    }
}
