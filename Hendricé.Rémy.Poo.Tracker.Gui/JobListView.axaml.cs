using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class JobListView : UserControl, IJobListView
    {
        private UserControl _sortControls;
        private UserControl _filterControls;
        private UserControl _conflictsControl;
        private ObservableCollection<JobControl> JobsObservable { get; init; }

        public event EventHandler<SortParams> SortRequested;
        public event EventHandler<FilterParams> FilterRequested;
        public event EventHandler<JobViewCreatedEventArgs> JobViewCreated;
        public event EventHandler QuitRequested;


        public JobListView()
        {
            JobsObservable = new ObservableCollection<JobControl>();
            DataContext = JobsObservable;
            InitializeComponent();
            LocateControls();
            InitSortControls();
            InitFilterControls();
        }

        private void LocateControls()
        {
            _sortControls = this.FindControl<UserControl>("SortControls");
            _filterControls = this.FindControl<UserControl>("FilterControls");
            _conflictsControl = this.FindControl<UserControl>("Conflicts");
        }

        private void InitSortControls()
        {
            var sortControls = new SortControls();
            _sortControls.Content = sortControls;
            sortControls.SortRequested += OnSortRequested;
        }

        private void InitFilterControls()
        {
            var filterControls = new FilterControls();
            _filterControls.Content = filterControls;
            filterControls.FilterRequested += OnFilterRequested;
        }

        public void Close()
        {
        }

        public void ShowConflicts(IEnumerable<JobConflict> conflicts)
        {
            if(conflicts.Count() > 0)
            {
                var conflictsControl = new ConflictsControl();
                conflictsControl.SetConflicts(conflicts);
                _conflictsControl.Content = conflictsControl;
                _conflictsControl.IsVisible = true;
            }
        }

        public void Update(IEnumerable<Job> jobs)
        {
            JobsObservable.Clear();
            foreach(Job job in jobs)
            {
                var jobControl = new JobControl();
                FireJobViewCreated(jobControl, job);
                JobsObservable.Add(jobControl);
            }
        }

        private void FireJobViewCreated(IJobView view, Job job)
        {
            JobViewCreatedEventArgs args = new JobViewCreatedEventArgs(view, job);
            JobViewCreated?.Invoke(this, args);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnFilterRequested(object? sender, FilterParams args)
        {
            FilterRequested?.Invoke(sender, args);
        }

        private void OnSortRequested(object? sender, SortParams args)
        {
            SortRequested?.Invoke(sender, args);
        }
    }
}
