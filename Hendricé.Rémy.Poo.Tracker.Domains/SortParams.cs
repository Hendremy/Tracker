using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public record SortParams
    {

        public SortParams()
        {
            Param = SortOption.StartDate;
            Ascending = true;
        }

        public SortParams(SortOption param, bool ascending)
        {
            Param = param;
            Ascending = ascending;
        }

        public SortOption Param { get; init; }
        public bool Ascending { get; init; }
    }
}
