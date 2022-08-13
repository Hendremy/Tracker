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
        public MainWindow()
        {
            InitializeComponent();
        }

        public event EventHandler<SortParams> SortRequested;
        public event EventHandler<FilterParams> FilterRequested;
        public event EventHandler QuitRequested;

        public void ShowUserJobs()
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<Job> jobs)
        {
            throw new NotImplementedException();
        }
    }
}
