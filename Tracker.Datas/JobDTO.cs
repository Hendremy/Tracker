using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public class JobDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }

        public string Technician { get; init; }

        public TimeReport TimeReport { get; init; }

        public DateTime ActualStartDate => TimeReport.ActualStartDate;
        public DateTime ActualEndDate => TimeReport.ActualEndDate;
        public DateTime ExpectedStartDate => TimeReport.ExpectedStartDate;
        public DateTime ExpectedEndDate => TimeReport.ExpectedEndDate;

        public JobDTO (string name, string description, string technician, DateTime expStart, DateTime expEnd, DateTime actStart, DateTime actEnd)
        {
            Name = name;
            Description = description;
            Technician = technician;
            var expected = new DaySpan(expStart, expEnd);
            var actual = new DaySpan(actStart, actEnd);
            TimeReport = new TimeReport(expected, actual);
        }
    }
}
