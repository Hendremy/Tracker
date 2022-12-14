using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public interface IJobListView
    {
        public event EventHandler<SortParams> SortRequested;
        public event EventHandler<FilterParams> FilterRequested;
        public event EventHandler QuitRequested;
        public event EventHandler<JobViewCreatedEventArgs> JobViewCreated;

        public void Close();
        public void ShowConflicts(IEnumerable<JobConflict> conflicts);
        public void Update(IEnumerable<Job> jobs);
    }
}
