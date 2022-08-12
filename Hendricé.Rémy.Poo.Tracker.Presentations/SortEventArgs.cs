using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public record SortEventArgs
    {
        public SortEventArgs(SortOption param, bool ascending)
        {
            Param = param;
            Ascending = ascending;
        }

        public SortOption Param { get; init; }
        public bool Ascending { get; init; }
    }
}
