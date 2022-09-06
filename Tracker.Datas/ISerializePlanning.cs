using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public interface ISerializePlanning
    {
        public PlanningDTO Deserialize(string planningString);

        public string Serialize(PlanningDTO planning);
    }
}
