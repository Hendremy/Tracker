using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public interface IJobView
    {
        public event EventHandler StartJobRequested;
        public event EventHandler EndJobRequested;

        void InitView(Job job);
        void OnDatePropertyChanged(object? sender, PropertyChangedEventArgs args);
    }
}
