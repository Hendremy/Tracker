using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public class Presenter
    {
        private static Presenter singleton;
        private static readonly string JOBLIST_HEADER = $"{"Chantier",-30}|{"Description",-25}|{"Dates de début et de fin prévues",-32}|{"Statut",-10}|{"Dates de début et de fin effectives",-35}|{"Jours de retard",-15}\n" +
            $"--------------------------------------------------------------------------------------------------------------------------------------------------------\n";

        public static Presenter GetInstance()
        {
            if(singleton == null)
            {
                singleton = new Presenter();
            }
            return singleton;
        }

        private Presenter() { }

        public string JobListToString(IEnumerable<Job> items)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(JOBLIST_HEADER);
            foreach(Job job in items)
            {
                string expectedSpan = $"{job.TimeReport.ExpectedStartDate.ToShortDateString()} => {job.TimeReport.ExpectedEndDate.ToShortDateString()}";
                string actualSpan = ActualSpanToString(job);
                string status = job.GetStatusString();
                string jobRow = $"\n{job.Planning,-30}|{ job.Description,-25}|{ expectedSpan,-32}|{ status,-10}|{ actualSpan,-35}|{job.GetDelay(),-15}";
                sb.Append(jobRow);
            }
            sb.Append('\n');
            return sb.ToString();
        }

        private string ActualSpanToString(Job job) => job.GetStatus() switch
        {
            JobStatus.Done => $"{job.TimeReport.ActualStartDate.ToShortDateString()} => {job.TimeReport.ActualEndDate.ToShortDateString()}",
            JobStatus.Doing => $"{job.TimeReport.ActualStartDate.ToShortDateString()} => Indéfini",
            _ => "Indéfinies"
        };

        public string SortOptionToString(SortOption opt) => opt switch
        {
            SortOption.Planning => "Chantier",
            SortOption.Status => "Statut",
            _ => "Date de début"
        };

        public string FilterOptionToString(FilterOption opt) => opt switch
        {
            FilterOption.Planning => "Chantier",
            FilterOption.Status => "Statut",
            FilterOption.None => "-",
            _ => "Date"
        };
    }
}
