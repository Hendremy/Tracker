using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Presentations
{
    public record FilterEventArgs
    {
        public FilterOption Param { get; init; }
        public string Value { get; init; }

        public FilterEventArgs(FilterOption param, string value)
        {
            Param = param;
            Value = value;
        }
    }
}
