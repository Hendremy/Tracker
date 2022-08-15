using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public class PlanningDTO
    {
        public string Name { get; init; }
        public ISet<JobDTO> Jobs { get; init; }

        public PlanningDTO(string name, IEnumerable<JobDTO> jobs)
        {
            Name = name;
            Jobs = new HashSet<JobDTO>(jobs);
        }

    }
}
