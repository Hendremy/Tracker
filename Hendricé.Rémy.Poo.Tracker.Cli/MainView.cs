using Hendricé.Rémy.Poo.Tracker.Domains;
using Hendricé.Rémy.Poo.Tracker.Presentations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public class MainView : CliView, IMainView
    {
        private const string MENU = "1 -> Modifier tri | 2 -> Modifier filtre | 3 -> Quitter";
        private bool _stop = false;
        private string _filterChoice = "-";
        private string _sortChoice= "-";

        public MainView()
        {
            _thread = new Thread(new ThreadStart(Loop));
        }

        public event EventHandler<SortEventArgs> SortRequested;
        public event EventHandler<FilterEventArgs> FilterRequested;
        public event EventHandler QuitRequested;

        public void Start(ObservableCollection<Job> jobs)
        {
            jobs.CollectionChanged += OnCollectionChanged;
            StartThread();
        }

        private void Loop()
        {
            while (!_stop)
            {
                HandleChoice(AskInt(MENU));
            }
            QuitRequested?.Invoke(this, EventArgs.Empty);
        }

        private void HandleChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    ChooseSort();
                    break;
                case 2:
                    ChooseFilter();
                    break;
                case 3:
                    RequestQuit();
                    break;
            }
        }

        private void ChooseSort()
        {
            SortEventArgs sortArgs = new ChooseSortView().AskChoice();
            string ascending = sortArgs.Ascending ? "^" : "v"; 
            _sortChoice = $"{sortArgs.Param} {ascending}";
            SortRequested.Invoke(this, sortArgs);
        }

        private void ChooseFilter()
        {
            FilterEventArgs filterArgs = new ChooseFilterView().AskChoice();
            _filterChoice = $"{filterArgs.Param} -> {filterArgs.Value}";
            FilterRequested?.Invoke(this, filterArgs);
        }

        private void RequestQuit()
        {
            QuitRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            Update(args.NewItems);
        }

        private void Update(IEnumerable items)
        {
            IEnumerable<Job> jobs = (IEnumerable<Job>) items;
            WriteLine("Tâches"
                + "\n-------------------------"
                + $"\nTrié par : {_sortChoice,-7} | Filtré par : {_filterChoice,-7}\n");
            WriteLine(_presenter.JobListToString(jobs));
        }

        public void Close()
        {
            _stop = true;
        }
    }
}
