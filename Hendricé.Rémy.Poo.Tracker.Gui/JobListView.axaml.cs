using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections.Generic;

namespace Hendricé.Rémy.Poo.Tracker.Gui
{
    public partial class JobListView : UserControl, IJobListView
    {
        private UserControl _sortControls;
        private UserControl _filterControls;

        public event EventHandler<SortParams> SortRequested;
        public event EventHandler<FilterParams> FilterRequested;
        public event EventHandler QuitRequested;


        public JobListView()
        {
            InitializeComponent();
            LocateControls();
            InitSortControls();
            InitFilterControls();
        }

        private void LocateControls()
        {
            _sortControls = this.FindControl<UserControl>("SortControls");
            _filterControls = this.FindControl<UserControl>("FilterControls");
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
        }

        public void Update(IEnumerable<Job> jobs)
        {
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
