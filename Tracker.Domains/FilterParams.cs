using Hendricé.Rémy.Poo.Tracker.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Domains
{
    public record FilterParams
    {
        public FilterOption Param { get; init; }
        public string Value { get; init; }

        public FilterParams()
        {
            Param = FilterOption.None;
            Value = "";
        }

        public FilterParams(FilterOption param, string value)
        {
            Param = param;
            Value = value;
        }
    }
}
