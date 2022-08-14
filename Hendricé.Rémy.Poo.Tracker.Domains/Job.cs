using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public class Job : INotifyPropertyChanged
    {
        private readonly string _name;
        private readonly string _description;
        private readonly Planning _planning;
        private readonly TimeReport _timeReport;

        public event PropertyChangedEventHandler PropertyChanged;

        public Job(string name, string description, Planning planning, TimeReport timeReport)
        {
            _name = name;
            _description = description;
            _planning = planning;
            _timeReport = timeReport;
        }

        public string Name => _name;

        public string Description => _description;

        public string Planning => _planning.Name;

        public TimeReport TimeReport => _timeReport;

        public JobStatus GetStatus() => _timeReport.GetStatus();

        public string GetStatusString() => GetStatus() switch
        {
            JobStatus.Todo => "A faire",
            JobStatus.Done => "Terminée",
            _ => "En cours"
        };

        public int GetDelay() => _timeReport.GetDelay();

        public bool HasDate(DateTime date)
        {
            return TimeReport.HasDate(date);
        }

        public void Start()
        {
            if (TimeReport.Start())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Start"));
            }
        }

        public void End()
        {
            if (TimeReport.End())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("End"));
            }
        }
    }
}
